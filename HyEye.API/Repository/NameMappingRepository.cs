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
    public interface INameMappingRepository
    {
        TaskVisionMappingVO GetTaskVisionMapping(string taskName);

        void AddTaskVisionMapping(string taskName);

        #region Input

        string GetAcqImageMapping(string taskName, string acqImageName);

        void AddAcqImageMapping(string taskName, string acqImageName, string toolName);

        void DeleteAcqImageMapping(string taskName, string acqImageName);

        #endregion

        void Save();
    }

    public class NameMappingRepository : INameMappingRepository
    {
        readonly ITaskRepository taskRepo;
        readonly IMapper mapper;
        readonly IGLog log;

        readonly List<TaskVisionMapping> taskVisionMappings;

        public NameMappingRepository(
            ITaskRepository taskRepo,
            IMapper mapper,
            IGLog log)
        {
            this.taskRepo = taskRepo;
            this.mapper = mapper;
            this.log = log;

            taskVisionMappings = ApiConfig.TaskVisionMappingConfig.TaskVisionMappings;

            this.taskRepo.TaskAdd += TaskRepo_TaskAdd;
            this.taskRepo.TaskDelete += TaskRepo_TaskDelete;
            this.taskRepo.TaskRename += TaskRepo_TaskRename;

            // 这里不能添加取像和标定的 Add 事件，因为不知道控件名称
            // 取像和标定的删除不能在这里做关联，因为 ToolBlock 中要获取 Input Name 做删除操作
            // 标定的重命名也不能在这里做，因为标定名称和 ToolBlock 中是一致的，ToolBlock 中需要获取名称做重命名
            // 取像的重命名可以在这里添加事件，因为取像重命名时 ToolBlock 无需获取映射

            this.taskRepo.AcqImageRename += TaskRepo_AcqImageRename;

            this.taskRepo.AfterSave += TaskRepo_TaskSave;
        }

        bool changedToken = false;

        #region Task 事件

        private void TaskRepo_TaskAdd(TaskInfoVO taskInfo)
        {
            AddTaskVisionMapping(taskInfo.Name);
        }

        private void TaskRepo_TaskDelete(string taskName)
        {
            taskVisionMappings.Remove(taskVisionMappings.FirstOrDefault(a => a.TaskName == taskName));

            changedToken = true;
        }

        private void TaskRepo_TaskRename(string oldTaskName, string newTaskName)
        {
            TaskVisionMapping mapping = taskVisionMappings.FirstOrDefault(a => a.TaskName == oldTaskName);

            if (mapping != null)
            {
                mapping.TaskName = newTaskName;

                changedToken = true;
            }
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            TaskVisionMapping mapping = taskVisionMappings.FirstOrDefault(a => a.TaskName == taskName);
            if (mapping == null)
            {
                log.Error(new ApiLogMessage(taskName, oldAcqImageName, A_Rename, R_Fail, "重命名取像映射失败，未找到任务"));
                throw new ApiException("重命名取像映射失败，未找到任务");
            }

            NameMapper inputMapper = mapping.Inputs.FirstOrDefault(a => a.Key == oldAcqImageName);
            if (inputMapper != null)
            {
                inputMapper.Key = newAcqImageName;
            }

            NameMapper graphicMapper = mapping.Graphics.FirstOrDefault(a => a.Key == oldAcqImageName);
            if (graphicMapper != null)
            {
                graphicMapper.Key = newAcqImageName;
            }

            changedToken = true;
        }

        private void TaskRepo_TaskSave()
        {
            Save();
        }

        #endregion

        public TaskVisionMappingVO GetTaskVisionMapping(string taskName)
        {
            TaskVisionMapping mapping = taskVisionMappings.FirstOrDefault(a => a.TaskName == taskName);

            return mapper.Map<TaskVisionMappingVO>(mapping);
        }

        public void AddTaskVisionMapping(string taskName)
        {
            TaskVisionMapping mapping = new TaskVisionMapping { TaskName = taskName };

            taskVisionMappings.Add(mapping);

            changedToken = true;
        }

        #region 取像映射

        public string GetAcqImageMapping(string taskName, string acqImageName)
        {
            return taskVisionMappings.FirstOrDefault(a => a.TaskName == taskName)
                ?.Inputs.FirstOrDefault(a => a.Key == acqImageName)?.Value;
        }

        public void AddAcqImageMapping(string taskName, string acqImageName, string toolName)
        {
            TaskVisionMapping mapping = taskVisionMappings.FirstOrDefault(a => a.TaskName == taskName);
            if (mapping == null)
            {
                log.Error(new ApiLogMessage(taskName, acqImageName, A_Add, R_Fail, "新增输入映射失败，未找到任务"));
                throw new ApiException("新增输入映射失败，未找到任务");
            }

            mapping.Inputs.Add(new NameMapper { Key = acqImageName, Value = toolName });

            changedToken = true;
        }

        public void DeleteAcqImageMapping(string taskName, string acqImageName)
        {
            TaskVisionMapping mapping = taskVisionMappings.FirstOrDefault(a => a.TaskName == taskName);

            if (mapping != null)
            {
                mapping.Inputs.RemoveAll(a => a.Key == acqImageName);
                mapping.Graphics.RemoveAll(a => a.Key == acqImageName);
            }

            changedToken = true;
        }

        #endregion

        public void Save()
        {
            if (changedToken)
            {
                changedToken = false;

                ApiConfig.Save(ApiConfig.TaskVisionMappingConfig);
            }
        }

    }
}
