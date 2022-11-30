using System.Collections.Generic;
using System.Linq;
using VisionSDK;
using VisionSDK._VisionPro;

namespace VisionFactory
{
    public class JointDisplayComponentSet
    {
        readonly List<IDisplayTaskImageComponent> jointDisplay = new List<IDisplayTaskImageComponent>();

        public IDisplayTaskImageComponent GetJointDisplayControl(string taskName, string acqImageName)
        {
            IDisplayTaskImageComponent display = jointDisplay.FirstOrDefault(c => c.TaskName == taskName && c.AcqOrCalibName == acqImageName);
            if (display == null)
            {
                display = new VisionProDisplayTaskImageComponent
                {
                    TaskName = taskName,
                    AcqOrCalibName = acqImageName
                };

                jointDisplay.Add(display);
            }
            return display;
        }
    }
}
