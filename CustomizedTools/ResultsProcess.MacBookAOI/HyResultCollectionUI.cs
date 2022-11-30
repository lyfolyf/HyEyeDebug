using System;
using System.ComponentModel;

using GL.Kit.Log;
using HyVision.Tools;
using HyVision.Models;

namespace ResultsProcess
{
    // public partial class HyResultCollectionUI : UserControl
    public partial class HyResultCollectionUI : BaseHyUserToolEdit<HyResultCollectionBL>
    {
        private LogPublisher log;
        private HyResultCollectionBL resultCollection;

        public HyResultCollectionUI()
        {
            InitializeComponent();
        }

        public HyResultCollectionUI(LogPublisher log, HyResultCollectionBL resultCollection) : this() 
        {
            if (log == null)
                log = Autofac.AutoFacContainer.Resolve<LogPublisher>();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HyResultCollectionBL Subject
        {
            get { return resultCollection; }
            set
            {
                if (!object.Equals(resultCollection, value))
                {
                    resultCollection = value;
                    hyTCEInput.Subject = resultCollection.Inputs;
                    hyTCEOutput.Subject = resultCollection.Outputs;

                    // 自动添加输入参数
                    foreach((string name, Type type) in ConstField.GetProperties())
                    {
                        HyTerminal input = new HyTerminal(name, type);
                        input.GUID = Guid.NewGuid().ToString("N");
                        if (!resultCollection.Inputs.Contains(name))
                            resultCollection.Inputs.Add(input);
                    }

                    // 自动添加输出参数
                    HyTerminal output = new HyTerminal(ConstField.RESULT_JSON_STR, typeof(string));
                    output.GUID = Guid.NewGuid().ToString("N");
                    if (!resultCollection.Outputs.Contains(ConstField.RESULT_JSON_STR))
                        resultCollection.Outputs.Add(output);
                }
            }
        }

        public override void UpdateDataToObject()
        {
            if (resultCollection == null)
                resultCollection = new HyResultCollectionBL();
        }

        public override void Save()
        {
            try
            {
                UpdateDataToObject();
            }
            catch (Exception ex)
            {
                log?.Error(ex.Message);
            }
        }
    }
}
