using HyVision.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HyVision.Tools
{
    [Serializable]
    [XmlInclude(typeof(ThridLib.HyThridLibTool))]
    public class HyToolCollection : BaseHyCollection, IHyCollection<IHyUserTool>, IEnumerable<IHyUserTool>, IXmlSerializable, ICloneable
    {
        //add by LuoDian @ 20211213 用于子料号的快速切换
        private string materialSubName = "default";
        [System.Xml.Serialization.XmlElementAttribute("MaterialSubName")]
        public string MaterialSubName { get => materialSubName; set => materialSubName = value; }


        readonly IList<object> InnerList;

        public HyToolCollection()
        {
            InnerList = new List<object>();
        }

        public int Count => InnerList.Count;

        public IHyUserTool this[int index]
        {
            get
            {
                return (IHyUserTool)InnerList[index];
            }
            set
            {
                IHyUserTool oldValue = (IHyUserTool)InnerList[index];
                //OnReplacingItem(index, oldValue, value);
                InnerList[index] = value;

                //OnReplacedItem(index, oldValue, value);

                //这里要考虑下
                //oldValue.Dispose();
            }
        }

        public IHyUserTool this[string key]
        {
            get
            {
                IHyUserTool hyUserTool = (IHyUserTool)InnerList.FirstOrDefault(a => ((IHyUserTool)a).Name == key);

                if (hyUserTool == null)
                    throw new HyVisionException($"集合中不包含 Key = {key} 的值");

                return hyUserTool;
            }
        }

        public bool Contains(string key)
        {
            return InnerList.Any(a => ((IHyUserTool)a).Name == key);
        }

        public void Add(object item)
        {
            if (item is IHyUserTool hyTool)
                Add(hyTool);
            else
                throw new HyVisionException("无效的项");
        }

        public void Add(IHyUserTool item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                Type type = item.GetType();
                int index = 1;
                string name = type.Name + index.ToString();
                while (Contains(name))
                {
                    index++;
                    name = type.Name + index.ToString();
                }
                item.Name = name;
            }

            int count = InnerList.Count;

            Insert(InnerList.Count, item);
        }

        public void Insert(int index, IHyUserTool item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            ((BaseHyUserTool)item).Exception += OnException;
            item.NameChanged += Item_NameChanged;

            InnerList.Add(item);
            OnInserted(index, item);
        }

        public bool Remove(string key)
        {
            int index = InnerList.IndexOf(a => ((IHyUserTool)a).Name == key);

            if (index == -1) return false;

            RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= InnerList.Count)
                throw new IndexOutOfRangeException();

            object tool = InnerList[index];
            InnerList.RemoveAt(index);

            OnRemoved(index, tool);

            ((IHyUserTool)tool).NameChanged -= Item_NameChanged;
        }

        public void Clear()
        {
            foreach (object tool in InnerList)
            {
                ((IHyUserTool)tool).NameChanged -= Item_NameChanged;
            }

            InnerList.Clear();

            OnCleared();
        }

        public bool MoveUp(int index)
        {
            if (index < 0 || index >= InnerList.Count) throw new IndexOutOfRangeException();

            if (index == 0) return false;

            int toIndex = index - 1;

            InnerList.Exchange(index, toIndex);

            OnMoved(index, toIndex);

            return true;
        }

        public bool MoveDown(int index)
        {
            if (index < 0 || index >= InnerList.Count) throw new IndexOutOfRangeException();

            if (index == InnerList.Count - 1) return false;

            int toIndex = index + 1;

            InnerList.Exchange(index, toIndex);

            OnMoved(index, toIndex);

            return true;
        }

        public IEnumerator GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        IEnumerator<IHyUserTool> IEnumerable<IHyUserTool>.GetEnumerator()
        {
            foreach (IHyUserTool item in InnerList)
            {
                yield return item;
            }
        }

        private void Item_NameChanged(object sender, ValueChangedEventArgs<string> e)
        {
            int index = InnerList.IndexOf(t => ((IHyUserTool)t).Name == e.NewValue);

            OnItemValueChanged(index, InnerList[index]);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                reader.Read();
                return;
            }

            reader.Read();

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                PeekWhitespace(reader);

                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.LocalName == "ToolAssembly")
                    {
                        string assemblyName = reader.GetAttribute("Assembly");
                        Assembly assembly = Assembly.LoadFrom(assemblyName);

                        string typeName = reader.GetAttribute("Type");
                        Type type = assembly.GetType(typeName);

                        reader.Read();
                        PeekWhitespace(reader);

                        IHyUserTool tool = ReadTool(reader, type);
                        InnerList.Add(tool);

                        PeekWhitespace(reader);

                        reader.ReadEndElement();
                    }

                    //add by LuoDian @ 20211215 读取XML的时候，需要读取子料号的信息
                    else if (reader.LocalName == "MaterialSubName")
                    {
                        reader.Read();
                        PeekWhitespace(reader);

                        XmlSerializer serializer = new XmlSerializer(MaterialSubName.GetType());
                        MaterialSubName = string.IsNullOrEmpty(reader.Value) ? "default" : reader.Value;

                        reader.Read();
                        reader.ReadEndElement();
                        reader.Read();
                    }

                    else
                    {
                        throw new HyVisionException("配置文件解析失败");
                    }
                }
            }

            reader.ReadEndElement();
        }

        void PeekWhitespace(XmlReader reader)
        {
            while (reader.NodeType == XmlNodeType.Whitespace)
            {
                reader.Read();
            }
        }

        IHyUserTool ReadTool(XmlReader reader, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            return (IHyUserTool)serializer.Deserialize(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            //add by LuoDian @ 20211215 保存XML的时候，需要把子料号的信息写进去
            writer.WriteElementString("MaterialSubName", MaterialSubName);

            foreach (object tool in InnerList)
            {
                Type toolType = tool.GetType();
                XmlSerializer serializer = new XmlSerializer(toolType);

                writer.WriteStartElement("ToolAssembly");
                writer.WriteAttributeString("Assembly", toolType.Assembly.GetName().Name + ".dll");
                writer.WriteAttributeString("Type", toolType.FullName);

                serializer.Serialize(writer, tool, ns);
                writer.WriteEndElement();
            }
        }

        //add by LuoDian @ 20211215 为了添加子料号的时候，从现有子料号中拷贝
        public HyToolCollection Clone()
        {
            return (HyToolCollection)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
