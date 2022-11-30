using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.PMAlign;
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
using VisionSDK._VisionPro.Extension;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace VisionSDK._VisionPro
{
    public class VisionProHandeyeSingleComponent : IHandeyeSingleComponent
    {
        class ToolBlockComponent
        {
            CogToolBlockEditV2 toolBlockEdit;
            CogToolBlock toolBlock;

            public CalibrationInfoVO CalibInfo { get; set; }

            readonly IBasicRepository basicRepo;
            readonly IPathRepository pathRepo;
            readonly IGLog log;

            public ToolBlockComponent(
                CalibrationInfoVO calibInfo,
                IBasicRepository basicRepo,
                IPathRepository pathRepo,
                IGLog log)
            {
                this.CalibInfo = calibInfo;
                this.basicRepo = basicRepo;
                this.pathRepo = pathRepo;
                this.log = log;

                string patternPath = pathRepo.GetCalibPatternPath(calibInfo.Name);
                if (File.Exists(patternPath))
                {
                    try
                    {
                        toolBlock = CogSerializer.LoadObjectFromFile(patternPath) as CogToolBlock;
                    }
                    catch (Exception e)
                    {
                        log.Error(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_LoadVpp, R_Fail, $"加载标定模板文件 \"{patternPath}\" 出错，{e.Message}"));
                    }
                }

                if (toolBlock == null)
                {
                    toolBlock = new CogToolBlock();

                    InitToolBlock();
                }
            }

            void InitToolBlock()
            {
                toolBlock.AddInput("Img1", typeof(ICogImage));

                toolBlock.AddOutputs(new string[] {
                    "X1", "Y1",
                    "X2", "Y2",
                    "X3", "Y3",
                    "X4", "Y4",
                    "X5", "Y5",
                    "X6", "Y6",
                    "X7", "Y7",
                    "X8", "Y8",
                    "X9", "Y9",
                }, typeof(double));
            }

            public Control Control
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

            public void SetImage(ICogImage cogImage)
            {
                toolBlock.Inputs["Img1"].Value = cogImage;
            }

            public Dictionary<string, object> Run(ICogImage cogImage)
            {
                toolBlock.Inputs["Img1"].Value = cogImage;
                toolBlock.Run();

                Dictionary<string, object> outputs = new Dictionary<string, object>()
                {
                    { "X1", toolBlock.Outputs["X1"].Value },
                    { "Y1", toolBlock.Outputs["Y1"].Value },
                    { "X2", toolBlock.Outputs["X2"].Value },
                    { "Y2", toolBlock.Outputs["Y2"].Value },
                    { "X3", toolBlock.Outputs["X3"].Value },
                    { "Y3", toolBlock.Outputs["Y3"].Value },
                    { "X4", toolBlock.Outputs["X4"].Value },
                    { "Y4", toolBlock.Outputs["Y4"].Value },
                    { "X5", toolBlock.Outputs["X5"].Value },
                    { "Y5", toolBlock.Outputs["Y5"].Value },
                    { "X6", toolBlock.Outputs["X6"].Value },
                    { "Y6", toolBlock.Outputs["Y6"].Value },
                    { "X7", toolBlock.Outputs["X7"].Value },
                    { "Y7", toolBlock.Outputs["Y7"].Value },
                    { "X8", toolBlock.Outputs["X8"].Value },
                    { "Y8", toolBlock.Outputs["Y8"].Value },
                    { "X9", toolBlock.Outputs["X9"].Value },
                    { "Y9", toolBlock.Outputs["Y9"].Value },
                };

                ICogRecord record = toolBlock.CreateLastRunRecord();
                if (record == null || record.SubRecords.Count == 0)
                    outputs.Add("Graphic", null);
                else
                    outputs.Add("Graphic", record.SubRecords[0]);

                return outputs;
            }

            public void Save()
            {
                Directory.CreateDirectory(pathRepo.CalibrationVppDirectory);

                string path = pathRepo.GetCalibPatternPath(CalibInfo.Name);

                VisionProUtils.SaveSubject(toolBlock, path, basicRepo.VPPExcludeDataBindings);

                log.Info(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_Save, R_Success, $"保存标定模板 VPP：{path}"));
            }
        }

        class PMAlignComponent : IDisposable
        {
            public CalibrationInfoVO CalibInfo { get; set; }

            CogPMAlignEditV2 pMAlignEdit;
            CogPMAlignTool pMAlignTool;

            readonly IBasicRepository basicRepo;
            readonly IPathRepository pathRepo;
            readonly IGLog log;

            public PMAlignComponent(
                CalibrationInfoVO calibInfo,
                IBasicRepository basicRepo,
                IPathRepository pathRepo,
                IGLog log)
            {
                this.CalibInfo = calibInfo;
                this.basicRepo = basicRepo;
                this.pathRepo = pathRepo;
                this.log = log;

                string patternPath = pathRepo.GetCalibPatternPath(calibInfo.Name);
                if (File.Exists(patternPath))
                {
                    try
                    {
                        pMAlignTool = CogSerializer.LoadObjectFromFile(patternPath) as CogPMAlignTool;
                    }
                    catch (Exception e)
                    {
                        log.Error(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_LoadVpp, R_Fail, $"加载标定模板文件 \"{patternPath}\" 出错，{e.Message}"));
                    }
                }

                if (pMAlignTool == null)
                {
                    pMAlignTool = new CogPMAlignTool();
                }
            }

            public Control Control
            {
                get
                {
                    if (pMAlignEdit == null)
                    {
                        pMAlignEdit = new CogPMAlignEditV2
                        {
                            Dock = DockStyle.Fill,
                            Subject = pMAlignTool
                        };
                        pMAlignEdit.SubjectChanged += (sender, e) =>
                        {
                            pMAlignTool = pMAlignEdit.Subject;
                        };
                    }

                    return pMAlignEdit;
                }
            }

            public void SetImage(ICogImage cogImage) => pMAlignTool.InputImage = cogImage;

            public (CogTransform2DLinear Pose, CogCompositeShape Shape) Run(ICogImage cogImage)
            {
                pMAlignTool.InputImage = cogImage;
                pMAlignTool.Run();

                if (pMAlignTool.Results == null || pMAlignTool.Results.Count == 0)
                    return (null, null);

                CogTransform2DLinear pose = pMAlignTool.Results[0].GetPose();

                CogCompositeShape shape = pMAlignTool.Results?[0].CreateResultGraphics(CogPMAlignResultGraphicConstants.All);

                return (pose, shape);
            }

            public void Save()
            {
                Directory.CreateDirectory(pathRepo.CalibrationVppDirectory);

                string pMAlignPath = pathRepo.GetCalibPatternPath(CalibInfo.Name);

                VisionProUtils.SaveSubject(pMAlignTool, pMAlignPath, basicRepo.VPPExcludeDataBindings);

                log.Info(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_Save, R_Success, $"保存标定模板 VPP：{pMAlignPath}"));
            }

            public void Dispose()
            {
                pMAlignTool.Dispose();
                pMAlignEdit?.Dispose();
            }
        }

        class NPointToNPointComponent : IDisposable
        {
            public CalibrationInfoVO CalibInfo { get; set; }

            CogCalibNPointToNPointEditV2 nPointToNPointEdit;
            readonly CogCalibNPointToNPointTool nPointToNPoint;

            readonly IPathRepository pathRepo;
            readonly IGLog log;

            public NPointToNPointComponent(
                CalibrationInfoVO calibInfo,
                CogCalibNPointToNPointTool nPointToNPoint,
                IPathRepository pathRepo,
                IGLog log)
            {
                this.CalibInfo = calibInfo;
                this.nPointToNPoint = nPointToNPoint;
                this.pathRepo = pathRepo;
                this.log = log;
            }

            public Control Control
            {
                get
                {
                    if (nPointToNPointEdit == null)
                    {
                        nPointToNPointEdit = new CogCalibNPointToNPointEditV2
                        {
                            Dock = DockStyle.Fill,
                            Subject = nPointToNPoint
                        };
                    }
                    return nPointToNPointEdit;
                }
            }

            public void SetInputImage(ICogImage cogImage)
            {
                nPointToNPoint.CalibrationImage = cogImage;
                nPointToNPoint.InputImage = cogImage;
            }

            public CogPointMarker[] ToPhysicalPoint(CogPointMarker[] ImagePoint)
            {
                CogPointMarker[] PhysicalPoint = new CogPointMarker[ImagePoint.Length];

                for (int i = 0; i < ImagePoint.Length; i++)
                {
                    PhysicalPoint[i] = ImagePoint[i].MapLinear(
                        nPointToNPoint.Calibration.GetComputedUncalibratedFromRawCalibratedTransform().InvertBase() as CogTransform2DLinear,
                        CogCopyShapeConstants.All);
                }

                return PhysicalPoint;
            }

            public ICogImage GetOutputImage() => nPointToNPoint.OutputImage;

            public void Dispose()
            {
                nPointToNPointEdit?.Dispose();
                nPointToNPoint?.Dispose();
            }

            int PointPairCount = 0;

            public void Calibrate(List<HandeyeSinglePoint> points)
            {
                ClearPointPair();

                SetPointPair(points);

                nPointToNPoint.Calibration.DOFsToCompute = CogNPointToNPointDOFConstants.ScalingAspectRotationSkewAndTranslation;

                nPointToNPoint.Calibration.Calibrate();
            }

            public ICogImage Run(ICogImage cogImage)
            {
                nPointToNPoint.CalibrationImage = cogImage;
                nPointToNPoint.InputImage = cogImage;
                nPointToNPoint.Run();
                return nPointToNPoint.OutputImage;
            }

            void ClearPointPair()
            {
                while (PointPairCount > 0)
                {
                    nPointToNPoint.Calibration.DeletePointPair(--PointPairCount);
                }
            }

            void SetPointPair(List<HandeyeSinglePoint> points)
            {
                nPointToNPoint.Calibration.NumPoints = points.Where(a => a.Disable == false).Count();

                int i = 0;

                foreach (HandeyeSinglePoint p in points)
                {
                    if (points[i].Disable) continue;

                    nPointToNPoint.Calibration.SetUncalibratedPoint(i, p.X1, p.Y1);
                    nPointToNPoint.Calibration.SetRawCalibratedPoint(i, p.X2.Value, p.Y2.Value);
                    i++;

                    PointPairCount++;
                }
            }

            const string TxtValue = "txtValue";

            public double GetRMS()
            {
                return nPointToNPoint.Calibration.ComputedRMSError;
            }

            public double GetAspect()
            {
                CogCalibNPointToNPointEditV2 edit = (CogCalibNPointToNPointEditV2)Control;

                string str = edit.Controls.Find(TxtValue, true)[5].Text;
                if (str != null && double.TryParse(str, out double aspect))
                    return aspect;
                else
                    throw new VisionException("获取纵横比失败");
            }

            public double GetSkew()
            {
                CogCalibNPointToNPointEditV2 edit = (CogCalibNPointToNPointEditV2)Control;

                string str = edit.Controls.Find(TxtValue, true)[3].Text;
                if (str != null && double.TryParse(str, out double aspect))
                    return aspect;
                else
                    throw new VisionException("获取倾斜值失败");
            }
        }

        CalibrationInfoVO calibInfo;

        ToolBlockComponent _toolBlockComponent;
        ToolBlockComponent toolBlockComponent
        {
            get
            {
                if (_toolBlockComponent == null)
                    _toolBlockComponent = new ToolBlockComponent(calibInfo, basicRepo, pathRepo, log);

                return _toolBlockComponent;
            }
        }

        PMAlignComponent _pMAlignComponent;
        PMAlignComponent pMAlignComponent
        {
            get
            {
                if (_pMAlignComponent == null)
                    _pMAlignComponent = new PMAlignComponent(calibInfo, basicRepo, pathRepo, log);

                return _pMAlignComponent;
            }
        }

        readonly NPointToNPointComponent nPointToNPointComponent;

        readonly IToolBlockComponent toolBlock;
        readonly ICalibrationRepository calibRepo;
        readonly IBasicRepository basicRepo;
        readonly IPathRepository pathRepo;
        readonly IGLog log;

        static readonly Dictionary<string, object> ErrOutputs = new Dictionary<string, object>
        {
             {  "X",  999 },
             {  "Y",  999 },
             {  "A",  999 }
        };

        public VisionProHandeyeSingleComponent(
            CalibrationInfoVO calibInfo,
            IToolBlockComponent toolBlock,
            ICalibrationRepository calibRepo,
            IBasicRepository basicRepo,
            IPathRepository pathRepo,
            IGLog log)
        {
            this.calibInfo = calibInfo;
            this.calibRepo = calibRepo;
            this.basicRepo = basicRepo;
            this.pathRepo = pathRepo;
            this.log = log;

            this.toolBlock = toolBlock;

            if (calibInfo.HandEyeInfo == null)
                calibInfo.HandEyeInfo = new HandEyeInfoVO();

            nPointToNPointComponent = new NPointToNPointComponent(calibInfo, (CogCalibNPointToNPointTool)toolBlock.GetTool(calibInfo.Name), pathRepo, log);
        }

        public Control PatternControl
        {
            get
            {
                calibInfo = calibRepo.GetCalibration(calibInfo.Name);
                if (calibInfo.HandEyeSingleInfo.PMAlignOrToolBlock)
                    return pMAlignComponent.Control;
                else
                    return toolBlockComponent.Control;
            }
        }

        public Control NPointToNPointControl
        {
            get { return nPointToNPointComponent.Control; }
        }

        public void RenameCalibration(string newName)
        {
            calibInfo.Name = newName;
        }

        public void SetPatternImage(object img, bool isGrey)
        {
            ICogImage cogImage;

            if (img is Bitmap bmp)
            {
                cogImage = VisionProUtils.ToCogImage(bmp, isGrey);
            }
            else if (img is ICogImage cogImg)
            {
                cogImage = cogImg;
            }
            else
                return;

            if (calibInfo.HandEyeInfo.PMAlignOrToolBlock)
                pMAlignComponent.SetImage(cogImage);
            else
                toolBlockComponent.SetImage(cogImage);
        }

        // 在 S 指令中执行
        public int Start()
        {
            calibInfo = calibRepo.GetCalibration(calibInfo.Name);
            if (calibInfo.HandEyeSingleInfo.PMAlignOrToolBlock)
                pMAlignComponent.CalibInfo = calibInfo;
            else
                toolBlockComponent.CalibInfo = calibInfo;

            nPointToNPointComponent.CalibInfo = calibInfo;

            log.Info(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_Calibration, R_Start));

            return ErrorCodeConst.OK;
        }

        public (int errCode, Dictionary<string, object>) Run(object img, bool isGrey)
        {
            ICogImage cogImage;

            if (img is Bitmap bmp)
            {
                cogImage = VisionProUtils.ToCogImage(bmp, isGrey);
            }
            else if (img is ICogImage cogImg)
            {
                cogImage = cogImg;
            }
            else
            {
                return (ErrorCodeConst.CalibrateFail, null);
            }

            nPointToNPointComponent.SetInputImage(cogImage);

            Dictionary<string, object> outputs;

            if (calibInfo.HandEyeSingleInfo.PMAlignOrToolBlock)
            {
                var (Pose, Shape) = pMAlignComponent.Run(cogImage);

                if (img is Bitmap bmp1)
                {
                    bmp1.Dispose();
                }

                if (Pose == null)
                    return (ErrorCodeConst.HandeyeNotFindPattern, null);

                outputs = new Dictionary<string, object>
                {
                     {  "X",  Pose.TranslationX },
                     {  "Y",  Pose.TranslationY },
                     {  "A",  CogMisc.RadToDeg(Pose.Rotation) },
                     { "Graphic",  Shape }
                };
            }
            else
            {
                outputs = toolBlockComponent.Run(cogImage);

                if (img is Bitmap bmp1)
                {
                    bmp1.Dispose();
                }
            }

            return (ErrorCodeConst.OK, outputs);
        }

        public object RunNP(object img, bool isGrey)
        {
            ICogImage cogImage = null;

            if (img is Bitmap bmp)
            {
                cogImage = VisionProUtils.ToCogImage(bmp, isGrey);
            }
            else if (img is ICogImage cogImg)
            {
                cogImage = cogImg;
            }

            return nPointToNPointComponent.Run(cogImage);
        }

        /// <summary>
        /// 标定
        /// <para>坐标数据从外部传入，不做内部保存，因为可以在外部对数据做修改</para>
        /// </summary>
        public void Calibrate(List<HandeyeSinglePoint> points)
        {
            if (points.Count < 3)
                throw new VisionException("坐标数据少于 3 条，无法标定");

            try
            {
                nPointToNPointComponent.Calibrate(points);

                log.Info(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_Calibration, R_Success));
            }
            catch (Exception e)
            {
                log.Error(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_Calibration, R_Fail, e.Message));

                throw new VisionException("标定失败");
            }
        }

        public void Save()
        {
            toolBlock.Save();
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        public void SavePattern()
        {
            if (calibInfo.HandEyeSingleInfo.PMAlignOrToolBlock)
                pMAlignComponent.Save();
            else
                toolBlockComponent.Save();
        }

        public void Dispose()
        {
            pMAlignComponent.Dispose();

            nPointToNPointComponent.Dispose();
        }

        /// <summary>
        /// 返回 NPointToNPoint 的 OutputImage
        /// </summary>
        public object OutputImage
        {
            get { return nPointToNPointComponent.GetOutputImage(); }
        }

        public double GetRMS() => nPointToNPointComponent.GetRMS();

        public double GetAspect() => nPointToNPointComponent.GetAspect();

        public double GetSkew() => nPointToNPointComponent.GetSkew();
    }
}
