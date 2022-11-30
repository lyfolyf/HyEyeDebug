using HyEye.API.Repository;
using HyEye.Services.Script;
using System.Collections.Generic;
using System.Linq;

namespace HyEye.Services
{
    public interface IScriptService
    {
        void Init();

        void Clear();

        void RunScriptCmd(string[] args);

        LinkedDictionary<string, object> ModifyOutputs(string taskName, int acqIndex, LinkedDictionary<string, object> outputs);
    }

    public class ScriptService : IScriptService
    {
        readonly IDataRepository dataSaveRepo;
        readonly IHyEyeAdvancedScript script;
        readonly ITaskRepository taskRepo;

        public ScriptService(
            IDataRepository dataSaveRepo,
            IHyEyeAdvancedScript script,
            ITaskRepository taskRepo)
        {
            this.dataSaveRepo = dataSaveRepo;
            this.script = script;
            this.taskRepo = taskRepo;
        }

        Dictionary<string, int> taskAcqCount;

        public void Init()
        {
            taskAcqCount = taskRepo.GetTasks().ToDictionary(k => k.Name, v => v.CameraAcquireImage.AcquireImages.Count);

            script.Init();
        }

        public void Clear()
        {
            script.Clear();
        }

        public void RunScriptCmd(string[] args)
        {
            script.RunScriptCmd(args);
        }

        public LinkedDictionary<string, object> ModifyOutputs(string taskName, int acqIndex, LinkedDictionary<string, object> outputs)
        {
            LinkedDictionary<string, object> output1 = script.ModifyOutputs(taskName, acqIndex, outputs);

            if (dataSaveRepo.Enabled)
            {
                if (dataSaveRepo.SaveMode == API.Config.DataSaveMode.All)
                {
                    script.SaveRecord(taskName, acqIndex, outputs);
                }
                else if (dataSaveRepo.SaveMode == API.Config.DataSaveMode.Last)
                {
                    if (acqIndex == taskAcqCount[taskName])
                    {
                        script.SaveRecord(taskName, acqIndex, outputs);
                    }
                }
            }

            return output1;
        }

    }
}
