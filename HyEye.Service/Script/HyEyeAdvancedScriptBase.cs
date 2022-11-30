using Autofac;
using System;
using System.Collections.Generic;

namespace HyEye.Services.Script
{
    public class HyEyeAdvancedScriptBase : IHyEyeAdvancedScript
    {
        IDataService dataService;

        public HyEyeAdvancedScriptBase()
        {
            dataService = AutoFacContainer.Resolve<IDataService>();
        }

        public virtual void Init()
        {

        }

        public virtual void Clear()
        {

        }

        public virtual void RunScriptCmd(string[] args)
        {

        }

        public virtual LinkedDictionary<string, object> ModifyOutputs(string taskName, int acqIndex, LinkedDictionary<string, object> outputs)
        {
            return outputs;
        }

        public virtual void SaveRecord(string taskName, int acqIndex, LinkedDictionary<string, object> outputs)
        {
            dataService.WriteRecord(taskName, acqIndex, outputs);
        }

        public virtual void MergeRecord(string[] taskNames, DateTime beginTime, DateTime endTime)
        {

        }

    }
}
