using Autofac;
using GL.Kit.Extension;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using HyEye.Services;
using HyEye.WForm.Calibration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VisionFactory;
using WeifenLuo.WinFormsUI.Docking;
using IImageService = HyEye.Services.IImageService;

namespace HyEye.WForm.Settings
{
    public delegate void ClickShowToolBlockSetDelegate(string taskName);

    public partial class FrmTaskSetting : DockContent
    {
        readonly ITaskRepository taskRepo;
        readonly ICameraRepository cameraRepo;
        readonly ICalibrationRepository calibRepo;
        readonly IOpticsRepository opticsRepo;
        readonly INameMappingRepository nameMappingRepo;
        readonly ITaskService taskService;
        readonly IImageService imageService;
        readonly ToolBlockComponentSet componentSet;
        readonly IGLog log;

        public ClickShowToolBlockSetDelegate showToolBlockSetHandle;

        public FrmTaskSetting(ITaskRepository taskRepo,
            ICameraRepository cameraRepo,
            ICalibrationRepository calibRepo,
            IOpticsRepository opticsRepo,
            INameMappingRepository nameMappingRepo,
            ICommandRepository commandRepo,  // 这里仅仅是为了实例化一下好让事件加载
            ITaskService taskService,
            IImageService imageService,
            ToolBlockComponentSet componentSet,
            IGLog log)
        {
            InitializeComponent();

            this.log = log;

            this.taskRepo = taskRepo;
            this.cameraRepo = cameraRepo;
            this.calibRepo = calibRepo;
            this.opticsRepo = opticsRepo;
            this.nameMappingRepo = nameMappingRepo;

            this.taskService = taskService;
            this.imageService = imageService;

            this.componentSet = componentSet;

            cameraRepo.AfterCameraRename += CameraRepo_CameraRename;
            cameraRepo.AfterSetCameraInfo += CameraRepo_AfterSetCameraInfo;

            this.componentSet.ToolBlockRemovedCalib += ComponentSet_ToolBlockRemovedCalib;

            InitCalibrationMenu();
        }

        readonly Color VirtualCameraForeColor = Color.FromArgb(0xFF, 0xBF, 0x00);
        readonly Color PhysicalCameraForeColor = Color.Black;
        readonly Color ErrorCameraForeColor = Color.Red;

        private void CameraRepo_AfterSetCameraInfo(List<CameraInfoVO> obj)
        {
            LoadTask();

            BuildCameraMenu();
        }

        private void CameraRepo_CameraRename(string sn, string name)
        {
            foreach (TreeNode taskNode in tvTasks.Nodes)
            {
                if (taskNode.Nodes.Count == 0) continue;

                TreeNode cameraNode = taskNode.FirstNode;

                TaskInfoVO task = taskRepo.GetTaskByName(taskNode.Text);
                if (task.CameraAcquireImage.CameraSN == sn)
                {
                    CameraInfoVO camera = cameraRepo.GetCameraInfo(sn);
                    if (camera != null)
                    {
                        cameraNode.Text = camera.ToString();
                    }
                }
            }

            foreach (ToolStripMenuItem tsmi in tsmiAddCamera.DropDownItems)
            {
                CameraInfoVO camera;
                if ((camera = (CameraInfoVO)tsmi.Tag).SN == sn)
                {
                    camera.UserDefinedName = name;
                    tsmi.Text = name;
                }
            }
        }

        private void ComponentSet_ToolBlockRemovedCalib(string taskName, string calibName)
        {
            TreeNode taskNode = tvTasks.FindNode(false, n => n.Text == taskName);
            TreeNode calibNode = tvTasks.FindNode(true, n => n.Text == calibName);

            if (calibNode == null) return;

            string acqImageName = getAcqImageName(calibNode);
            CalibrationType calibType = (CalibrationType)calibNode.Tag;

            taskRepo.DeleteCalibration(taskName, acqImageName, calibType, calibName);

            calibNode.Remove();
        }

        void InitCalibrationMenu()
        {
            foreach (CalibrationType type in Enum.GetValues(typeof(CalibrationType)))
            {
                ToolStripMenuItem tsmiNewCalibrationType = new ToolStripMenuItem
                {
                    Text = type.ToDescription(),
                    Tag = type
                };
                tsmiNewCalibrationType.Click += TsmiNewCalibrationType_Click;
                tsmiNewCalibration.DropDownItems.Add(tsmiNewCalibrationType);
            }
        }

        private void FrmTaskSetting_Load(object sender, EventArgs e)
        {
            BuildCameraMenu();

            LoadTask();

            formSettings = AutoFacContainer.Resolve<FormSettings>();
            formMain = AutoFacContainer.Resolve<FormMain>();

            vpTaskNameIndex = tvTasks.GetNewNameIndex("VP 任务", 0);
            hyTaskNameIndex = tvTasks.GetNewNameIndex("HY 任务", 0);
            acqImageNameIndex = tvTasks.GetNewNameIndex("拍照", 2, t => { return t.Split(ConnChar)[0]; });
            checkerboardIndex = tvTasks.GetNewNameIndex("Checkerboard", 3);
            handEyeIndex = tvTasks.GetNewNameIndex("HandEye", 3);
            jointIndex = tvTasks.GetNewNameIndex("联合标定", 3);
        }

        #region 加载

        // 加载右键菜单相机
        void BuildCameraMenu()
        {
            tsmiAddCamera.DropDownItems.Clear();

            List<CameraInfoVO> cameraInfos = cameraRepo.GetCamerasWithVirtual();

            foreach (CameraInfoVO camera in cameraInfos)
            {
                ToolStripMenuItem tsmiCamera = new ToolStripMenuItem
                {
                    Text = camera.ToString(),
                    Tag = camera
                };

                tsmiAddCamera.DropDownItems.Add(tsmiCamera);

                tsmiCamera.Click += tsmiSetCamera_Click;
            }
        }

        /********************************************
         * 相机节点的 Tag 添加 CameraInfoVO
         * 标定节点的 Tag 添加 CalibrationType
         ********************************************/

        //拍照名称和 ToolBlock 中对应的 Input 名称之间的连接符
        const char ConnChar = ':';

        // 加载任务
        void LoadTask()
        {
            tvTasks.BeginUpdate();

            tvTasks.Nodes.Clear();

            List<TaskInfoVO> tasks = taskRepo.GetTasks();

            foreach (TaskInfoVO task in tasks)
            {
                TreeNode taskNode = buildTaskNode(task);
                tvTasks.Nodes.Add(taskNode);

                if (task.CameraAcquireImage != null)
                {
                    TreeNode cameraNode = buildCameraNode(task.CameraAcquireImage);
                    if (cameraNode != null)
                    {
                        taskNode.Nodes.Add(cameraNode);

                        foreach (AcquireImageInfoVO acqImage in task.CameraAcquireImage.AcquireImages)
                        {
                            TreeNode acqImageNode = buildAcqImageNode(task.Name, acqImage);
                            cameraNode.Nodes.Add(acqImageNode);
                        }
                    }
                }
            }

            tvTasks.ExpandAll();

            TreeNode buildTaskNode(TaskInfoVO task)
            {
                return new TreeNode
                {
                    Text = task.Name,
                    Checked = task.Enabled
                };
            }

            TreeNode buildCameraNode(CameraAcquireImageInfoVO cameraAcqImage)
            {
                CameraInfoVO camera = cameraRepo.GetCameraInfo(cameraAcqImage.CameraSN);
                if (camera != null)
                {
                    return new TreeNode(camera.ToString())
                    {
                        ForeColor = camera.IsVirtualCamera() ? VirtualCameraForeColor : PhysicalCameraForeColor
                    };
                }
                else
                {
                    return new TreeNode("!" + cameraAcqImage.CameraSN)
                    {
                        ForeColor = ErrorCameraForeColor
                    };
                }
            }

            TreeNode buildAcqImageNode(string taskName, AcquireImageInfoVO acqImage)
            {
                string inputName = nameMappingRepo.GetAcqImageMapping(taskName, acqImage.Name);

                TreeNode acqImageNode = new TreeNode($"{acqImage.Name}{ConnChar}{inputName}");

                if (!string.IsNullOrEmpty(acqImage.CheckerboardName))
                {
                    TreeNode checkerboardNode = new TreeNode
                    {
                        Text = acqImage.CheckerboardName,
                        Tag = CalibrationType.Checkerboard
                    };
                    acqImageNode.Nodes.Add(checkerboardNode);
                }
                if (acqImage.HandEyeNames != null && acqImage.HandEyeNames.Count > 0)
                {
                    foreach (string handeyeName in acqImage.HandEyeNames)
                    {
                        TreeNode handEyeNode = new TreeNode
                        {
                            Text = handeyeName,
                            Tag = CalibrationType.HandEye
                        };
                        acqImageNode.Nodes.Add(handEyeNode);
                    }
                }
                if (!string.IsNullOrEmpty(acqImage.HandEyeSingleName))
                {
                    TreeNode handeyeSingleNode = new TreeNode
                    {
                        Text = acqImage.HandEyeSingleName,
                        Tag = CalibrationType.HandEyeSingle
                    };
                    acqImageNode.Nodes.Add(handeyeSingleNode);
                }
                if (!string.IsNullOrEmpty(acqImage.JointName))
                {
                    TreeNode jointNode = new TreeNode
                    {
                        Text = acqImage.JointName,
                        Tag = CalibrationType.Joint
                    };
                    acqImageNode.Nodes.Add(jointNode);

                    bool exists = false;
                    foreach (ToolStripItem tsmi in tsmiQuoteJointCalib.DropDownItems)
                    {
                        if (tsmi.Text == acqImage.JointName)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        AddQuoteJoint(acqImage.JointName);
                    }
                }

                return acqImageNode;
            }

            tvTasks.EndUpdate();
        }

        private void AddQuoteJoint(string name)
        {
            ToolStripMenuItem tsmiQuoteJoint = new ToolStripMenuItem
            {
                Text = name
            };
            tsmiQuoteJoint.Click += tsmiQuoteJointCalib_Click;
            tsmiQuoteJointCalib.DropDownItems.Add(tsmiQuoteJoint);
        }

        // 任务树拍照节点的 ToolTip
        string getToolTipText(string taskName, string acqImageName)
        {
            return nameMappingRepo.GetAcqImageMapping(taskName, acqImageName);
        }

        #endregion

        Operation operation = Operation.None;

        bool esc = false;

        private void tvTasks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                esc = true;
                //tvTasks.SelectedNode.EndEdit(true);
                tvTasks.LabelEdit = false;
            }
        }

        string getTaskName(TreeNode node)
        {
            while (node.Level != 0)
            {
                node = node.Parent;
            }
            return node.Text;
        }

        string getAcqImageName(TreeNode node)
        {
            if (node.Level < 2) throw new Exception("Bug: 获取拍照名称错误");

            while (node.Level != 2)
            {
                node = node.Parent;
            }

            return node.Text.Split(ConnChar)[0];
        }

        #region 树节点编辑

        private void tvTasks_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (operation == Operation.AddVisionProTask)
            {
                if (esc)
                {
                    e.Node.Remove();
                    esc = false;
                    return;
                }

                string taskName = (e.Label ?? e.Node.Text)?.Trim();

                if (!verifyTaskName(taskName, out string errmsg))
                {
                    if (e.Label != null)
                        e.Node.Text = e.Label;

                    MessageBoxUtils.ShowWarn(errmsg);
                    TopLevelControl.BringToFront();

                    tvTasks.SelectedNode = e.Node;
                    e.Node.BeginEdit();

                    return;
                }

                taskRepo.AddTask(taskName, TaskType.VP);
            }
            else if (operation == Operation.AddHyVisionTask)
            {
                if (esc)
                {
                    e.Node.Remove();
                    esc = false;
                    return;
                }

                string taskName = (e.Label ?? e.Node.Text)?.Trim();

                if (!verifyTaskName(taskName, out string errmsg))
                {
                    if (e.Label != null)
                        e.Node.Text = e.Label;

                    MessageBoxUtils.ShowWarn(errmsg);
                    TopLevelControl.BringToFront();

                    tvTasks.SelectedNode = e.Node;
                    e.Node.BeginEdit();

                    return;
                }

                taskRepo.AddTask(taskName, TaskType.HY);
            }
            else if (operation == Operation.RenameTask)
            {
                if (e.Label != null && e.Label != e.Node.Text)
                {
                    if (!verifyTaskName(e.Label, out string errmsg))
                    {
                        e.CancelEdit = true;

                        MessageBoxUtils.ShowWarn(errmsg);
                        TopLevelControl.BringToFront();

                        tvTasks.SelectedNode = e.Node;
                        e.Node.BeginEdit();

                        return;
                    }

                    taskRepo.RenameTask(e.Node.Text, e.Label);

                    // 如果 [任务1] 重命名成 [任务2]，再次添加任务时，默认任务名称依然是 [任务2]
                }
            }
            else if (operation == Operation.AddAcqImage)
            {
                if (esc)
                {
                    e.Node.Remove();
                    esc = false;
                    return;
                }

                string acqImageName = (e.Label ?? e.Node.Text).Trim();
                string taskName = getTaskName(e.Node);

                if (!verifyAcqImageName(taskName, acqImageName, out string errmsg))
                {
                    if (e.Label != null)
                        e.Node.Text = e.Label;

                    MessageBoxUtils.ShowWarn(errmsg);
                    TopLevelControl.BringToFront();

                    tvTasks.SelectedNode = e.Node;
                    e.Node.BeginEdit();

                    return;
                }

                taskRepo.AddAcqImage(taskName, acqImageName);

                tvTasks.BeginInvoke(new MethodInvoker(
                     delegate
                     {
                         string inputName = nameMappingRepo.GetAcqImageMapping(taskName, acqImageName);
                         tvTasks.SelectedNode.Text = $"{acqImageName}{ConnChar}{inputName}";
                     }));

                //string inputName = nameMappingRepo.GetAcqImageMapping(taskName, acqImageName);
                //e.Node.Text = $"{acqImageName}{ConnChar}{inputName}";

                //e.Node.ToolTipText = getToolTipText(taskName, acqImageName);
            }
            else if (operation == Operation.RenameAcqImage)
            {
                if (e.Label != null && e.Label != e.Node.Text)
                {
                    string acqImageName = e.Label.Trim();
                    string taskName = getTaskName(e.Node);

                    if (!verifyAcqImageName(taskName, acqImageName, out string errmsg))
                    {
                        e.CancelEdit = true;

                        MessageBoxUtils.ShowWarn(errmsg);
                        TopLevelControl.BringToFront();

                        tvTasks.SelectedNode = e.Node;
                        e.Node.BeginEdit();

                        return;
                    }

                    taskRepo.RenameAcqImage(taskName, e.Node.Text, acqImageName);

                    tvTasks.BeginInvoke(new MethodInvoker(
                     delegate
                     {
                         string inputName = nameMappingRepo.GetAcqImageMapping(taskName, acqImageName);
                         tvTasks.SelectedNode.Text = $"{acqImageName}{ConnChar}{inputName}";
                     }));
                }
            }
            else if (operation == Operation.AddCheckerboard
                || operation == Operation.AddHandEye
                || operation == Operation.AddHandEye1
                || operation == Operation.AddJoint)
            {
                if (esc)
                {
                    e.Node.Remove();
                    esc = false;
                    return;
                }

                string calibName = (e.Label ?? e.Node.Text).Trim();

                if (!verifyCalibrationName(calibName, out string errmsg))
                {
                    if (e.Label != null)
                        e.Node.Text = e.Label;

                    MessageBoxUtils.ShowWarn(errmsg);
                    TopLevelControl.BringToFront();

                    tvTasks.SelectedNode = e.Node;
                    e.Node.BeginEdit();
                    return;
                }

                string taskName = getTaskName(e.Node);
                string acqImageName = getAcqImageName(e.Node);
                CalibrationType type;

                if (operation == Operation.AddCheckerboard)
                    type = CalibrationType.Checkerboard;
                else if (operation == Operation.AddHandEye)
                    type = CalibrationType.HandEye;
                else if (operation == Operation.AddHandEye1)
                    type = CalibrationType.HandEyeSingle;
                else
                    type = CalibrationType.Joint;

                taskRepo.AddCalibration(taskName, acqImageName, type, calibName);

                if (operation == Operation.AddJoint)
                {
                    AddQuoteJoint(calibName);
                }
            }
            else if (operation == Operation.RenameCalibration)
            {
                if (e.Label != null && e.Label != e.Node.Text)
                {
                    string calibName = e.Label.Trim();
                    string taskName = getTaskName(e.Node);

                    if (!verifyCalibrationName(calibName, out string errmsg))
                    {
                        e.CancelEdit = true;

                        MessageBoxUtils.ShowWarn(errmsg);
                        TopLevelControl.BringToFront();

                        tvTasks.SelectedNode = e.Node;
                        e.Node.BeginEdit();

                        return;
                    }

                    CalibrationType calibType = (CalibrationType)e.Node.Tag;

                    taskRepo.RenameCalibration(taskName, calibType, e.Node.Text, calibName);
                }
            }

            tvTasks.LabelEdit = false;
        }

        bool verifyTaskName(string taskName, out string errmsg)
        {
            if (string.IsNullOrWhiteSpace(taskName))
            {
                errmsg = "任务名称不能为空";
                return false;
            }

            if (taskName.IsMatch(ComPattern.IllegalPathChar))
            {
                errmsg = "任务名称不能包含下列字符：/ \\ : * ? \" < > |";
                return false;
            }

            if (taskName.IsMatch("^\\.+$"))
            {
                errmsg = "无效的任务名称";
                return false;
            }

            if (taskRepo.ExistsTaskName(taskName))
            {
                errmsg = "任务名称已存在";
                return false;
            }

            errmsg = null;
            return true;
        }

        bool verifyAcqImageName(string taskName, string acqImageName, out string errmsg)
        {
            if (string.IsNullOrEmpty(acqImageName))
            {
                errmsg = "拍照名称不能为空";
                return false;
            }

            if (acqImageName.IsMatch(ComPattern.IllegalPathChar))
            {
                errmsg = "拍照名称不能包含下列字符：/ \\ : * ? \" < > |";
                return false;
            }

            if (acqImageName.IsMatch("^\\.+$"))
            {
                errmsg = "无效的拍照名称";
                return false;
            }

            if (taskRepo.ExistsAcqImage(taskName, acqImageName))
            {
                errmsg = "拍照名称已存在";
                return false;
            }

            errmsg = null;
            return true;
        }

        bool verifyCalibrationName(string calibName, out string errmsg)
        {
            if (string.IsNullOrWhiteSpace(calibName))
            {
                errmsg = "标定名称不能为空";
                return false;
            }

            if (calibName.IsMatch(ComPattern.IllegalPathChar))
            {
                errmsg = "标定名称不能包含下列字符：/ \\ : * ? \" < > |";
                return false;
            }

            if (calibName.IsMatch("^\\.+$"))
            {
                errmsg = "无效的标定名称";
                return false;
            }

            if (calibRepo.ExistsCalibrationName(calibName))
            {
                errmsg = "标定名称已存在";
                return false;
            }

            errmsg = null;
            return true;
        }

        #endregion

        #region 其他方法

        // 获取默认的任务名
        int vpTaskNameIndex;
        int hyTaskNameIndex;

        string getDefaultTaskName(TaskType type)
        {
            if (type == TaskType.VP)
                return "VP 任务" + (++vpTaskNameIndex).ToString();
            else
                return "HY 任务" + (++hyTaskNameIndex).ToString();
        }

        int acqImageNameIndex;

        string getDefaultAcqImageName()
        {
            return "拍照" + (++acqImageNameIndex).ToString();
        }

        int checkerboardIndex;
        int handEyeIndex;
        int jointIndex;

        string getDefaultCheckerboardName()
        {
            return "Checkerboard" + (++checkerboardIndex).ToString();
        }

        string getDefaultHandEyeName()
        {
            return "HandEye" + (++handEyeIndex).ToString();
        }

        string getDefalutJointName()
        {
            return "联合标定" + (++jointIndex).ToString();
        }

        #endregion

        #region 任务

        private void tsmiNewVisionProTask_Click(object sender, EventArgs e)
        {
            operation = Operation.AddVisionProTask;

            string taskName = getDefaultTaskName(TaskType.VP);

            TreeNode node = new TreeNode(taskName);

            tvTasks.Nodes.Add(node);
            tvTasks.SelectedNode = node;
            tvTasks.LabelEdit = true;

            node.BeginEdit();
        }

        private void tsmiHyVisionTask_Click(object sender, EventArgs e)
        {
            operation = Operation.AddHyVisionTask;

            string taskName = getDefaultTaskName(TaskType.HY);

            TreeNode node = new TreeNode(taskName);

            tvTasks.Nodes.Add(node);
            tvTasks.SelectedNode = node;
            tvTasks.LabelEdit = true;

            node.BeginEdit();
        }

        private void tsmiBatchAddTask_Click(object sender, EventArgs e)
        {
            FrmBatchAddTask frm = AutoFacContainer.Resolve<FrmBatchAddTask>();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < frm.TaskCount; i++)
                {
                    string taskName = getDefaultTaskName(frm.Type);
                    taskRepo.AddTask(taskName, frm.Type);

                    TreeNode taskNode = new TreeNode(taskName);
                    tvTasks.Nodes.Add(taskNode);

                    TreeNode cameraNode = new TreeNode
                    {
                        Text = frm.CameraInfo.ToString(),
                        ForeColor = frm.CameraInfo.IsVirtualCamera() ? VirtualCameraForeColor : PhysicalCameraForeColor
                    };
                    taskRepo.SetCamera(taskName, frm.CameraInfo, false);

                    taskNode.Nodes.Add(cameraNode);

                    for (int j = 0; j < frm.AcqImageCount; j++)
                    {
                        string acqImageName = getDefaultAcqImageName();
                        taskRepo.AddAcqImage(taskName, acqImageName);

                        string inputName = nameMappingRepo.GetAcqImageMapping(taskName, acqImageName);

                        TreeNode acqImageNode = new TreeNode($"{acqImageName}{ConnChar}{inputName}");

                        cameraNode.Nodes.Add(acqImageNode);
                    }

                    taskNode.ExpandAll();
                }
            }
        }

        private void tsmiRenameTask_Click(object sender, EventArgs e)
        {
            operation = Operation.RenameTask;

            tvTasks.LabelEdit = true;
            tvTasks.SelectedNode.BeginEdit();
        }

        private void tsmiDeleteTask_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowQuestion("确定要删除该任务吗？") == DialogResult.No) return;

            TreeNode selNode = tvTasks.SelectedNode;

            if (taskRepo.DeleteTask(selNode.Text))
            {
                selNode.Remove();
            }
        }

        private void tvTasks_AfterCheck(object sender, TreeViewEventArgs e)
        {
            taskRepo.SetTaskEnabled(e.Node.Text, e.Node.Checked);
        }

        #endregion

        #region 相机

        // 菜单选择相机
        private void tsmiSubSetCamera_Click(object sender, EventArgs e)
        {
            (CameraInfoVO camera, bool concurrent) = ((CameraInfoVO, bool))((sender as ToolStripMenuItem).Tag);

            TreeNode taskNode = tvTasks.SelectedNode.Level == 1 ? tvTasks.SelectedNode.Parent : tvTasks.SelectedNode;
            string taskName = taskNode.Text;

            taskRepo.SetCamera(taskName, camera, concurrent);

            buildCaramNode(taskNode, camera);
        }

        private void tsmiSetCamera_Click(object sender, EventArgs e)
        {
            CameraInfoVO camera = (CameraInfoVO)((sender as ToolStripMenuItem).Tag);

            TreeNode taskNode = tvTasks.SelectedNode.Level == 1 ? tvTasks.SelectedNode.Parent : tvTasks.SelectedNode;
            string taskName = taskNode.Text;

            taskRepo.SetCamera(taskName, camera, false);

            buildCaramNode(taskNode, camera);
        }

        void buildCaramNode(TreeNode taskNode, CameraInfoVO camera)
        {
            TreeNode cameraNode;
            if (taskNode.Nodes.Count == 0)
            {
                cameraNode = new TreeNode();

                taskNode.Nodes.Add(cameraNode);
            }
            else
            {
                cameraNode = taskNode.Nodes[0];
            }

            cameraNode.Text = camera.ToString();
            cameraNode.ForeColor = camera.IsVirtualCamera() ? VirtualCameraForeColor : PhysicalCameraForeColor;

            taskNode.ExpandAll();

            tvTasks.SelectedNode = cameraNode;
        }

        #endregion

        #region 拍照

        private void tsmiAddAcqImage_Click(object sender, EventArgs e)
        {
            operation = Operation.AddAcqImage;

            string acqImageName = getDefaultAcqImageName();
            TreeNode node = new TreeNode(acqImageName);

            TreeNode cameraNode = tvTasks.SelectedNode.Level == 2 ? tvTasks.SelectedNode.Parent : tvTasks.SelectedNode;
            cameraNode.Nodes.Add(node);

            tvTasks.SelectedNode = node;
            tvTasks.LabelEdit = true;
            node.BeginEdit();
        }

        private void tsmiDeleteAcqImage_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowQuestion("确定要删除该拍照吗？") == DialogResult.No) return;

            TreeNode acqImageNode = tvTasks.SelectedNode;
            string taskname = getTaskName(acqImageNode);

            taskRepo.DeleteAcqImage(taskname, getAcqImageName(acqImageNode));

            acqImageNode.Remove();
        }

        private void tsmiRenameTakePhone_Click(object sender, EventArgs e)
        {
            operation = Operation.RenameAcqImage;

            TreeNode node = tvTasks.SelectedNode;

            node.Text = node.Text.Split(ConnChar)[0];

            tvTasks.LabelEdit = true;
            node.BeginEdit();
        }

        #endregion

        #region 运行

        private void tsmiRunTask_Click(object sender, EventArgs e)
        {
            if (tvTasks.SelectedNode == null) return;

            if (!imageService.Running)
                imageService.Start();

            taskService.RunTask(tvTasks.SelectedNode.Text);
        }

        private void tsmiRunAcqImage_Click(object sender, EventArgs e)
        {
            if (tvTasks.SelectedNode == null) return;

            string taskName = getTaskName(tvTasks.SelectedNode);
            string acqImageName = getAcqImageName(tvTasks.SelectedNode);

            if (!imageService.Running)
                imageService.Start();

            taskService.RunAcqImage(taskName, acqImageName);
        }

        #endregion

        #region 光学设置

        // 光学设置
        private void tsmiOpticalSetting_Click(object sender, EventArgs e)
        {
            if (tvTasks.SelectedNode == null) return;

            showOpticalSetting(tvTasks.SelectedNode);
        }

        void showOpticalSetting(TreeNode node)
        {
            string taskName = getTaskName(node);
            string acqImageName = getAcqImageName(node);

            string text = Utils.GetOpticalFormName(taskName, acqImageName);

            FrmOpticalSetting form = (FrmOpticalSetting)formSettings.FindSubForm(text);
            if (form == null)
            {
                form = (FrmOpticalSetting)formMain.FindSubForm(text);
            }
            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmOpticalSetting>();
                form.Text = text;
                form.LoadOptics(taskName, acqImageName, null);
            }
            formSettings.ShowAloneForm(DockState.Document, form);
        }

        private void tsmiDeleteOpticalSetting_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowQuestion("确定要删除该光学设置吗？") == DialogResult.Yes)
            {
                string taskName = getTaskName(tvTasks.SelectedNode);
                string acqImageName = getAcqImageName(tvTasks.SelectedNode);

                opticsRepo.DeleteOpticsByAcqImage(taskName, acqImageName);
            }
        }

        #endregion

        #region 标定

        private void tsmiNewCalibration_Click(object sender, EventArgs e)
        {

        }

        // 添加标定
        private void TsmiNewCalibrationType_Click(object sender, EventArgs e)
        {
            if (tvTasks.SelectedNode == null) return;

            CalibrationType type = (CalibrationType)(sender as ToolStripMenuItem).Tag;

            string calibName;
            if (type == CalibrationType.Checkerboard)
            {
                operation = Operation.AddCheckerboard;

                calibName = getDefaultCheckerboardName();
            }
            else if (type == CalibrationType.HandEye)
            {
                operation = Operation.AddHandEye;

                calibName = getDefaultHandEyeName();
            }
            else if (type == CalibrationType.HandEyeSingle)
            {
                operation = Operation.AddHandEye1;

                calibName = getDefaultHandEyeName();
            }
            else if (type == CalibrationType.Joint)
            {
                operation = Operation.AddJoint;

                calibName = getDefalutJointName();
            }
            else
            {
                return;
            }

            TreeNode node = new TreeNode
            {
                Text = calibName,
                Tag = type
            };

            TreeNode acqImageNode = tvTasks.SelectedNode.Level == 2 ? tvTasks.SelectedNode : tvTasks.SelectedNode.Parent;
            acqImageNode.Nodes.Add(node);

            tvTasks.SelectedNode = node;
            tvTasks.LabelEdit = true;
            node.BeginEdit();
        }

        // 删除标定
        private void tsmiDeleteCalibration_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowQuestion("确定要删除该标定吗？") == DialogResult.No) return;

            string taskName = getTaskName(tvTasks.SelectedNode);
            string acqImageName = getAcqImageName(tvTasks.SelectedNode);
            CalibrationType calibType = (CalibrationType)tvTasks.SelectedNode.Tag;
            string calibName = tvTasks.SelectedNode.Text;

            taskRepo.DeleteCalibration(taskName, acqImageName, calibType, calibName);

            tvTasks.SelectedNode.Remove();

            if(calibRepo.GetCalibration(calibName) == null)
            {
                if (calibType == CalibrationType.Joint)
                {
                    List<TreeNode> nodes = tvTasks.Nodes.FindNodes(true, a => a.Text == calibName);
                    foreach (TreeNode tn in nodes)
                    {
                        tn.Remove();
                    }
                }
            }
        }

        // 重命名标定
        private void tsmiRenameCalibration_Click(object sender, EventArgs e)
        {
            operation = Operation.RenameCalibration;

            tvTasks.LabelEdit = true;
            tvTasks.SelectedNode.BeginEdit();
        }

        // 引用联合标定
        private void tsmiQuoteJointCalib_Click(object sender, EventArgs e)
        {
            string calibName = ((ToolStripMenuItem)sender).Text;
            string taskName = getTaskName(tvTasks.SelectedNode);
            string acqImageName = getAcqImageName(tvTasks.SelectedNode);

            taskRepo.QuoteCalibration(taskName, acqImageName, CalibrationType.Joint, calibName);

            TreeNode node = new TreeNode
            {
                Text = calibName,
                Tag = CalibrationType.Joint
            };

            TreeNode acqImageNode = tvTasks.SelectedNode.Level == 2 ? tvTasks.SelectedNode : tvTasks.SelectedNode.Parent;
            acqImageNode.Nodes.Add(node);

            tvTasks.SelectedNode = node;
        }

        FormSettings formSettings;
        FormMain formMain;

        // 显示标定窗口
        void showCalibration(TreeNode node)
        {
            string taskName = getTaskName(node);
            string acqName = getAcqImageName(node);
            string calibName = node.Text;
            CalibrationType calibType = (CalibrationType)node.Tag;

            string text = Utils.GetCalibrationFormName(taskName, calibName, calibType);

            Form form = formSettings.FindSubForm(text);
            if (form == null)
            {
                form = formMain.FindSubForm(text);
            }

            if (calibType == CalibrationType.Checkerboard)
            {
                FrmCheckerboardSetting frmCheckerboard;
                if (form == null)
                {
                    frmCheckerboard = AutoFacContainer.Resolve<FrmCheckerboardSetting>();
                    frmCheckerboard.Init(taskName, calibName);
                    frmCheckerboard.Text = text;
                }
                else
                {
                    frmCheckerboard = (FrmCheckerboardSetting)form;
                }
                frmCheckerboard.OpenOptical = OpenCalibrationOptical;
                formSettings.ShowAloneForm(DockState.Document, frmCheckerboard);
            }
            else if (calibType == CalibrationType.HandEye)
            {
                FrmHandEyeSetting frmHandEye;
                if (form == null)
                {
                    frmHandEye = AutoFacContainer.Resolve<FrmHandEyeSetting>();
                    frmHandEye.Init(taskName, acqName, calibName);
                    frmHandEye.Text = text;
                }
                else
                {
                    frmHandEye = (FrmHandEyeSetting)form;
                }
                frmHandEye.OpenOptical = OpenCalibrationOptical;
                formSettings.ShowAloneForm(DockState.Document, frmHandEye);
            }
            else if (calibType == CalibrationType.HandEyeSingle)
            {
                FrmHandEyeSingleSetting frmHandEyeSingle;
                if (form == null)
                {
                    frmHandEyeSingle = AutoFacContainer.Resolve<FrmHandEyeSingleSetting>();
                    frmHandEyeSingle.Init(taskName, acqName, calibName);
                    frmHandEyeSingle.Text = text;
                }
                else
                {
                    frmHandEyeSingle = (FrmHandEyeSingleSetting)form;
                }
                frmHandEyeSingle.OpenOptical = OpenCalibrationOptical;
                formSettings.ShowAloneForm(DockState.Document, frmHandEyeSingle);
            }
            else if (calibType == CalibrationType.Joint)
            {
                FrmJoint frmJoint;
                if (form == null)
                {
                    frmJoint = AutoFacContainer.Resolve<FrmJoint>();
                    if (frmJoint.Init(calibName))
                    {
                        frmJoint.Text = text;
                    }
                    else
                    {
                        frmJoint.Dispose();
                        frmJoint = null;
                        return;
                    }
                }
                else
                {
                    frmJoint = (FrmJoint)form;
                }

                formSettings.ShowAloneForm(DockState.Document, frmJoint);
            }
        }

        // 打开标定的光学设置
        private void OpenCalibrationOptical(string taskName, string calibName)
        {
            string text = $"{taskName}/{calibName}-光学设置";

            FrmOpticalSetting form = (FrmOpticalSetting)formSettings.FindSubForm(text);
            if (form == null)
            {
                form = (FrmOpticalSetting)formMain.FindSubForm(text);
            }
            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmOpticalSetting>();
                form.LoadOptics(taskName, null, calibName);
            }
            formSettings.ShowAloneForm(DockState.Document, form);
        }

        #endregion

        #region 移动/展开

        private void tsmiMoveUp_Click(object sender, EventArgs e)
        {
            TreeNode selNode = tvTasks.SelectedNode;

            if (selNode.Level == 0)
            {
                taskRepo.MoveUpTask(selNode.Index);

                tvTasks.MoveUpNode(selNode);
            }
            else if (selNode.Level == 2)
            {
                string taskName = getTaskName(selNode);

                taskRepo.MoveUpAcqImage(taskName, getAcqImageName(selNode), selNode.Index);

                tvTasks.MoveUpNode(selNode);
            }
        }

        private void tsmiMoveDown_Click(object sender, EventArgs e)
        {
            TreeNode selNode = tvTasks.SelectedNode;

            if (selNode.Level == 0)
            {
                taskRepo.MoveDownTask(selNode.Index);

                tvTasks.MoveDownNode(selNode);
            }
            else if (selNode.Level == 2)
            {
                string taskName = getTaskName(selNode);

                taskRepo.MoveDownAcqImage(taskName, getAcqImageName(selNode), selNode.Index);

                tvTasks.MoveDownNode(selNode);
            }
        }

        private void tsmiExpandAll_Click(object sender, EventArgs e)
        {
            tvTasks.ExpandAll();
        }

        private void tsmiCollapseAll_Click(object sender, EventArgs e)
        {
            tvTasks.CollapseAll();
        }

        #endregion

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            BuildCameraMenu();
        }

        private void tvTasks_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 2)
                showOpticalSetting(e.Node);
            else if (e.Node.Level == 3)
                showCalibration(e.Node);

            if (e.Node.Level == 0)
            {
                string taskName = getTaskName(e.Node);
                showToolBlockSetHandle?.Invoke(taskName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            taskRepo.Save();
        }

        enum Operation
        {
            None,

            AddVisionProTask,

            AddHyVisionTask,

            RenameTask,

            AddAcqImage,

            RenameAcqImage,

            AddCheckerboard,

            AddHandEye,

            AddHandEye1,

            AddJoint,

            RenameCalibration
        }

        #region 右键菜单显示

        private void cmsTaskTree_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TreeNode selnode = tvTasks.SelectedNode;
            if (selnode == null)
            {
                SetTaskMenu(true, false);
                SetCameraMenu(false);
                SetAcqImageMenu(false, false);
                SetCalibrationMenu(false, false);
                SetMoveMenu(false);
            }
            // 任务
            else if (selnode.Level == 0)
            {
                SetTaskMenu(true, true);
                SetCameraMenu(true);
                SetAcqImageMenu(false, false);
                SetCalibrationMenu(false, false);
                SetMoveMenu(true);
            }
            // 相机
            else if (selnode.Level == 1)
            {
                SetTaskMenu(false, false);
                SetCameraMenu(true);
                SetAcqImageMenu(true, false);
                SetCalibrationMenu(false, false);
                SetMoveMenu(false);
            }
            // 拍照
            else if (selnode.Level == 2)
            {
                SetTaskMenu(false, false);
                SetCameraMenu(false);
                SetAcqImageMenu(true, true);
                SetCalibrationMenu(true, false);
                SetMoveMenu(true);
            }
            // 标定
            else if (selnode.Level == 3)
            {
                SetTaskMenu(false, false);
                SetCameraMenu(false);
                SetAcqImageMenu(false, false);
                SetCalibrationMenu(true, true);
                SetMoveMenu(false);
            }
        }

        void SetTaskMenu(bool visible, bool enabled)
        {
            tsmiNewVisionProTask.Visible = visible;
            tsmiHyVisionTask.Visible = visible;
            tsmiBatchAddTask.Visible = visible;
            tsmiRenameTask.Visible = visible;
            tsmiDeleteTask.Visible = visible;
            tsmiRunTask.Visible = visible;

            toolStripSeparator1.Visible = visible;

            tsmiRenameTask.Enabled = enabled;
            tsmiDeleteTask.Enabled = enabled;
            tsmiRunTask.Enabled = visible;
        }

        void SetCameraMenu(bool visible)
        {
            tsmiAddCamera.Visible = visible;

            toolStripSeparator2.Visible = visible;
        }

        void SetAcqImageMenu(bool visible, bool enabled)
        {
            tsmiAddAcqImage.Visible = visible;
            tsmiRenameAcqImage.Visible = visible;
            tsmiDeleteAcqImage.Visible = visible;

            tsmiOpticalSetting.Visible = visible;
            tsmiDeleteOpticalSetting.Visible = visible;

            tsmiRunAcqImage.Visible = visible;

            toolStripSeparator3.Visible = visible;

            tsmiRenameAcqImage.Enabled = enabled;
            tsmiDeleteAcqImage.Enabled = enabled;

            tsmiOpticalSetting.Enabled = enabled;
            tsmiDeleteOpticalSetting.Enabled = enabled;
            tsmiRunAcqImage.Enabled = enabled;
        }

        void SetCalibrationMenu(bool visible, bool enabled)
        {
            tsmiNewCalibration.Visible = visible;
            tsmiDeleteCalibration.Visible = visible;
            tsmiRenameCalibration.Visible = visible;

            toolStripSeparator4.Visible = visible;

            tsmiDeleteCalibration.Enabled = enabled;
            tsmiRenameCalibration.Enabled = enabled;

            if (visible)
            {
                string taskName = getTaskName(tvTasks.SelectedNode);
                string acqImageName = getAcqImageName(tvTasks.SelectedNode);

                AcquireImageInfoVO acqImage = taskRepo.GetAcqImage(taskName, acqImageName);

                foreach (ToolStripMenuItem item in tsmiNewCalibration.DropDownItems)
                {
                    CalibrationType type = (CalibrationType)item.Tag;
                    if (type == CalibrationType.Checkerboard)
                    {
                        item.Enabled = string.IsNullOrEmpty(acqImage.CheckerboardName);
                    }
                    if (type == CalibrationType.Joint)
                    {
                        item.Enabled = string.IsNullOrEmpty(acqImage.JointName);
                    }
                }
            }
        }

        void SetMoveMenu(bool visible)
        {
            tsmiMoveUp.Visible = visible;
            tsmiMoveDown.Visible = visible;
        }

        #endregion

        private void FrmTaskSetting_VisibleChanged(object sender, EventArgs e)
        {
            tvTasks.ExpandAll();
        }

        //add by LuoDian @ 20210824 用于离线批量跑任务
        private void tsmiBatchRunTaskOffline_Click(object sender, EventArgs e)
        {
            if (tvTasks.SelectedNode == null) return;

            if (!imageService.Running)
                imageService.Start();

            taskService.RunTaskForBatchRunOffline(tvTasks.SelectedNode.Text);
        }
    }
}
