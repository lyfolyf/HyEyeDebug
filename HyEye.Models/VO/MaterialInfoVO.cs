using System.Collections.Generic;

namespace HyEye.Models.VO
{
    public class MaterialInfoVO
    {
        public int Index { get; set; }

        public string Name { get; set; }

        //add by LuoDian @ 20211209 �������ϺŵĿ����л�
        public List<string> LstSubName { get; set; }
    }
}
