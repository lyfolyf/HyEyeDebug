using System.Collections.Generic;
using System.Windows.Forms;

namespace VisionSDK
{
    public interface IJointComponent
    {
        List<JointControl> Controls { get; }

        void AddQuote(string taskName, string acqImageName, IToolBlockComponent toolBlock);

        (int errCode, LinkedDictionary<string, object>) Run(string taskName, string acqName, object img, bool isGrey);

        void Calibration(string taskName, string acqName, List<(double X1, double Y1, double X2, double Y2)> points);

        void Save();
    }

    public class JointControl
    {
        public string TaskName { get; set; }

        public string AcqName { get; set; }

        public virtual Control DisplayedControl
        {
            get;
        }
    }
}
