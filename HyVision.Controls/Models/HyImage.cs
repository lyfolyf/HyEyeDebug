using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HyVision.Models
{
    [Serializable]
    public class HyImage : IXmlSerializable
    {
        Bitmap m_image;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public bool IsGrey { get; private set; }

        // Added by louis on Mar. 19 2022 采图顺序由图像直接携带比较合理，使用方便
        public int AcqIndex { get; private set; }

        public Bitmap Image
        {
            get { return m_image; }
            set
            {
                m_image = value.DeepClone();
                Width = m_image.Width;
                Height = m_image.Height;
                IsGrey = m_image.PixelFormat == PixelFormat.Format8bppIndexed;
            }
        }

        //add by LuoDian @ 20210819 为了方便做数据转换，添加图像数据指针
        public IntPtr ImageData { get; private set; }
        //add by LuoDian @ 20210819 为了方便做数据转换，添加图像数据的类型，Bitmap中的PixelFormat主要是体现颜色信息，这个新参数是保存数据的类型
        public string ImageDataType { get; private set; }
        //add by LuoDian @ 20210819 为了方便做数据转换，添加一个结构体，用于获取图像数据
        public HyImage(string _ImageDataType, int _Width, int _Height, IntPtr _ImageData)
        {
            ImageDataType = _ImageDataType;
            Width = _Width;
            Height = _Height;
            ImageData = _ImageData;
        }



        public HyImage() { }

        public HyImage(Bitmap bmp)
        {
            Image = bmp;
        }

        // Added by louis on Mar. 19 2022 增加默认参数acqIndex 采图顺序由图像直接携带比较合理，使用方便
        public HyImage(Bitmap bmp, bool isGrey, int acqIndex = 0)
        {
            //update by LuoDian @ 20211119 已经在相机基类的回调函数那里进行了拷贝，这里不再拷贝
            //m_image = (Bitmap)bmp.Clone();
            m_image = bmp;
            Width = m_image.Width;
            Height = m_image.Height;
            AcqIndex = acqIndex;
            IsGrey = isGrey;
        }

        static readonly XmlSerializer intSerializer;
        static readonly XmlSerializer boolSerializer;
        static readonly XmlSerializer strSerializer;

        static HyImage()
        {
            intSerializer = new XmlSerializer(typeof(int));
            boolSerializer = new XmlSerializer(typeof(bool));
            strSerializer = new XmlSerializer(typeof(string));
        }


        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            //reader.Read();

            //reader.ReadStartElement(nameof(Width));
            //Width = (int)intSerializer.Deserialize(reader);
            //reader.ReadEndElement();

            //reader.ReadStartElement(nameof(Height));
            //Height = (int)intSerializer.Deserialize(reader);
            //reader.ReadEndElement();

            //reader.ReadStartElement(nameof(IsGrey));
            //IsGrey = (bool)boolSerializer.Deserialize(reader);
            //reader.ReadEndElement();


            //reader.ReadStartElement(nameof(Image));
            ////update by He @ 20210913 加一个判断图像是否为null
            //if (Image != null)
            //{
            //    string base64Str = (string)strSerializer.Deserialize(reader);
            //    Image = ImageUtils.Base64StringToBitmap(IsGrey, Width, Height, base64Str);
            //}
            //reader.ReadEndElement();

            //reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            //writer.WriteStartElement(nameof(Width));
            //intSerializer.Serialize(writer, Width);
            //writer.WriteEndElement();

            //writer.WriteStartElement(nameof(Height));
            //intSerializer.Serialize(writer, Height);
            //writer.WriteEndElement();

            //writer.WriteStartElement(nameof(IsGrey));
            //boolSerializer.Serialize(writer, IsGrey);
            //writer.WriteEndElement();

            //writer.WriteStartElement(nameof(Image));
            ////update by He @ 20210913 加一个判断图像是否为null
            //if (Image != null)
            //{
            //    strSerializer.Serialize(writer, ImageUtils.BitmapToBase64String(Image));
            //}
            //writer.WriteEndElement();
        }
    }
}
