using HyEye.WForm.Display;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HyEye.WForm
{
    [Serializable]
    internal class SurfaceInfo
    {
        internal SurfaceInfo()
        {
            //Reset();
        }
        internal string Name { get; set; } = "LCM";
        internal string SurfaceModelPath { get; set; } = string.Empty;
        internal int VCount
        {
            get { return vCount; }
            set
            {
                vCount = value;
                //Reset();
            }
        }

        internal int HCount
        {
            get
            {
                return hCount;
            }
            set
            {
                hCount = value;
                //Reset();
            }
        }

        public List<AreaInfo> AreaInfos
        {
            get { return areaInfos; }
            set
            {
                areaInfos = value;
            }
        }

        //internal List<string> modelPathList = new List<string>();
        private List<AreaInfo> areaInfos = new List<AreaInfo>();
        private int vCount = 4;
        private int hCount = 3;

        //internal void SetModelPath(int index, string path)
        //{
        //    if (index < 0 || index >= modelPathList.Count)
        //    {
        //        return;
        //    }
        //    modelPathList[index] = path;
        //}
        //internal string GetModelPath(int index)
        //{
        //    if (index < 0 || index >= modelPathList.Count)
        //    {
        //        return string.Empty;
        //    }
        //    return modelPathList[index];
        //}
        //private void Reset()
        //{
        //    var length = vCount * hCount;
        //    while (modelPathList.Count > length)
        //    {
        //        modelPathList.RemoveAt(length);
        //    }
        //    while (modelPathList.Count < length)
        //    {
        //        modelPathList.Add(string.Empty);
        //    }
        //}

        public override string ToString()
        {
            return $"{Name} - {hCount}x{vCount} ";
        }
    }

    [Serializable]
    public class AreaInfo
    {
        public bool Selected { get; internal set; } = false;
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; } = 1;
        public int ColumnSpan { get; set; } = 1;
        public int Id { get; set; } = 1;
        public Color Color { get; set; } = Color.Cyan;
        public string Info { get; set; } = string.Empty;
        public string Block { get; set; } = string.Empty;
        /// <summary>
        /// 示例图片路径
        /// </summary>
        public string ModelPath { get; set; } = string.Empty;
    }
}
