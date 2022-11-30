using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models.VO;
using System.Drawing;
using System.Windows.Forms;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace VisionSDK._VisionPro
{
    public class VisionProCheckerboardComponent : ICheckerboardComponent
    {
        CalibrationInfoVO calibInfo;

        readonly IToolBlockComponent toolBlock;
        readonly IMaterialRepository materialRepo;
        readonly IGLog log;

        CogCalibCheckerboardEditV2 checkerboardEditV2;
        CogCalibCheckerboardTool checkerboardTool;

        public VisionProCheckerboardComponent(
            CalibrationInfoVO calibInfo,
            IToolBlockComponent toolBlock,
            IMaterialRepository materialRepo,
            IGLog log)
        {
            this.calibInfo = calibInfo;
            this.materialRepo = materialRepo;
            this.log = log;

            this.toolBlock = toolBlock;
            checkerboardTool = (CogCalibCheckerboardTool)toolBlock.GetTool(calibInfo.Name);
        }

        public Control DisplayedControl
        {
            get
            {
                if (checkerboardEditV2 == null)
                {
                    checkerboardEditV2 = new CogCalibCheckerboardEditV2
                    {
                        Dock = DockStyle.Fill,
                        Subject = checkerboardTool
                    };
                    checkerboardEditV2.SubjectChanged += (sender, e) =>
                    {
                        checkerboardTool = checkerboardEditV2.Subject;
                    };
                }
                return checkerboardEditV2;
            }
        }

        public void RenameCalibration(string newName)
        {
            calibInfo.Name = newName;
        }

        public void SetInputImage(Bitmap bitmap, bool isGrey)
        {
            if (bitmap != null)
            {
                ICogImage cogImage = VisionProUtils.ToCogImage(bitmap, isGrey);
                checkerboardTool.InputImage = cogImage;
                checkerboardTool.Calibration.CalibrationImage = cogImage;

                bitmap.Dispose();
            }
        }

        public object Calibrate(Bitmap bitmap, bool isGrey)
        {
            checkerboardTool.Calibration.Calibrate();
            checkerboardTool.Run();

            return checkerboardTool.OutputImage;
        }

        // HandEye 里做畸变矫正
        public object Run(Bitmap bitmap, bool isGrey)
        {
            ICogImage cogImage = VisionProUtils.ToCogImage(bitmap, isGrey);
            checkerboardTool.InputImage = cogImage;
            checkerboardTool.Run();

            bitmap.Dispose();

            return checkerboardTool.OutputImage;
        }

        public void Save()
        {
            toolBlock.Save();
        }

        const string AspectTextboxName = "numLinearTransformAspect";
        const string SkewTextboxName = "numLinearTransformSkew";

        public object OutputImage
        {
            get { return checkerboardTool.OutputImage; }
        }

        public double GetRMS()
        {
            return checkerboardTool.Calibration.ComputedRMSError;
        }

        public double GetAspect()
        {
            string str = checkerboardEditV2.Controls.Find(AspectTextboxName, true)[0].Controls[0].Text;
            if (str != null && double.TryParse(str, out double aspect))
                return aspect;
            else
            {
                throw new VisionException("获取纵横比失败");
            }
        }

        public double GetSkew()
        {
            string str = checkerboardEditV2.Controls.Find(SkewTextboxName, true)[0].Controls[0].Text;
            if (str != null && double.TryParse(str, out double aspect))
                return aspect;
            else
                throw new VisionException("获取倾斜值失败");
        }

        public void Dispose()
        {
            checkerboardTool?.Dispose();

            checkerboardEditV2?.Dispose();
        }

    }
}
