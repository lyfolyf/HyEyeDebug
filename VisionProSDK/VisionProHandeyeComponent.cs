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
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace VisionSDK._VisionPro
{
    public class VisionProHandeyeComponent : IHandeyeComponent
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
                    CogToolBlockTerminal input = new CogToolBlockTerminal("Img1", typeof(ICogImage));
                    toolBlock.Inputs.Add(input);

                    CogToolBlockTerminal outputX = new CogToolBlockTerminal("X", typeof(double));
                    CogToolBlockTerminal outputY = new CogToolBlockTerminal("Y", typeof(double));
                    CogToolBlockTerminal outputA = new CogToolBlockTerminal("A", typeof(double));
                    toolBlock.Outputs.Add(outputX);
                    toolBlock.Outputs.Add(outputY);
                    toolBlock.Outputs.Add(outputA);
                }
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
                    { "X", toolBlock.Outputs["X"].Value },
                    { "Y", toolBlock.Outputs["Y"].Value },
                    { "A", toolBlock.Outputs["A"].Value },
                    //{ "Graphic", toolBlock.CreateLastRunRecord().SubRecords[0] }
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

            public void Calibrate(List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)> points)
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

            void SetPointPair(List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)> points)
            {
                int xyPointNum = Math.Min(CalibInfo.HandEyeInfo.XYPointNum, points.Count);
                nPointToNPoint.Calibration.NumPoints = points.Take(xyPointNum).Where(a => a.disable == false).Count();
                for (int i = 0, j = 0; i < xyPointNum; i++)
                {
                    if (points[i].disable) continue;

                    nPointToNPoint.Calibration.SetUncalibratedPoint(j, points[i].X1, points[i].Y1);
                    nPointToNPoint.Calibration.SetRawCalibratedPoint(j, points[i].X2, points[i].Y2);
                    j++;

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

        class FitCircleComponent : IDisposable
        {
            public CalibrationInfoVO CalibInfo { get; set; }

            CogFitCircleEditV2 circleEdit;
            readonly CogFitCircleTool circleTool;
            readonly IToolBlockComponent toolBlock;

            readonly IPathRepository pathRepo;
            readonly IBasicRepository basicRepo;
            readonly IGLog log;

            public FitCircleComponent(
                CalibrationInfoVO calibInfo,
                IToolBlockComponent toolBlock,
                IBasicRepository basicRepo,
                IPathRepository pathRepo,
                IGLog log)
            {
                this.CalibInfo = calibInfo;
                this.basicRepo = basicRepo;
                this.pathRepo = pathRepo;
                this.log = log;

                this.toolBlock = toolBlock;

                string circlePath = pathRepo.GetCalibCircleVppPath(calibInfo.Name);
                if (File.Exists(circlePath))
                {
                    try
                    {
                        circleTool = (CogFitCircleTool)CogSerializer.LoadObjectFromFile(circlePath);
                    }
                    catch (Exception e)
                    {
                        log.Error(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_LoadVpp, R_Fail, $"加载旋转中心文件 \"{circlePath}\" 出错，{e.Message}"));
                    }
                }
                else
                {
                    circleTool = new CogFitCircleTool();
                }
            }

            public Control Control
            {
                get
                {
                    if (circleEdit == null)
                    {
                        circleEdit = new CogFitCircleEditV2
                        {
                            Dock = DockStyle.Fill,
                            Subject = circleTool
                        };
                    }
                    return circleEdit;
                }
            }

            public void SetInputImage(ICogImage cogImage) => circleTool.InputImage = cogImage;

            public CogCircle GetCircle(CogPointMarker[] PhysicalPoint)
            {
                if (PhysicalPoint.Length < CalibInfo.HandEyeInfo.APointNum)
                {
                    log.Error(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_GetCircle, R_Fail, "坐标点不足"));
                    return null;
                }

                circleTool.RunParams.NumPoints = CalibInfo.HandEyeInfo.APointNum;
                for (int i = 0; i < CalibInfo.HandEyeInfo.APointNum; i++)
                {
                    circleTool.RunParams.SetPoint(i, PhysicalPoint[i].X, PhysicalPoint[i].Y);
                }
                circleTool.Run();

                CogCircle result = circleTool.Result?.GetCircle();

                if (result == null)
                    log.Error(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_GetCircle, R_Fail));
                else
                {
                    log.Info(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_GetCircle, R_Success, $"CenterX = {result.CenterX}, CenterY = {result.CenterY}, Radius = {result.Radius}"));

                    AddCenterToToolBlock(result.CenterX, result.CenterY);
                }

                return result;
            }

            public void GetCircleByPointAndAngle(CogPointMarker[] PhysicalPoint, double[] PhysicalAngle)
            {
                if (PhysicalPoint.Length < CalibInfo.HandEyeInfo.APointNum)
                {
                    log.Error(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_GetCircle, R_Fail, "坐标点不足"));
                    return;
                }
                if (PhysicalPoint.Length != PhysicalAngle.Length)
                {
                    log.Error(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_GetCircle, R_Fail, "坐标点个数与角度个数不匹配"));
                    return;
                }

                (double x, double y) basePoint;
                List<(double x0, double y0)> CenterList = new List<(double x0, double y0)>();
                basePoint.x = PhysicalPoint[0].X;
                basePoint.y = PhysicalPoint[0].Y;

                double CurrentAngle = 0.0D;
                for (int i = 1; i < PhysicalPoint.Length; i++)
                {
                    CurrentAngle += PhysicalAngle[i];
                    CenterList.Add(GetCenter_Ni(basePoint, (PhysicalPoint[i].X, PhysicalPoint[i].Y), CurrentAngle));
                }
                double avgX = CenterList.Average(p => p.x0);
                double avgY = CenterList.Average(p => p.y0);

                AddCenterToToolBlock(avgX, avgY);
                log.Info(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_GetCircle, R_Success, $"CenterX = {avgX}, CenterY = {avgY}"));
            }

            (double x0, double y0) GetCenter_Ni((double x1, double y1) old_p, (double x2, double y2) new_p, double angle)
            {
                //反推方程太长了，拆开表示能清晰些。
                double b1 = new_p.x2 + old_p.y1 * GetSinA(angle) - old_p.x1 * GetCosA(angle);
                double b11 = 1 - GetCosA(angle);

                double b2 = old_p.x1 * GetSinA(angle) + old_p.y1 * GetCosA(angle) - new_p.y2;
                double b22 = GetCosA(angle) - 1;

                double y0 = (b2 * b11 - GetSinA(angle) * b1) / (b11 * b22 - GetSinA(angle) * GetSinA(angle));
                double x0 = (b1 - GetSinA(angle) * y0) / b11;

                x0 = double.IsNaN(x0) ? 0.0D : x0;
                y0 = double.IsNaN(y0) ? 0.0D : y0;

                return (x0, y0);
            }

            double GetSinA(double a)
            {
                switch (a)
                {
                    case 0:
                        return 0.0D;
                    case 30:
                        return 0.5;
                    case 90:
                        return 1;
                    case 180:
                        return 0.0D;
                    case 270:
                        return -1;
                    case 360:
                        return 0.0D;
                    default:
                        return Math.Sin(a * Math.PI / 180);
                }
            }

            double GetCosA(double a)
            {
                switch (a)
                {
                    case 0:
                        return 1;
                    case 60:
                        return 0.5;
                    case 90:
                        return 0.0D;
                    case 180:
                        return -1;
                    case 270:
                        return 0.0D;
                    case 360:
                        return 1;
                    default:
                        return Math.Cos(a * Math.PI / 180);
                }
            }

            void AddCenterToToolBlock(double centerX, double centerY)
            {
                string centerXName = CalibInfo.Name + "_CenterX";
                string centerYName = CalibInfo.Name + "_CenterY";

                if (toolBlock.InputContains(centerXName))
                {
                    toolBlock.SetInputValue(centerXName, centerX);
                }
                else
                {
                    toolBlock.AddInput(new ParamInfo { Name = centerXName, Type = ParamInfo.DoubleType, Value = centerX });
                }

                if (toolBlock.InputContains(centerYName))
                {
                    toolBlock.SetInputValue(centerYName, centerY);
                }
                else
                {
                    toolBlock.AddInput(new ParamInfo { Name = centerYName, Type = ParamInfo.DoubleType, Value = centerY });
                }
            }

            public void Save()
            {
                string circlePath = pathRepo.GetCalibCircleVppPath(CalibInfo.Name);

                VisionProUtils.SaveSubject(circleTool, circlePath, basicRepo.VPPExcludeDataBindings);

                log.Info(new CalibVisionLogMessage(CalibInfo.TaskName, CalibInfo.Name, A_Save, R_Success, $"保存旋转中心 VPP：{circlePath}"));
            }

            public void Dispose()
            {
                circleEdit?.Dispose();
                circleTool.Dispose();

                string circlePath = pathRepo.GetCalibCircleVppPath(CalibInfo.Name);
                if (File.Exists(circlePath))
                    File.Delete(circlePath);
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
        readonly FitCircleComponent fitCircleComponent;

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

        public VisionProHandeyeComponent(
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
            fitCircleComponent = new FitCircleComponent(calibInfo, toolBlock, basicRepo, pathRepo, log);
        }

        public Control PatternControl
        {
            get
            {
                calibInfo = calibRepo.GetCalibration(calibInfo.Name);
                if (calibInfo.HandEyeInfo.PMAlignOrToolBlock)
                    return pMAlignComponent.Control;
                else
                    return toolBlockComponent.Control;
            }
        }

        public Control NPointToNPointControl
        {
            get { return nPointToNPointComponent.Control; }
        }

        public Control FitCircleControl
        {
            get { return fitCircleComponent.Control; }
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

        int curIndex = 1;

        // 在 S 指令中执行
        public int Start()
        {
            calibInfo = calibRepo.GetCalibration(calibInfo.Name);
            if (calibInfo.HandEyeInfo.PMAlignOrToolBlock)
                pMAlignComponent.CalibInfo = calibInfo;
            else
                toolBlockComponent.CalibInfo = calibInfo;

            nPointToNPointComponent.CalibInfo = calibInfo;
            if (fitCircleComponent != null)
                fitCircleComponent.CalibInfo = calibInfo;

            log.Info(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_Calibration, R_Start));

            return ErrorCodeConst.OK;
        }

        public bool CheckIndex(int index)
        {
            return curIndex == index;
        }

        public (int errCode, Dictionary<string, object>) Run(int index, object img, bool isGrey)
        {
            if (curIndex != index)
                return (ErrorCodeConst.IndexError, ErrOutputs);

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

            if (curIndex == 1)
            {
                nPointToNPointComponent.SetInputImage(cogImage);
                if (fitCircleComponent != null)
                    fitCircleComponent.SetInputImage(cogImage);
            }

            Dictionary<string, object> outputs;

            if (calibInfo.HandEyeInfo.PMAlignOrToolBlock)
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

            curIndex++;

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
        public void Calibrate(List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)> points)
        {
            if (points.Count < 3)
                throw new VisionException("坐标数据少于 3 条，无法标定");

            try
            {
                nPointToNPointComponent.Calibrate(points);

                if (calibInfo.HandEyeInfo.EnabledFitCircle)
                {
                    CogPointMarker[] ImagePoint = points.Skip(calibInfo.HandEyeInfo.XYPointNum).Select(a => new CogPointMarker { X = a.X1, Y = a.Y1 }).ToArray();

                    double[] PhysicalAngle = points.Skip(calibInfo.HandEyeInfo.XYPointNum).Select(p => p.A2).ToArray();

                    CogPointMarker[] PhysicalPoint = nPointToNPointComponent.ToPhysicalPoint(ImagePoint);


                    switch (calibInfo.HandEyeInfo.FitCircleType)
                    {
                        case 1:
                            {
                                fitCircleComponent.GetCircle(PhysicalPoint);
                            }
                            break;
                        case 2:
                            {
                                fitCircleComponent.GetCircleByPointAndAngle(PhysicalPoint, PhysicalAngle);
                            }
                            break;
                    }
                }

                log.Info(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_Calibration, R_Success));
            }
            catch (Exception e)
            {
                log.Error(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_Calibration, R_Fail, e.Message));

                throw new VisionException("标定失败");
            }
        }

        public void Reset()
        {
            if (curIndex != 1)
            {
                curIndex = 1;

                log.Info(new CalibVisionLogMessage(calibInfo.TaskName, calibInfo.Name, A_Reset, R_Success));
            }
        }

        public void Save()
        {
            toolBlock.Save();
            fitCircleComponent?.Save();
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        public void SavePattern()
        {
            if (calibInfo.HandEyeInfo.PMAlignOrToolBlock)
                pMAlignComponent.Save();
            else
                toolBlockComponent.Save();
        }

        public void Dispose()
        {
            pMAlignComponent.Dispose();

            nPointToNPointComponent.Dispose();

            fitCircleComponent?.Dispose();
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
