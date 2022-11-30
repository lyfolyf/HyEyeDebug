using HyVision.Tools.XMLEdit.DA;
using LeadHawkCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyVision.Tools.XMLEdit.BL
{
    public class XMLEditBL : BaseHyUserTool
    {
        private XD_PhaseDeflectionPara xmlData;
        public XD_PhaseDeflectionPara XmlData { get => xmlData; set => xmlData = value; }

        private string filePath;
        public string FilePath { get => filePath; set => filePath = value; }

        private XD_PhaseDeflectionRun PhaseRun = new XD_PhaseDeflectionRun();

        public override Type ToolEditType => typeof(XMLEditUI);

        public override object Clone(bool containsData)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        protected override void Run2(string subName)
        {
            throw new NotImplementedException();
        }

        public int ReadXMLData(string folderPath)
        {
            byte[] pPath = System.Text.Encoding.Default.GetBytes(folderPath);
            int nReturn = PhaseRun.GetParaByPath(pPath);
            if (nReturn == 1 || nReturn == 2)
                return nReturn;

            xmlData = PhaseRun.m_Para;
            return 0;
        }

        public bool SaveXMLData(string xmlFilePath)
        {
            byte[] pPath = System.Text.Encoding.Default.GetBytes(xmlFilePath);
            return PhaseRun.SaveParaToXML(pPath);
        }

        /// <summary>
        /// ���ߵĳ�ʼ��
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// ���ߵı���ӿڣ��еĹ����ڱ������֮����Ҫ���³�ʼ�����������������ӿ����渴λ��ʼ����״̬
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            
        }
    }
}
