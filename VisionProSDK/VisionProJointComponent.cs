using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace VisionSDK._VisionPro
{
    public class VisionProJointComponent : IJointComponent
    {
        readonly CalibrationInfoVO calibInfo;
        readonly IBasicRepository basicRepo;
        readonly IPathRepository pathRepo;
        readonly IGLog log;

        Dictionary<string, IToolBlockComponent> toolBlocks;
        List<JointControl> m_controls;

        public VisionProJointComponent(
            CalibrationInfoVO calibInfo,
            Dictionary<string, IToolBlockComponent> toolBlocks,
            IBasicRepository basicRepo,
            IPathRepository pathRepo,
            IGLog log)
        {
            this.calibInfo = calibInfo;
            this.basicRepo = basicRepo;
            this.pathRepo = pathRepo;
            this.log = log;
            this.toolBlocks = toolBlocks;

            m_controls = new List<JointControl>
            {
                GetJointControl(calibInfo.JointInfo.Master, true)
            };
            foreach (TaskAcqImageVO taskAcq in calibInfo.JointInfo.Slaves)
            {
                m_controls.Add(GetJointControl(taskAcq, false));
            }
        }

        public void AddQuote(string taskName, string acqImageName, IToolBlockComponent toolBlock)
        {
            toolBlocks[taskName] = toolBlock;

            m_controls.Add(GetJointControl(new TaskAcqImageVO { TaskName = taskName, AcqImageName = acqImageName }, false));
        }

        JointControl GetJointControl(TaskAcqImageVO taskAcq, bool isMaster)
        {
            JointControl_VP jc = new JointControl_VP
            {
                TaskName = taskAcq.TaskName,
                AcqName = taskAcq.AcqImageName
            };

            string vppPath = getVppPath(taskAcq.TaskName, taskAcq.AcqImageName);

            CogToolBlock toolBlock = File.Exists(vppPath) ? LoadToolBlock(vppPath, taskAcq) : CreateToolBlock(taskAcq);
            jc.Subject = toolBlock;

            if (!isMaster)
            {
                string calibToolName = $"{calibInfo.Name}_{taskAcq.AcqImageName}";

                IToolBlockComponent taskToolBlock = toolBlocks[taskAcq.TaskName];

                object calibTool = taskToolBlock.GetTool(calibToolName);

                if (calibTool == null)
                {
                    taskToolBlock.AddCalibration(CalibrationType.Joint, calibToolName);
                    calibTool = taskToolBlock.GetTool(calibToolName);
                }

                jc.NPointToNPoint = (CogCalibNPointToNPointTool)calibTool;
            }

            return jc;
        }

        string getVppPath(string taskName, string acqName)
        {
            return pathRepo.GetJointVppPath(calibInfo.Name, taskName, acqName, TaskType.VP);
        }

        CogToolBlock LoadToolBlock(string vppPath, TaskAcqImageVO taskAcq)
        {
            try
            {
                return (CogToolBlock)CogSerializer.LoadObjectFromFile(vppPath);
            }
            catch (Exception e)
            {
                log.Error(new CalibVisionLogMessage(calibInfo.Name, $"{taskAcq.TaskName}-{taskAcq.AcqImageName}",
                    A_LoadVpp, R_Fail, $"加载 VPP 文件 \"{vppPath}\" 出错，{e.Message}"));

                return null;
            }
        }

        CogToolBlock CreateToolBlock(TaskAcqImageVO taskAcq)
        {
            CogToolBlock toolBlock = new CogToolBlock();

            toolBlock.AddInput(new ParamInfo { Name = "Img1", Type = typeof(ICogImage) });

            Type doubleType = typeof(double);

            for (int i = 1; i <= calibInfo.JointInfo.PointCount; i++)
            {
                toolBlock.AddOutput(new ParamInfo { Name = "X" + i.ToString(), Type = doubleType });
                toolBlock.AddOutput(new ParamInfo { Name = "Y" + i.ToString(), Type = doubleType });
            }

            return toolBlock;
        }

        public List<JointControl> Controls
        {
            get { return m_controls; }
        }

        public (int errCode, LinkedDictionary<string, object>) Run(string taskName, string acqName, object img, bool isGrey)
        {
            JointControl_VP control = (JointControl_VP)m_controls.FirstOrDefault(a => a.TaskName == taskName && a.AcqName == acqName);

            CogToolBlock toolBlock = control.Subject;

            if (img is ICogImage cogImage)
            {
            }
            else if (img is Bitmap bmp)
            {
                cogImage = VisionProUtils.ToCogImage(bmp, isGrey);
            }
            else
            {
                return (ErrorCodeConst.AcqImageError, null);
            }

            toolBlock.SetInputValue("Img1", cogImage);

            toolBlock.Run();

            LinkedDictionary<string, object> outputs = toolBlock.GetOutputValues();

            ICogRecord record = toolBlock.CreateLastRunRecord();
            if (record != null && record.SubRecords.Count > 0)
            {
                record = record.SubRecords[0];
            }
            outputs.Add("Record", record);

            if (control.NPointToNPoint != null)
            {
                control.NPointToNPoint.InputImage = cogImage;
                control.NPointToNPoint.CalibrationImage = cogImage;
            }

            return (ErrorCodeConst.OK, outputs);
        }

        public void Calibration(string taskName, string acqName, List<(double X1, double Y1, double X2, double Y2)> points)
        {
            JointControl_VP control = (JointControl_VP)m_controls.FirstOrDefault(a => a.TaskName == taskName && a.AcqName == acqName);

            control.NPointToNPoint.Calibration(points);
        }

        public void Save()
        {
            foreach (JointControl jc in m_controls)
            {
                JointControl_VP c = (JointControl_VP)jc;

                string path = getVppPath(c.TaskName,c.AcqName);

                VisionProUtils.SaveSubject(c.Subject, path, basicRepo.VPPExcludeDataBindings);

                log.Info(new CalibVisionLogMessage(calibInfo.Name, $"{c.TaskName}-{c.AcqName}", A_Save, R_Success, $"保存至“{path}”"));
            }

            foreach (IToolBlockComponent toolBlock in toolBlocks.Values)
            {
                toolBlock.Save();
            }
        }
    }

    public class JointControl_VP : JointControl
    {
        public CogToolBlock Subject { get; set; }

        CogToolBlockEditV2 toolBlockEdit;

        public override Control DisplayedControl
        {
            get
            {
                if (toolBlockEdit == null || toolBlockEdit.IsDisposed)
                {
                    toolBlockEdit = new CogToolBlockEditV2
                    {
                        Subject = Subject,
                        Dock = DockStyle.Fill
                    };
                }

                return toolBlockEdit;
            }
        }

        public CogCalibNPointToNPointTool NPointToNPoint { get; set; }

        CogCalibNPointToNPointEditV2 nPointToNPointEdit;

        public Control NPointToNPointControl
        {
            get
            {
                if (nPointToNPointEdit == null || nPointToNPointEdit.IsDisposed)
                {
                    nPointToNPointEdit = new CogCalibNPointToNPointEditV2
                    {
                        Subject = NPointToNPoint
                    };
                }

                return nPointToNPointEdit;
            }
        }
    }
}
