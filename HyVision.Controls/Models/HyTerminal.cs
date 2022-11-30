using System;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace HyVision.Models
{
    [Serializable]
    [XmlInclude(typeof(HyImage))]
    public class HyTerminal : IHyCloneable, IInternalResourcesDisposable
    {
        public event EventHandler<AttributeChangedEventArgs<HyTerminal>> AttributeChanged;

        string name;
        object mValue;
        Type valueType;
        string valueTypeString;
        string description;

        //add by LuoDian @ 20210722 解决HyHalconTerminal序列化的问题
        public string ConvertTargetType { get; set; }

        public HyTerminal()
        {

        }

        public HyTerminal(string name, Type valueType)
        {
            this.name = name;
            ValueType = valueType;
        }

        /// <summary>
        /// 内部使用属性
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// 内部使用属性
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 用来区分当前工具是那个子料号的
        /// </summary>
        public string MaterialSubName { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;

                    OnAttributeChanged();
                }
            }
        }

        // update by LuoDian @ 20210918 把图像数据序列出去后，一个是会造成XML文件很大，浏览很慢，第二个是有的时候会因图像数据异常导致序列化有问题，会导致ToolBlock反序列化失败
        [XmlElement(Type = typeof(HyVision.Tools.ImageDisplay.HyDefectRegion))]
        [XmlElement(Type = typeof(HyVision.Tools.ImageDisplay.HyDefectXLD))]
        [XmlElement(Type = typeof(HyVision.Tools.ImageDisplay.BaseHyROI))]
        [XmlElement(Type = typeof(List<HyVision.Tools.ImageDisplay.HyDefectXLD>))]
        //[XmlElement(Type = typeof(IntPtr))]
        [XmlElement(Type = typeof(HyRoiManager.RoiData))]
        [XmlElement(Type = typeof(HyImage))]
        [XmlElement(Type = typeof(Bitmap))]
        [XmlElement(Type = typeof(int))]
        [XmlElement(Type = typeof(double))]
        [XmlElement(Type = typeof(bool))]
        [XmlElement(Type = typeof(string))]
        [XmlElement(Type = typeof(List<HyImage>))]
        [XmlElement(Type = typeof(List<Bitmap>))]
        [XmlElement(Type = typeof(List<int>))]
        [XmlElement(Type = typeof(List<double>))]
        [XmlElement(Type = typeof(List<bool>))]
        [XmlElement(Type = typeof(List<string>))]
        [XmlElement(Type = typeof(char))]
        [XmlElement(Type = typeof(TimeSpan))]
        [XmlElement(Type = typeof(DateTime))]
        // add by LuoDian @ 20210918 把图像数据序列出去后，一个是会造成XML文件很大，浏览很慢，第二个是有的时候会因图像数据异常导致序列化有问题，会导致ToolBlock反序列化失败
        //[XmlIgnore]
        public object Value
        {
            get { return mValue; }
            set
            {
                //update by LuoDian @ 20210812 为了在运行完ToolBlock之后，对所有的Inputs进行复位，防止上次运行的图像对下次运行造成影响，添加可以把值设置为null的逻辑
                if (value == null)
                {
                    mValue = value;
                }
                else if (!object.Equals(mValue, value))
                {
                    if (ValueType == null)
                    {
                        ValueType = value.GetType();
                    }
                    if (value.GetType() == ValueType)
                    {
                        mValue = value;

                        OnAttributeChanged();
                    }
                    //add by LuoDian @ 20210810 为了支持扩展输出类型，添加父类子类类型的比较，即输入输出传递值的时候，可以支持子类与父类的值互相传递
                    else if (ValueType.IsSubclassOf(value.GetType()) || value.GetType().IsSubclassOf(ValueType))
                    {
                        mValue = value;
                        OnAttributeChanged();
                    }

                    else
                    {
                        throw new HyVisionException("无效的值类型");
                    }
                }
            }
        }

        [XmlElement(ElementName = "ValueType")]
        public string ValueTypeString
        {
            get { return valueTypeString; }
            set
            {
                // 只能设置一次
                if (valueTypeString == null)
                {
                    valueTypeString = value;
                    if (valueTypeString == "System.Drawing.Bitmap")
                        ValueType = typeof(Bitmap);
                    else if (valueTypeString == "HyRoiManager.RoiData")
                    {
                        ValueType = typeof(HyRoiManager.RoiData);
                    }
                    else
                        ValueType = Type.GetType(value);
                }
            }
        }

        // Added by louis on Mar. 19 2022 解决收集多张图像再运行时其它参数信息不对应的问题
        [XmlIgnore]
        public IEnumerable<(string Name, object Value)> AttachedParams { get; set; }

        [XmlIgnore]
        public Type ValueType
        {
            get { return valueType; }
            private set
            {
                valueType = value;
                valueTypeString = valueType.FullName;
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;

                    OnAttributeChanged();
                }
            }
        }

        protected virtual void OnAttributeChanged()
        {
            AttributeChanged?.Invoke(this, new AttributeChangedEventArgs<HyTerminal>(this));
        }

        public object Clone(bool containsData)
        {
            return new HyTerminal
            {
                GUID = GUID,
                From = From,
                Name = Name,
                Value = containsData ? Value : null,
                ValueTypeString = ValueTypeString,
                ValueType = ValueType,
                Description = Description
            };
        }

        public void DisposeInternalResources()
        {
            (Value as IDisposable)?.Dispose();
        }
    }

    public static class HyTerminalUtils
    {
        public static string GetToolTip(this HyTerminal terminal)
        {
            string typeName = terminal.ValueType?.Name;
            if (typeName.StartsWith("List"))
            {
                typeName = string.Format("List<{0}>", terminal.ValueType?.GenericTypeArguments[0].Name);
            }
            return terminal.Value?.ToString() ?? $"{typeName ?? terminal.ValueTypeString}";
        }
    }
}
