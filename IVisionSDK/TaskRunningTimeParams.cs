using HyEye.Models;
using System.Collections.Generic;

namespace VisionSDK
{
    public class TaskRunningTimeParams
    {
        public HyImageInfo HyImage { get; set; }

        public IEnumerable<(string Name, object Value)> Params;

        public ToolBlockCallBack CallBack;

        //add by LuoDian @ 20211214 添加一个参数，用于区分不同的子料号，加载对应子料号的参数
        public string SubName;

        //add by LuoDian @ 20211109 添加一个参数，用于指定是否等所有图像拍完再运行ToolBlock
        public bool IsWaitAllImage;
    }
}
