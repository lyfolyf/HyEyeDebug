using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System.Collections.Generic;
using System.Linq;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface IRecordShowRepository
    {
        List<RecordShowInfoVO> GetRecordShowList();

        void SetRecordShow(List<RecordShowInfoVO> recordInfos);

        RecordShowInfoVO GetRecordShow(string taskName);

        void AddRecordShow(RecordShowInfoVO info);

        void Save();
    }

    public class RecordShowRepository : IRecordShowRepository
    {

        readonly ITaskRepository taskRepo;
        readonly IMapper mapper;
        readonly IGLog log;
        readonly RecordShowConfig config;

        public RecordShowRepository(
            ITaskRepository taskRepo,
            IMapper mapper,
            IGLog log)
        {
            this.taskRepo = taskRepo;
            this.mapper = mapper;
            this.log = log;

            config = ApiConfig.RecordShowConfig;

            taskRepo.TaskAdd += TaskRepo_TaskAdd;
            taskRepo.TaskRename += TaskRepo_TaskRename;
            taskRepo.TaskDelete += TaskRepo_TaskDelete;
        }

        bool changedToken = false;

        private void TaskRepo_TaskAdd(TaskInfoVO taskInfo)
        {
            RecordShowInfo addInfo = new RecordShowInfo()
            {
                TaskName = taskInfo.Name,
                RecordIndex = 0
            };
            config.RecordShowInfos.Add(addInfo);

            changedToken = true;
        }

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            foreach (RecordShowInfo item in config.RecordShowInfos)
            {
                if (item.TaskName == oldName)
                    item.TaskName = newName;
            }

            changedToken = true;
        }

        private void TaskRepo_TaskDelete(string taskName)
        {
            config.RecordShowInfos.Remove(p => p.TaskName == taskName);

            changedToken = true;
        }

        public void AddRecordShow(RecordShowInfoVO info)
        {
            if (config.RecordShowInfos.Any(p => p.TaskName == info.TaskName))
            {
                log.Error(new ApiLogMessage(info.TaskName, null, A_Add, R_Fail, "该任务已添加设置"));
                throw new ApiException("该任务已添加设置");
            }

            RecordShowInfo rsInfo = mapper.Map<RecordShowInfo>(info);

            config.RecordShowInfos.Add(rsInfo);

            changedToken = true;
        }

        public RecordShowInfoVO GetRecordShow(string taskName)
        {
            RecordShowInfo recordShowInfo = config.RecordShowInfos.FirstOrDefault(p => p.TaskName == taskName);
            return mapper.Map<RecordShowInfoVO>(recordShowInfo);
        }

        public List<RecordShowInfoVO> GetRecordShowList()
        {
            return mapper.Map<List<RecordShowInfoVO>>(config.RecordShowInfos);
        }

        public void SetRecordShow(List<RecordShowInfoVO> recordInfos)
        {
            config.RecordShowInfos = mapper.Map<List<RecordShowInfo>>(recordInfos);

            changedToken = true;
        }

        public void Save()
        {
            if (changedToken)
            {
                ApiConfig.Save(ApiConfig.RecordShowConfig);
                log.Info(new ApiLogMessage("Record输出", null, A_Save, R_Success));
            }
        }

    }
}
