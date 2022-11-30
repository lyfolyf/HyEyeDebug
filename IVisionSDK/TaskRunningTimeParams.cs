using HyEye.Models;
using System.Collections.Generic;

namespace VisionSDK
{
    public class TaskRunningTimeParams
    {
        public HyImageInfo HyImage { get; set; }

        public IEnumerable<(string Name, object Value)> Params;

        public ToolBlockCallBack CallBack;

        //add by LuoDian @ 20211214 ���һ���������������ֲ�ͬ�����Ϻţ����ض�Ӧ���ϺŵĲ���
        public string SubName;

        //add by LuoDian @ 20211109 ���һ������������ָ���Ƿ������ͼ������������ToolBlock
        public bool IsWaitAllImage;
    }
}
