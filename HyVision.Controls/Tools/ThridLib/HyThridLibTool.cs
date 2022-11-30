using HyVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HyVision.Tools.ThridLib
{
    [Serializable]
    public class HyThridLibTool : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(HyThridLibToolEdit);

        string m_DllName;
        /// <summary>
        /// 要运行的方法所在 DLL 的名称
        /// <para>此 DLL 必须放在程序目录下</para>
        /// </summary>
        public string DllName
        {
            get { return m_DllName; }
            set
            {
                if (m_DllName != value)
                {
                    m_DllName = value;

                    m_ClassName = null;
                    m_FuncName = null;
                    type = null;
                    func = null;

                    if (m_DllName != null)
                    {
                        string filename = PathUtils.CurrentDirectory + "\\" + m_DllName;
                        if (File.Exists(filename))
                        {
                            assembly = Assembly.LoadFrom(filename);
                        }
                    }
                }
            }
        }

        string m_ClassName;
        /// <summary>
        /// 要运行方法所在的类名
        /// </summary>
        public string ClassName
        {
            get { return m_ClassName; }
            set
            {
                if (m_ClassName != value)
                {
                    m_ClassName = value;

                    m_FuncName = null;
                    func = null;

                    if (assembly != null)
                    {
                        type = assembly.GetType(value);
                    }
                }
            }
        }

        string m_FuncName;
        /// <summary>
        /// 要运行的方法名称
        /// <para>方法有且仅有一个 Dictionary&lt;string, object&gt; 类型的参数，并且返回值类型为 Dictionary&lt;string, object&gt;</para>
        /// </summary>
        public string FuncName
        {
            get { return m_FuncName; }
            set
            {
                if (m_FuncName != value)
                {
                    m_FuncName = value;

                    if (type != null)
                    {
                        MethodInfo method = type.GetMethod(value, new Type[] { typeof(Dictionary<string, object>) });
                        if (method != null)
                        {
                            if (method.IsStatic)
                            {
                                func = (Func<Dictionary<string, object>, Dictionary<string, object>>)method.CreateDelegate(typeof(Func<Dictionary<string, object>, Dictionary<string, object>>));
                            }
                            else
                            {
                                object c = Activator.CreateInstance(type);

                                func = (Func<Dictionary<string, object>, Dictionary<string, object>>)method.CreateDelegate(typeof(Func<Dictionary<string, object>, Dictionary<string, object>>), c);
                            }
                        }
                    }
                }
            }
        }

        Assembly assembly;
        Type type;
        Func<Dictionary<string, object>, Dictionary<string, object>> func;

        public HyThridLibTool()
        {

        }

        public HyThridLibTool(string name)
        {
            Name = name;
        }

        static readonly Type ImageType = typeof(HyImage);

        // update by LuoDian @ 20211214 添加一个参数，用于区分不同的子料号，加载对应子料号的参数
        protected override void Run2(string subName)
        {
            if (DllName == null || assembly == null)
                OnException($"运行[{Name}]时发生异常", new HyVisionException("未指定要调用的 DLL 或未找到指定的 DLL"));

            if (ClassName == null || type == null)
                OnException($"运行[{Name}]时发生异常", new HyVisionException("未指定要调用的类或未找到指定的类"));

            if (FuncName == null || func == null)
                OnException($"运行[{Name}]时发生异常", new HyVisionException("未指定要调用的方法或未找到指定的方法"));

            Dictionary<string, object> parameters = new Dictionary<string, object>(Inputs.Count * 2);
            foreach (HyTerminal input in Inputs)
            {
                if (input.ValueType == ImageType)
                    parameters.Add(input.Name, (input.Value as HyImage)?.Image);
                else
                    parameters.Add(input.Name, input.Value);
            }


            try
            {
                Dictionary<string, object> result = func(parameters);

                foreach (HyTerminal output in Outputs)
                {
                    if (result.ContainsKey(output.Name))
                        output.Value = result[output.Name];
                }

                OnRan();
            }
            catch (Exception e)
            {
                OnException($"调用[{m_DllName}]中方法时发生异常", e);
            }
        }

        internal Type[] GetClasses()
        {
            return assembly?.GetTypes()
                .Where(t => t.IsPublic && t.IsClass && !t.IsNested && !t.IsSubclassOf(typeof(Delegate)))
                .ToArray();
        }

        internal string[] GetFuncs()
        {
            return type?.GetMembers(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .Where(m => m.MemberType == MemberTypes.Method && !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_"))
                .Select(m => m.Name).ToArray();
        }

        public override object Clone(bool containsData)
        {
            HyThridLibTool thridLibTool = new HyThridLibTool();
            foreach (HyTerminal input in Inputs)
            {
                thridLibTool.Inputs.Add((HyTerminal)input.Clone(containsData));
            }

            foreach (HyTerminal output in Outputs)
            {
                thridLibTool.Outputs.Add((HyTerminal)output.Clone(containsData));
            }

            thridLibTool.DllName = DllName;
            thridLibTool.ClassName = ClassName;
            thridLibTool.FuncName = FuncName;

            return thridLibTool;
        }

        protected override void Dispose(bool disposing)
        {

        }

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            
        }
    }
}
