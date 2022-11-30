using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyVision.Tools.ImageDisplay;

namespace HyEye.WForm.Display
{
    /// <summary>
    /// 单个缺陷的信息
    /// </summary>
    [Serializable]
    public class DefectInfo
    {
        public DefectInfo(string imageSavePath, string defectType, double area, BaseHyROI rOI, Bitmap image = null, string algorithmData = "") : this(imageSavePath, image)
        {
            DefectType = defectType;
            Area = area;
            AlgorithmData = algorithmData;
            ROI = rOI;
        }

        public DefectInfo(string imageSavePath, Bitmap image = null)
        {
            ImageSavePath = imageSavePath;
            this.image = image;
        }

        /// <summary>
        /// 图片保存路径
        /// </summary>
        public string ImageSavePath { get; set; } = string.Empty;
        [NonSerialized]
        private Bitmap image = null;//不参与序列化
        public Bitmap Image
        {
            get
            {
                if (image == null)
                {
                    if (File.Exists(ImageSavePath))
                    {
                        try
                        {
                            image = new Bitmap(ImageSavePath);
                        }
                        catch { }
                    }
                }
                return image;
            }
            set { image = value; }
        }
        /// <summary>
        /// 检测大面
        /// </summary>
        public string Surface { get; internal set; } = string.Empty;
        /// <summary>
        /// 检测块区域
        /// </summary>
        public string Block { get; internal set; } = string.Empty;
        /// <summary>
        /// 相机名称
        /// </summary>
        public string Camera { get; internal set; } = string.Empty;
        /// <summary>
        /// 光源
        /// </summary>
        public string Light { get; internal set; } = string.Empty;
        /// <summary>
        /// 缺陷类型
        /// </summary>
        public string DefectType { get; set; } = string.Empty;
        /// <summary>
        /// 缺陷面积
        /// </summary>
        public double Area { get; set; } = 0;
        /// <summary>
        /// 算法数据
        /// </summary>
        public string AlgorithmData { get; set; } = string.Empty;
        /// <summary>
        /// 缺陷ROI
        /// </summary>
        public BaseHyROI ROI { get; set; } = null;


    }
    /// <summary>
    /// 单张图片的检测信息
    /// </summary>
    [Serializable]
    public class BlockResultInfo
    {
        private readonly List<DefectInfo> defects = new List<DefectInfo>();
        [NonSerialized]
        private Bitmap image = null;//不参与序列化
        private string imageName = string.Empty;

        public BlockResultInfo(string imageSavePath)
        {
            ImageSavePath = imageSavePath;
            if (File.Exists(ImageSavePath))
            {
                FileInfo fileInfo = new FileInfo(ImageSavePath);
                ImageName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
                //try
                //{
                //    image = new Bitmap(ImageSavePath);
                //}
                //catch { }
            }
        }

        public BlockResultInfo(Bitmap image, string imageName)
        {
            Image = image;
            ImageName = imageName;
        }

        public BlockResultInfo(Bitmap image, string imageName, string imageSavePath) : this(image, imageName)
        {
            ImageSavePath = imageSavePath;
        }

        /// <summary>
        /// 检测大面
        /// </summary>
        public string Surface { get; private set; } = string.Empty;
        /// <summary>
        /// 检测块区域
        /// </summary>
        public string Block { get; private set; } = string.Empty;
        /// <summary>
        /// 相机名称
        /// </summary>
        public string Camera { get; private set; } = string.Empty;
        /// <summary>
        /// 光源
        /// </summary>
        public string Light { get; private set; } = string.Empty;
        public string PruductModel { get; private set; } = string.Empty;
        public string SN { get; private set; } = string.Empty;
        public string Date { get; private set; } = string.Empty;
        /// <summary>
        /// 图片
        /// </summary>
        public Bitmap Image
        {
            get
            {
                if (image == null)
                {
                    if (File.Exists(ImageSavePath))
                    {
                        FileInfo fileInfo = new FileInfo(ImageSavePath);
                        ImageName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
                        try
                        {
                            image = new Bitmap(ImageSavePath);
                        }
                        catch { }
                    }
                }
                return image;
            }
            set
            {
                image = value;
            }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImageName // 示例： J413.RSH0000151370800.CosmeticAOI.20210930.154352.LCM.Bar_All.1_4
        {
            get { return imageName; }
            set
            {
                string[] info = value.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (info.Length == 8)
                {
                    PruductModel = info[0];
                    SN = info[1];
                    Date = $"{info[3]}.{info[4]}";
                    Surface = info[6] == "Mandrel" ? "Mandrel" : info[5];
                    Light = info[6];
                    string[] cs = info[7].Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    Camera = $"Camera {cs[0]}";
                    Block = info[7];
                }
                if (info.Length == 7)
                {
                    PruductModel = info[0];
                    SN = info[1];
                    Date = $"{info[3]}.{info[4]}";
                    Surface = info[5];
                    Light = info[6];
                    Camera = $"Camera {info[6]}";
                    Block = info[6];
                }
                imageName = value;
            }
        }
        /// <summary>
        /// 图片保存路径
        /// </summary>
        public string ImageSavePath { get; set; } = string.Empty;
        /// <summary>
        /// 检测缺陷信息列表
        /// </summary>
        public List<DefectInfo> Defects
        {
            get
            {
                return defects;
            }
        }

        public void Push(DefectInfo defectInfo)
        {
            defectInfo.Surface = this.Surface;
            defectInfo.Block = this.Block;
            defectInfo.Camera = this.Camera;
            defectInfo.Light = this.Light;
            defects.Add(defectInfo);
        }
        public void Reset()
        {
            image = null;
            ImageSavePath = string.Empty;
            defects.Clear();
        }
    }
    /// <summary>
    /// 区域面的检测结果信息
    /// </summary>
    [Serializable]
    public class SurfaceResultInfo
    {
        /// <summary>
        /// 检测大面
        /// </summary>
        public string Surface { get; private set; } = string.Empty;
        public string PruductModel { get; private set; } = string.Empty;
        public string SN { get; private set; } = string.Empty;
        public string Date { get; private set; } = string.Empty;
        List<BlockResultInfo> blockResultInfos = new List<BlockResultInfo>();
        private List<DefectInfo> defects = new List<DefectInfo>();

        public List<DefectInfo> Defects
        {
            get
            {
                defects = new List<DefectInfo>();
                foreach (var item in blockResultInfos)
                {
                    defects.AddRange(item.Defects);
                }
                return defects;
            }
        }

        public double DefectTotalArea
        {
            get
            {
                double area = 0;
                foreach (var item in Defects)
                {
                    area += item.Area;
                }
                return area;
            }
        }
        public void Push(BlockResultInfo Info)
        {
            blockResultInfos.Add(Info);
            Surface = Info.Surface;
            PruductModel = Info.PruductModel;
            SN = Info.SN;
            Date = Info.Date;
        }
        public void Reset()
        {
            blockResultInfos = new List<BlockResultInfo>();
            PruductModel = string.Empty;
            SN = string.Empty;
            Date = string.Empty;
        }

        public BlockResultInfo GetBlockResultInfo(DefectInfo defectInfo)
        {
            return blockResultInfos.Find(k => k.Defects.Contains(defectInfo));
        }

        public List<BlockResultInfo> GetBlockResultInfos(string block)
        {
            return blockResultInfos.FindAll(k => k.Block == block);
        }

        public int DefectCount(string block)
        {
            var list = GetBlockResultInfos(block);
            int count = 0;
            foreach (var item in list)
            {
                count += item.Defects.Count;
            }
            return count;
        }
    }
}
