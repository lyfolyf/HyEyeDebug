using HyVision.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HyVision.Tools.TerminalCollection
{
    [Serializable]
    public class HyTerminalCollection : BaseHyCollection, IHyCollection<HyTerminal>, IEnumerable<HyTerminal>, IXmlSerializable
    {
        readonly IList<HyTerminal> InnerList;

        public IHyUserTool Parent { get; internal set; }

        public HyTerminalCollection()
        {
            InnerList = new List<HyTerminal>();
        }

        public HyTerminalCollection(IHyUserTool parent)
        {
            Parent = parent;

            InnerList = new List<HyTerminal>();
        }

        public int Count => InnerList.Count;

        public HyTerminal this[int index]
        {
            get
            {
                return InnerList[index];
            }
            set
            {
                object oldValue = InnerList[index];
                //OnReplacingItem(index, oldValue, value);
                InnerList[index] = value;
                //OnReplacedItem(index, oldValue, value);

                //考虑下是否要释放
            }
        }

        public HyTerminal this[string key]
        {
            get
            {
                HyTerminal terminal = InnerList.FirstOrDefault(a => a.Name == key);

                if (terminal == null)
                    throw new HyVisionException($"集合中不包含 Key = {key} 的值");

                return terminal;
            }
        }

        public bool Contains(string key)
        {
            return InnerList.Any(a => a.Name == key);
        }

        public int IndexOf(string key)
        {
            return InnerList.IndexOf(a => a.Name == key);
        }

        public void Add(HyTerminal terminal)
        {
            Insert(InnerList.Count, terminal);
        }

        public void Insert(int index, HyTerminal terminal)
        {
            if (terminal.GUID == null)
                terminal.GUID = Guid.NewGuid().ToString("N");

            terminal.AttributeChanged += Value_AttributeChanged;

            InnerList.Insert(index, terminal);

            OnInserted(index, terminal);
        }

        public bool Remove(string key)
        {
            int index = InnerList.IndexOf(a => a.Name == key);

            if (index == -1) return false;

            RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= InnerList.Count)
                throw new IndexOutOfRangeException();

            HyTerminal terminal = InnerList[index];
            InnerList.RemoveAt(index);

            terminal.AttributeChanged -= Value_AttributeChanged;

            OnRemoved(index, terminal);
        }

        public void Clear()
        {
            foreach (HyTerminal terminal in InnerList)
            {
                terminal.AttributeChanged -= Value_AttributeChanged;
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

        public IEnumerator<HyTerminal> GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Value_AttributeChanged(object sender, AttributeChangedEventArgs<HyTerminal> e)
        {
            int index = InnerList.IndexOf(e.Value);

            OnItemValueChanged(index, e.Value);
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
                    if (reader.LocalName == "TerminalAssembly")
                    {
                        string assemblyName = reader.GetAttribute("Assembly");
                        Assembly assembly = Assembly.LoadFrom(assemblyName);

                        string typeName = reader.GetAttribute("Type");
                        Type type = assembly.GetType(typeName);

                        reader.Read();
                        PeekWhitespace(reader);

                        HyTerminal terminal = ReadTerminal(reader, type);
                        InnerList.Add(terminal);

                        PeekWhitespace(reader);
                        reader.ReadEndElement();
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

        HyTerminal ReadTerminal(XmlReader reader, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            return (HyTerminal)serializer.Deserialize(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            foreach (HyTerminal terminal in InnerList)
            {
                Type terminalType = terminal.GetType();
                XmlSerializer serializer = new XmlSerializer(terminalType);

                writer.WriteStartElement("TerminalAssembly");
                writer.WriteAttributeString("Assembly", terminalType.Assembly.GetName().Name + ".dll");
                writer.WriteAttributeString("Type", terminalType.FullName);

                serializer.Serialize(writer, terminal, ns);

                writer.WriteEndElement();
            }
        }
    }
}
