using HyVision.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconSDK.Calib
{
    [Serializable]
    public class CalibFormTestBL : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(CalibFormTest);

        public override object Clone(bool containsData)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        protected override void Run2()
        {
            
        }
    }
}
