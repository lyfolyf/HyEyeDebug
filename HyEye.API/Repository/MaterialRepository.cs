using AutoMapper;
using GL.Kit.Log;
using GL.Kit.Native;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface IMaterialRepository
    {
        event EventHandler MaterialChanging;

        List<MaterialInfoVO> GetMaterials();

        MaterialInfoVO Add(string name, bool copyCurrent);

        MaterialInfoVO Backup(string name);

        void Delete(string name);

        void Rename(string oldname, string newname);

        bool ForcedExit { get; }

        string CurrMaterial { get; }

        //add by LuoDian @ 20211209 用于子料号的快速切换
        string CurSubName { get; }

        //add by LuoDian @ 20211215 添加子料号的时候，需要修改Materials
        List<MaterialInfo> GetMaterialsForAddSubName();
        //add by LuoDian @ 20211215 添加一个用于设置子料号的值的接口
        bool ChangeMaterialSubName(string subName);

        bool ChangeMaterial(int index);

        void Save();
    }

    public class MaterialRepository : IMaterialRepository
    {
        public event EventHandler MaterialChanging;

        readonly IMapper mapper;
        readonly IGLog log;

        readonly SetupConfig setup;
        readonly List<MaterialInfo> m_materials;

        readonly string ConfigDirectory;

        //add by LuoDian @ 20211209 用于子料号的快速切换
        public string CurSubName { get { return setup.StartSubMaterial; } }

        public MaterialRepository(IMapper mapper, IGLog log)
        {
            this.mapper = mapper;
            this.log = log;

            ConfigDirectory = PathUtils.CurrentDirectory + "config\\";

            setup = ApiConfig.SetupConfig;
            m_materials = ApiConfig.MaterialConfig.Materials;

            //add by LuoDian @ 20211215 添加初始默认值
            foreach(MaterialInfo material in m_materials)
            {
                if(material.LstSubName == null || material.LstSubName.Count < 1)
                    material.LstSubName = new List<string>();
                if (!material.LstSubName.Contains(CurSubName) && material.Name == CurrMaterial)
                    material.LstSubName.Add(CurSubName);
                else if (material.LstSubName.Count < 1)
                    material.LstSubName.Add("default");
            }

            curIndex = m_materials.Count == 0 ? 1 : m_materials.Max(a => a.Index) + 1;

            InitMaterials();
        }

        int curIndex;

        void InitMaterials()
        {
            bool changed = false;
            string[] materials = Directory.GetDirectories(ConfigDirectory).Select(m => Path.GetFileName(m)).ToArray();

            foreach (string m in materials)
            {
                if (!m_materials.Any(a => a.Name == m))
                {
                    //add by LuoDian @ 20211209 用于子料号的快速切换
                    List<string> lstSubName = new List<string>();
                    lstSubName.Add("default");

                    m_materials.Add(new MaterialInfo { Name = m, Index = curIndex++, LstSubName = lstSubName });

                    changed = true;
                }
            }

            if (changed)
                Save();
        }

        public List<MaterialInfoVO> GetMaterials()
        {
            return mapper.Map<List<MaterialInfoVO>>(m_materials);
        }

        //add by LuoDian @ 20211215 添加子料号的时候，需要修改Materials
        public List<MaterialInfo> GetMaterialsForAddSubName()
        {
            return ApiConfig.MaterialConfig.Materials;
        }

        public MaterialInfoVO Add(string name, bool copyCurrent)
        {
            string path = setup.ConfigRoot + name;

            if (Directory.Exists(path))
            {
                log.Error(new ApiLogMessage("料号", name, A_Add, R_Fail, "料号已存在"));

                throw new ApiException("新增料号失败，料号已存在");
            }
            else
            {
                //add by LuoDian @ 20211209 用于子料号的快速切换
                List<string> lstSubName = new List<string>();

                if (copyCurrent)
                {
                    //add by LuoDian @ 20211209 用于子料号的快速切换
                    lstSubName = m_materials.FirstOrDefault(a => a.Name == CurrMaterial)?.LstSubName;
                    
                    DirectoryUtils.Copy(setup.ConfigRoot + CurrMaterial, path);
                }
                else
                {
                    //add by LuoDian @ 20211209 用于子料号的快速切换
                    lstSubName.Add("default");

                    Directory.CreateDirectory(path);
                }

                MaterialInfo m = new MaterialInfo { Name = name, Index = curIndex++, LstSubName = lstSubName };
                m_materials.Add(m);
                Save();

                log.Info(new ApiLogMessage("料号", name, A_Add, R_Success, $"新建料号[{name}]"));

                return mapper.Map<MaterialInfoVO>(m);
            }
        }

        public MaterialInfoVO Backup(string name)
        {
            string src = setup.ConfigRoot + "\\" + name;

            string des = PathUtils.GetCopyDirectory(src);

            //add by LuoDian @ 20211209 用于子料号的快速切换
            List<string> LstSubName = m_materials.FirstOrDefault(a => a.Name == name)?.LstSubName;

            try
            {
                DirectoryUtils.Copy(src, des);

                //update by LuoDian @ 20211216 备份的名称不能用路径
                string newName = des.Substring(des.LastIndexOf("\\") + 1);
                //MaterialInfo m = new MaterialInfo { Name = des, Index = curIndex++, LstSubName = LstSubName };
                MaterialInfo m = new MaterialInfo { Name = newName, Index = curIndex++, LstSubName = LstSubName };

                m_materials.Add(m);
                Save();

                log.Info(new ApiLogMessage("料号", name, A_Backup, R_Success, $"{name} -> {des}"));

                return mapper.Map<MaterialInfoVO>(m);
            }
            catch (Exception e)
            {
                log.Error(new ApiLogMessage("料号", name, A_Backup, R_Fail, e.Message));

                throw new ApiException("备份料号失败");
            }
        }

        public void Delete(string name)
        {
            string path = setup.ConfigRoot + "\\" + name;

            try
            {
                Directory.Delete(path, true);

                m_materials.Remove(a => a.Name == name);
                Save();

                log.Info(new ApiLogMessage("料号", name, A_Delete, R_Success));
            }
            catch (Exception e)
            {
                log.Error(new ApiLogMessage("料号", name, A_Delete, R_Fail, e.Message));

                throw new ApiException("删除料号失败");
            }
        }

        public void Rename(string oldname, string newname)
        {
            if (oldname == newname) return;

            MaterialInfo material = m_materials.FirstOrDefault(a => a.Name == oldname);

            if (material == null)
            {
                log.Error(new ApiLogMessage("料号", oldname, A_Rename, R_Fail, "料号未找到"));

                throw new ApiException("料号重命名失败，料号未找到");
            }

            if (Directory.Exists(setup.ConfigRoot + newname))
            {
                log.Error(new ApiLogMessage("料号", oldname, A_Add, R_Fail, "料号名称已存在"));

                throw new ApiException("重命名料号失败，料号名称已存在");
            }

            try
            {
                DirectoryUtils.Rename(setup.ConfigRoot + oldname, newname);

                material.Name = newname;
                Save();

                if (oldname == CurrMaterial)
                {
                    setup.StartMaterial = newname;
                }

                log.Info(new ApiLogMessage("料号", oldname, A_Rename, R_Success, $"[{oldname}] -> [{newname}]"));
            }
            catch (Exception e)
            {
                log.Error(new ApiLogMessage("料号", oldname, A_Rename, R_Fail, e.Message));
                throw new ApiException("重命名料号失败：" + e.Message);
            }
        }

        public bool ForcedExit { get; private set; }

        public string CurrMaterial
        {
            get { return setup.StartMaterial; }
        }

        public bool ChangeMaterial(int index)
        {
            MaterialInfo material = m_materials.FirstOrDefault(a => a.Index == index);
            if (material == null)
            {
                log.Error(new ApiLogMessage("料号", null, A_Change, R_Fail, "料号不存在"));

                return false;
            }

            MaterialInfo curMaterial = m_materials.First(a => a.Name == CurrMaterial);
            if (index == curMaterial.Index)
            {
                log.Error(new ApiLogMessage("料号", null, A_Change, R_Fail, "当前料号索引就是 " + index));

                return false;
            }

            if (Exists(material.Name))
            {
                MaterialChanging?.Invoke(this, EventArgs.Empty);

                setup.StartMaterial = material.Name;

                ForcedExit = true;

                ApplicationUtils.RestartProcess();

                return true;
            }
            else
            {
                log.Error(new ApiLogMessage("料号", null, A_Change, R_Fail, "料号不存在"));

                return false;
            }
        }

        //add by LuoDian @ 20211215 添加一个用于设置子料号的值的接口
        public bool ChangeMaterialSubName(string subName)
        {
            List<string> lstSubName = m_materials.Find(a => a.Name == CurrMaterial).LstSubName;
            if(lstSubName == null || !lstSubName.Contains(subName))
            {
                log.Error(new ApiLogMessage("料号", null, A_Change, R_Fail, $"子料号[{subName}]不存在"));
                return false;
            }
            setup.StartSubMaterial = subName;
            return true;
        }

        bool Exists(string meterialName)
        {
            return Directory.Exists("config\\" + meterialName);
        }

        public void Save()
        {
            ApiConfig.Save(ApiConfig.MaterialConfig);
        }
    }
}
