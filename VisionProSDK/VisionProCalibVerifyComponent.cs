using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace VisionSDK._VisionPro
{
    public class VisionProCalibVerifyComponent : ICalibVerifyComponent
    {
        readonly IGLog log;
        readonly IPathRepository pathRepo;

        CogToolBlockEditV2 toolBlockEdit;
        CogToolBlock toolBlock;
        string vppPath;

        string taskName;
        string calibName;

        static readonly Type cogImageType = typeof(ICogImage);
        static readonly Type doubleType = typeof(double);
        const string InputImageName = "Img1";

        CalibrationType calibType;

        public Control DisplayedControl
        {
            get
            {
                if (toolBlockEdit == null)
                {
                    toolBlockEdit = new CogToolBlockEditV2
                    {
                        Dock = DockStyle.Fill,
                        Subject = toolBlock
                    };
                    toolBlockEdit.SubjectChanged += (sender, e) =>
                    {
                        toolBlock = toolBlockEdit.Subject;
                    };
                }

                return toolBlockEdit;
            }
        }

        public VisionProCalibVerifyComponent(
            string taskName,
            string calibName,
            CalibrationType calibType,
            IPathRepository pathRepo,
            IGLog log)
        {
            this.taskName = taskName;
            this.calibName = calibName;

            this.pathRepo = pathRepo;
            this.log = log;

            this.calibType = calibType;

            vppPath = pathRepo.GetCalibVerifyVppPath(calibName);
            if (File.Exists(vppPath))
            {
                try
                {
                    toolBlock = (CogToolBlock)CogSerializer.LoadObjectFromFile(vppPath);
                }
                catch (Exception e)
                {
                    log.Error(new CalibVisionLogMessage(taskName, calibName, A_LoadVpp, R_Fail, $"加载标定验证文件 \"{vppPath}\" 出错，{e.Message}"));
                }
            }
            else
            {
                toolBlock = new CogToolBlock();

                CogToolBlockTerminal input = new CogToolBlockTerminal(InputImageName, cogImageType);
                toolBlock.Inputs.Add(input);

                if (calibType == CalibrationType.Checkerboard)
                {
                    toolBlock.Outputs.Add(new CogToolBlockTerminal(InputOutputConst.Output_Distance, doubleType));
                }
                else
                {
                    toolBlock.Outputs.Add(new CogToolBlockTerminal(InputOutputConst.Output_X, doubleType));
                    toolBlock.Outputs.Add(new CogToolBlockTerminal(InputOutputConst.Output_Y, doubleType));
                    toolBlock.Outputs.Add(new CogToolBlockTerminal(InputOutputConst.Output_A, doubleType));
                }
            }
        }

        public Dictionary<string, double?> Run(object img)
        {
            if (!toolBlock.Inputs.Contains(InputImageName))
                throw new VisionException($"验证失败，Inputs 中缺失 [{InputImageName}]");

            if (calibType == CalibrationType.Checkerboard)
            {
                if (!toolBlock.Outputs.Contains(InputOutputConst.Output_Distance))
                    throw new VisionException($"验证失败，Outputs 中缺失 [{InputOutputConst.Output_Distance}]");
            }
            else
            {
                if (!toolBlock.Outputs.Contains(InputOutputConst.Output_X))
                    throw new VisionException($"验证失败，Outputs 中缺失 [{InputOutputConst.Output_X}]");
                if (!toolBlock.Outputs.Contains(InputOutputConst.Output_Y))
                    throw new VisionException($"验证失败，Outputs 中缺失 [{InputOutputConst.Output_Y}]");
                if (!toolBlock.Outputs.Contains(InputOutputConst.Output_A))
                    throw new VisionException($"验证失败，Outputs 中缺失 [{InputOutputConst.Output_A}]");
            }

            try
            {
                toolBlock.Inputs[InputImageName].Value = (ICogImage)img;

                toolBlock.Run();
            }
            catch (Exception e)
            {
                throw new VisionException($"ToolBlock 运行失败，" + e.Message);
            }

            Dictionary<string, double?> result = new Dictionary<string, double?>();
            if (toolBlock.Outputs.Contains(InputOutputConst.Output_Distance))
            {
                result.Add(InputOutputConst.Output_Distance, (double?)toolBlock.Outputs[InputOutputConst.Output_Distance].Value);
            }
            else
            {
                result.Add(InputOutputConst.Output_X, (double?)toolBlock.Outputs[InputOutputConst.Output_X].Value);
                result.Add(InputOutputConst.Output_Y, (double?)toolBlock.Outputs[InputOutputConst.Output_Y].Value);
                result.Add(InputOutputConst.Output_A, (double?)toolBlock.Outputs[InputOutputConst.Output_A].Value);
            }

            return result;
        }

        public void SetInputImage(Bitmap bmp, bool isGrey)
        {
            toolBlock.Inputs[InputImageName].Value = VisionProUtils.ToCogImage(bmp, isGrey);
        }

        public void Dispose()
        {
            toolBlockEdit?.Dispose();
            toolBlock?.Dispose();
        }

        public void Save()
        {
            Directory.CreateDirectory(pathRepo.CalibrationVppDirectory);

            CogSerializer.SaveObjectToFile(toolBlock, vppPath);
            log.Info(new CalibVisionLogMessage(taskName, calibName, A_Save, R_Success, $"标定验证 VPP 保存至“{vppPath}”"));
        }
    }
}
