using System.Collections.Generic;
using VisionFactory;
using VisionSDK;

namespace HyEye.Services
{
    public interface ITaskUtils
    {
        void SetInputValue(string taskName, string paramName, object value);

        void SetInputValues(string taskName, LinkedDictionary<string, object> @params);

        void SetOutputValue(string taskName, string paramName, object value);

        void SetOutputValues(string taskName, LinkedDictionary<string, object> @params);
    }

    public class TaskUtils : ITaskUtils
    {
        readonly ToolBlockComponentSet toolBlockComponentSet;

        public TaskUtils(ToolBlockComponentSet toolBlockComponentSet)
        {
            this.toolBlockComponentSet = toolBlockComponentSet;
        }

        public void SetInputValue(string taskName, string paramName, object value)
        {
            IToolBlockComponent toolBlock = toolBlockComponentSet.GetComponent(taskName);

            if (toolBlock.InputContains(paramName))
            {
                toolBlock.SetInputValue(paramName, value);
            }
        }

        public void SetInputValues(string taskName, LinkedDictionary<string, object> @params)
        {
            IToolBlockComponent toolBlock = toolBlockComponentSet.GetComponent(taskName);

            foreach (var p in @params)
            {
                if (toolBlock.InputContains(p.Key))
                {
                    toolBlock.SetInputValue(p.Key, p.Value);
                }
            }
        }

        public void SetOutputValue(string taskName, string paramName, object value)
        {
            IToolBlockComponent toolBlock = toolBlockComponentSet.GetComponent(taskName);

            if (toolBlock.OutputContains(paramName))
            {
                toolBlock.SetOutputValue(paramName, value);
            }
        }

        public void SetOutputValues(string taskName, LinkedDictionary<string, object> @params)
        {
            IToolBlockComponent toolBlock = toolBlockComponentSet.GetComponent(taskName);

            foreach (var p in @params)
            {
                if (toolBlock.OutputContains(p.Key))
                {
                    toolBlock.SetOutputValue(p.Key, p.Value);
                }
            }
        }
    }
}
