using Autofac;
using CameraSDK.Models;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using HyEye.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisionSDK;

namespace HyEye.WForm.Calibration
{
    public partial class FrmHandeyeVerify : Form
    {
        static readonly Color DefaultColor = SystemColors.Control;
        static readonly Color OkColor = Color.FromArgb(32, 255, 32);
        static readonly Color ErrorColor = Color.FromArgb(255, 32, 32);

        readonly string taskName;
        readonly string calibName;

        readonly ITaskRepository taskRepo;
        readonly IOpticsRepository opticsRepo;
        readonly ICalibrationVerifyRepository calibVerifyRepo;
        readonly ISimulationRepository simulationRepo;

        readonly IHandeyeComponent handeyeComponent;
        readonly IAcquireImage acqImage;
        readonly IGLog log;

        //add by LuoDian @ 20211214 用于子料号的快速切换
        readonly IMaterialRepository materialRepo;

        HandeyeVerifyInfoVO verifyInfo;
        ICalibVerifyComponent component;

        public FrmHandeyeVerify(
            string taskName,
            string calibName,
            ITaskRepository taskRepo,
            IOpticsRepository opticsRepo,
            ICalibrationVerifyRepository calibVerifyRepo,
            ISimulationRepository simulationRepo,
            IHandeyeComponent handeyeComponent,
            IAcquireImage acqImage,
            IMaterialRepository materialRepo,
            IGLog log)
        {
            InitializeComponent();

            dataGridView1.Columns[1].ValueType = typeof(double);
            dataGridView1.Columns[2].ValueType = typeof(double);
            dataGridView1.Columns[3].ValueType = typeof(double);

            this.taskName = taskName;
            this.calibName = calibName;

            this.taskRepo = taskRepo;
            this.opticsRepo = opticsRepo;
            this.calibVerifyRepo = calibVerifyRepo;
            this.simulationRepo = simulationRepo;

            this.handeyeComponent = handeyeComponent;
            this.acqImage = acqImage;
            this.log = log;

            //add by LuoDian @ 20211214 用于子料号的快速切换
            this.materialRepo = materialRepo;

            Text = calibName + " 标定验证";
        }

        private void FrmHandeyeVerify_Load(object sender, EventArgs e)
        {
            verifyInfo = calibVerifyRepo.GetVerifyInfo<HandeyeVerifyInfoVO>(calibName);
            if (verifyInfo == null)
                verifyInfo = new HandeyeVerifyInfoVO { CalibrationName = calibName };

            ckbRMS.Checked = verifyInfo.RmsEnabled;
            nudRmsTolerance.Value = (decimal)verifyInfo.RmsTolerance;

            ckbAspect.Checked = verifyInfo.AspectEnabled;
            nudAspectTolerance.Value = (decimal)verifyInfo.AspectTolerance;

            ckbSkew.Checked = verifyInfo.SkewEnabled;
            nudSkewTolerance.Value = (decimal)verifyInfo.SkewTolerance;

            ckbX.Checked = verifyInfo.XEnabled;
            nudXTheoreticalValue.Value = (decimal)verifyInfo.XTheoreticalValue;
            nudXTolerance.Value = (decimal)verifyInfo.XTolerance;

            ckbY.Checked = verifyInfo.YEnabled;
            nudYTheoreticalValue.Value = (decimal)verifyInfo.YTheoreticalValue;
            nudYTolerance.Value = (decimal)verifyInfo.YTolerance;

            ckbA.Checked = verifyInfo.AEnabled;
            nudATheoreticalValue.Value = (decimal)verifyInfo.ATheoreticalValue;
            nudATolerance.Value = (decimal)verifyInfo.ATolerance;

            component = AutoFacContainer.Resolve<ICalibVerifyComponent>(
                new NamedParameter("taskName", taskName),
                new NamedParameter("calibName", calibName),
                new NamedParameter("calibType", CalibrationType.HandEye));
            pnlToolBlock.Controls.Add(component.DisplayedControl);
        }

        private void ckbRMS_CheckedChanged(object sender, EventArgs e)
        {
            nudRmsTolerance.Enabled = ckbRMS.Checked;
        }

        private void ckbAspect_CheckedChanged(object sender, EventArgs e)
        {
            nudAspectTolerance.Enabled = ckbAspect.Checked;
        }

        private void ckbSkew_CheckedChanged(object sender, EventArgs e)
        {
            nudSkewTolerance.Enabled = ckbSkew.Checked;
        }

        private void ckbX_CheckedChanged(object sender, EventArgs e)
        {
            nudXTheoreticalValue.Enabled = ckbX.Checked;
            nudXTolerance.Enabled = ckbX.Checked;

            pnlToolBlock.Enabled = ckbX.Checked || ckbY.Checked || ckbA.Checked;
        }

        private void ckbY_CheckedChanged(object sender, EventArgs e)
        {
            nudYTheoreticalValue.Enabled = ckbY.Checked;
            nudYTolerance.Enabled = ckbY.Checked;

            pnlToolBlock.Enabled = ckbX.Checked || ckbY.Checked || ckbA.Checked;
        }

        private void ckbA_CheckedChanged(object sender, EventArgs e)
        {
            nudATheoreticalValue.Enabled = ckbA.Checked;
            nudATolerance.Enabled = ckbA.Checked;

            pnlToolBlock.Enabled = ckbX.Checked || ckbY.Checked || ckbA.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            verifyInfo.RmsEnabled = ckbRMS.Checked;
            verifyInfo.RmsTolerance = (double)nudRmsTolerance.Value;

            verifyInfo.AspectEnabled = ckbAspect.Checked;
            verifyInfo.AspectTolerance = (double)nudAspectTolerance.Value;

            verifyInfo.SkewEnabled = ckbSkew.Checked;
            verifyInfo.SkewTolerance = (double)nudSkewTolerance.Value;

            verifyInfo.XEnabled = ckbX.Checked;
            verifyInfo.XTheoreticalValue = (double)nudXTheoreticalValue.Value;
            verifyInfo.XTolerance = (double)nudXTolerance.Value;

            verifyInfo.YEnabled = ckbY.Checked;
            verifyInfo.YTheoreticalValue = (double)nudYTheoreticalValue.Value;
            verifyInfo.YTolerance = (double)nudYTolerance.Value;

            verifyInfo.AEnabled = ckbA.Checked;
            verifyInfo.ATheoreticalValue = (double)nudATheoreticalValue.Value;
            verifyInfo.ATolerance = (double)nudATolerance.Value;

            calibVerifyRepo.SetVerifyInfo(verifyInfo);
            calibVerifyRepo.Save();

            component.Save();
        }

        private void btnGetPoint_Click(object sender, EventArgs e)
        {
            Dictionary<string, double?> outputs = component.Run(handeyeComponent.OutputImage);

            dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1,
                outputs[InputOutputConst.Output_X],
                outputs[InputOutputConst.Output_Y],
                outputs[InputOutputConst.Output_A]);
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            pnlRMS.BackColor = DefaultColor;
            pnlAspect.BackColor = DefaultColor;
            pnlSkew.BackColor = DefaultColor;
            pnlX.BackColor = DefaultColor;
            pnlY.BackColor = DefaultColor;
            pnlA.BackColor = DefaultColor;

            if (ckbRMS.Checked)
            {
                verifyRms();
            }

            if (ckbAspect.Checked)
            {
                verifyAspect();
            }

            if (ckbSkew.Checked)
            {
                verifySkew();
            }

            if (ckbX.Checked || ckbY.Checked || ckbA.Checked)
                verifyXYA();
        }

        void verifyRms()
        {
            try
            {
                double rms = handeyeComponent.GetRMS();
                tbRmsActualValue.Text = rms.ToString();

                if (rms > (double)nudRmsTolerance.Value)
                {
                    pnlRMS.BackColor = ErrorColor;
                    log.Error($"[{calibName}]标定验证失败，RMS = {rms} 超出公差允许范围");
                }
                else
                {
                    pnlRMS.BackColor = OkColor;
                }
            }
            catch
            {
                tbRmsActualValue.Text = string.Empty;
                pnlRMS.BackColor = ErrorColor;

                log.Error($"[{calibName}]获取 RMS 失败");
            }
        }

        void verifyAspect()
        {
            try
            {
                double aspect = handeyeComponent.GetAspect();
                tbAspectActualValue.Text = aspect.ToString();

                double tolerance = Math.Abs(aspect - verifyInfo.AspectTheoreticalValue);

                if (tolerance > (double)nudAspectTolerance.Value)
                {
                    pnlAspect.BackColor = ErrorColor;
                    log.Error($"[{calibName}]标定验证失败，纵横比 = {aspect} 超出公差允许范围");
                }
                else
                {
                    pnlAspect.BackColor = OkColor;
                }
            }
            catch
            {
                tbAspectActualValue.Text = string.Empty;
                pnlAspect.BackColor = ErrorColor;

                log.Error($"[{calibName}]获取纵横比失败");
            }
        }

        void verifySkew()
        {
            try
            {
                double skew = handeyeComponent.GetSkew();
                tbSkewActualValue.Text = skew.ToString();

                double tolerance = Math.Abs(skew - verifyInfo.SkewTheoreticalValue);

                if (tolerance > (double)nudSkewTolerance.Value)
                {
                    pnlSkew.BackColor = ErrorColor;
                    log.Error($"[{calibName}]标定验证失败，倾斜 = {skew} 超出公差允许范围");
                }
                else
                {
                    pnlSkew.BackColor = OkColor;
                }
            }
            catch
            {
                tbSkewActualValue.Text = string.Empty;
                pnlSkew.BackColor = ErrorColor;

                log.Error($"[{calibName}]获取倾斜失败");
            }
        }

        void verifyXYA()
        {
            if (dataGridView1.Rows.Count < 2)
            {
                log.Error($"[{calibName}]标定验证失败，机械手走位坐标不足");
                return;
            }

            if (ckbX.Checked && checkNotEmpty(1))
            {
                pnlX.BackColor = verify(1, (double)nudXTheoreticalValue.Value, (double)nudXTolerance.Value) ? OkColor : ErrorColor;
            }

            if (ckbY.Checked && checkNotEmpty(2))
            {
                pnlY.BackColor = verify(2, (double)nudYTheoreticalValue.Value, (double)nudYTolerance.Value) ? OkColor : ErrorColor;
            }

            if (ckbA.Checked && checkNotEmpty(3))
            {
                pnlA.BackColor = verify(3, (double)nudATheoreticalValue.Value, (double)nudATolerance.Value) ? OkColor : ErrorColor;
            }
        }

        bool verify(int colIndex, double theoreticalValue, double tolerance)
        {
            bool result = true;

            for (int step = 1, rowCount = dataGridView1.Rows.Count; step < rowCount; step++)
            {
                for (int rowIndex1 = 0; ; rowIndex1++)
                {
                    int rowIndex2 = rowIndex1 + step;
                    if (rowIndex2 >= rowCount) break;

                    double v1 = (double)dataGridView1.Rows[rowIndex1].Cells[colIndex].Value;
                    double v2 = (double)dataGridView1.Rows[rowIndex2].Cells[colIndex].Value;

                    double v = v2 - v1;

                    double diff = Math.Abs(v - theoreticalValue * step);

                    if (diff > tolerance)
                    {
                        result = false;
                        log.Error($"[{calibName}]标定验证失败，{dataGridView1.Columns[colIndex].HeaderText} 坐标从 {v1} 到 {v2} 超出公差允许范围");
                    }
                }
            }

            return result;
        }

        bool checkNotEmpty(int colIndex)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[colIndex].Value == null)
                {
                    log.Error("坐标集合中包含空数据，无法验证");
                    return false;
                }
            }
            return true;
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;

            dataGridView1.Rows.RemoveAt(index);

            for (int i = index; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void tsmiDeleteAll_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tsmiDelete.Enabled = dataGridView1.CurrentCell != null;
        }

        private void btnAcqImage_Click(object sender, EventArgs e)
        {
            // Modified by louis on January 25, 2022, solve the problem that the camera is not turned off due to abnormal code
            try
            {
                TaskInfoVO taskInfo = taskRepo.GetTaskByName(taskName);

                if (!acqImage.CheckCamera(taskInfo)) return;

                acqImage.Start(taskInfo);

                int errCode;
                CameraImage cameraImage;

	            if (simulationRepo.Enabled)
	            {
	                errCode = acqImage.GetImageFromLocal(calibName, out cameraImage);
	            }
	            else
	            {
	                OpticsInfoVO opticsInfo = opticsRepo.GetOptics(taskInfo.Name, null, calibName, materialRepo.CurSubName);

	                //add by LuoDian @ 20220111 为了提升软触发的效率，把触发设置和获取图像数据分开，以便在触发完成后，立马返回信号响应，不必等图像数据输出
	                errCode = acqImage.Trigger(opticsInfo);
	                if (errCode != ErrorCodeConst.OK)
	                {
	                    MessageBoxUtils.ShowError("触发设置失败");
	                    return;
	                }

	                //update by LuoDian 在把触发与获取图像数据分开之后，已经不需要拍照信息对象OpticsInfoVO了
	                errCode = acqImage.GetImageFromCamera(out cameraImage);
	            }

                if (errCode == ErrorCodeConst.OK)
                    component.SetInputImage(cameraImage.Bitmap, cameraImage.IsGrey);
                else
                    MessageBoxUtils.ShowError("取像失败");
            }
            catch(Exception ex)
            {
                log.Error("取像失败, 错误信息：{0}", ex);
            }
            finally
            {
                acqImage.Close();
            }
        }
    }
}
