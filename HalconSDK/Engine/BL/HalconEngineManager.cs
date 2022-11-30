using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HalconDotNet;




namespace HalconSDK.Engine.BL
{

    [Serializable]
    public class HalconEngineManager
    {

        private HDevEngine hdevEngine;
        private HDevProcedure hdevProcedure;
        private List<string> SelectedFuncNames = new List<string>();
        private Dictionary<string, string> ParamNameMapping = new Dictionary<string, string>();
        private Dictionary<string, object> InputOutputParms = new Dictionary<string, object>();

        //private Dictionary<string, HObject> IconicParams = new Dictionary<string, HObject>();

        private bool _isDebugMode = false;

        private uint _systemWidth = 2000;

        private uint _systemHeight = 2000;


        public bool IsDebugMode
        {
            get
            {
                return _isDebugMode;
            }
            set
            {
                if (_isDebugMode == value)
                {
                    return;
                }

                _isDebugMode = value;

                if (_isDebugMode == true)
                {
                    hdevEngine.StartDebugServer();
                }
                else
                {
                    hdevEngine.StopDebugServer();
                }
            }
        }


        public uint SystemWidth
        {
            get { return _systemWidth; }
            set 
            {
                _systemWidth = value;
                HOperatorSet.SetSystem("width", _systemWidth);
            }
        }

        public uint SystemHeight
        {
            get { return _systemHeight; }
            set
            {
                _systemHeight = value;
                HOperatorSet.SetSystem("height", _systemHeight);
            }
        }


        public HalconEngineManager()
        {
            hdevEngine = new HDevEngine();
            hdevProcedure = new HDevProcedure();
            hdevEngine.SetEngineAttribute("debug_password", "lead");

            HOperatorSet.SetSystem("width", SystemWidth);
            HOperatorSet.SetSystem("height", SystemHeight);
       
        }

        public string InitializeHalconEngine(string HdplFilePath, List<string> SelectedFunNames)
        {
            try
            {
                string FileDir = Path.GetDirectoryName(HdplFilePath);
                hdevEngine.AddProcedurePath(FileDir);


                InputOutputParms.Clear();
                ParamNameMapping.Clear();
                SelectedFuncNames.Clear();
                string FuncParamName = null;

                for (int i = 0; i < SelectedFunNames.Count; i++)
                {
                    SelectedFuncNames.Add(SelectedFunNames[i]);
                    hdevProcedure.LoadProcedure(SelectedFunNames[i]);
                    HDevProcedureCall ProcedureCall = hdevProcedure.CreateCall();

                    HTuple InputIconicParms = hdevProcedure.GetInputIconicParamNames();
                    for (int m = 0; m < InputIconicParms.Length; m++)
                    {
                        FuncParamName = $"{SelectedFunNames[i]},{InputIconicParms[m].S}";

                        HObject Hobj = new HObject();
                        HOperatorSet.GenEmptyObj(out Hobj);
                        InputOutputParms[FuncParamName] = Hobj;
                    }

                    HTuple InputCtrlParms = hdevProcedure.GetInputCtrlParamNames();
                    for (int m = 0; m < InputCtrlParms.Length; m++)
                    {
                        FuncParamName = $"{SelectedFunNames[i]},{InputCtrlParms[m].S}";
                        InputOutputParms[FuncParamName] = new HTuple();
                    }



                    HTuple OutputIconicParms = hdevProcedure.GetOutputIconicParamNames();
                    for (int k = 0; k < OutputIconicParms.Length; k++)
                    {
                        FuncParamName = $"{SelectedFunNames[i]},{OutputIconicParms[k].S}";

                        HObject Hobj = new HObject();
                        HOperatorSet.GenEmptyObj(out Hobj);
                        InputOutputParms[FuncParamName] = Hobj;
                    }

                    HTuple OutputCtrlParms = hdevProcedure.GetOutputCtrlParamNames();
                    for (int k = 0; k < OutputCtrlParms.Length; k++)
                    {
                        FuncParamName = $"{SelectedFunNames[i]},{OutputCtrlParms[k].S}";
                        InputOutputParms[FuncParamName] = new HTuple();
                    }

                    ProcedureCall.Dispose();
                }

            }
            catch (Exception err)
            {
                return err.ToString();
            }

            return "OK";
        }

        public void AddProcedurePath(string HdplFilePath)
        {
            if (!string.IsNullOrEmpty(HdplFilePath))
            {
                string FileDir = Path.GetDirectoryName(HdplFilePath);
                hdevEngine.AddProcedurePath(FileDir);
            }
        }

        public void SetProcedurePath(string HdplFilePath)
        {
            if (!string.IsNullOrEmpty(HdplFilePath))
            {
                string FileDir = Path.GetDirectoryName(HdplFilePath);
                hdevEngine.SetProcedurePath(FileDir);
            }
        }

        public string GetProcedureNames(out string[] FuncNames)
        {
            try
            {
                HTuple names = hdevEngine.GetProcedureNames();
                FuncNames = names.ToSArr();
                return "OK";
            }
            catch (Exception err)
            {
                FuncNames = null;
                return err.ToString();
            }
        }

        public void SetNameMapping(Dictionary<string, string> NameMapping)
        {
            ParamNameMapping.Clear();

            foreach (string name in NameMapping.Keys)
            {
                ParamNameMapping.Add(name, NameMapping[name]);
            }
        }

        public string SetValue(string FuncParamName, object value)
        {
            InputOutputParms[FuncParamName] = value;
            return "OK";
        }

        public string GetValue(string FuncParamName, out object value)
        {
            InputOutputParms.TryGetValue(FuncParamName, out object val);

            if (val != null)
            {
                value = val;
                return "OK";
            }

            value = null;
            return $"找不到 {FuncParamName} 参数！";
        }


        //public void SetIconicParams(string FuncParamName, HObject hObject)
        //{
        //    IconicParams[FuncParamName] = hObject;
        //}

        //public HObject GetIconicParams(string FuncParamName)
        //{
        //    IconicParams.TryGetValue(FuncParamName, out HObject value);

        //    if (value != null)
        //    {
        //        Console.WriteLine($"IconicParams.Count :{IconicParams.Count}");
        //        return value;
        //    }

        //    HObject obj = new HObject();
        //    obj.GenEmptyObj();
        //    return obj;
        //}

        public string RunInitFunction()
        {
            List<string> Init_Funstions = SelectedFuncNames.FindAll(s => s.StartsWith("Init"));

            return RunFunction(Init_Funstions);
        }

        public string RunFunctionsExceptInit()
        {
            List<string> Funstions = SelectedFuncNames.FindAll(s => !s.StartsWith("Init"));

            return RunFunction(Funstions);
        }

        private string RunFunction(List<string> SelectedFunNames)
        {
            string FuncParamName = "";
            try
            {
                for (int i = 0; i < SelectedFunNames.Count; i++)
                {
                    hdevProcedure.LoadProcedure(SelectedFunNames[i]);
                    HDevProcedureCall ProcedureCall = hdevProcedure.CreateCall();

                    //1.赋值
                    HTuple InputIconicParms = hdevProcedure.GetInputIconicParamNames();
                    for (int j = 0; j < InputIconicParms.Length; j++)
                    {
                        FuncParamName = $"{SelectedFunNames[i]},{InputIconicParms[j].S}";
                        ParamNameMapping.TryGetValue(FuncParamName, out string NewName);
                        if (string.IsNullOrEmpty(NewName))
                        {
                            ProcedureCall.SetInputIconicParamObject(InputIconicParms[j].S, InputOutputParms[FuncParamName] as HObject);
                        }
                        else
                        {
                            ProcedureCall.SetInputIconicParamObject(InputIconicParms[j].S, InputOutputParms[NewName] as HObject);
                        }
                    }

                    HTuple InputCtrlParms = hdevProcedure.GetInputCtrlParamNames();
                    for (int j = 0; j < InputCtrlParms.Length; j++)
                    {
                        FuncParamName = $"{SelectedFunNames[i]},{InputCtrlParms[j].S}";
                        ParamNameMapping.TryGetValue(FuncParamName, out string NewName);
                        if (string.IsNullOrEmpty(NewName))
                        {
                            ProcedureCall.SetInputCtrlParamTuple(InputCtrlParms[j].S, InputOutputParms[FuncParamName] as HTuple);
                        }
                        else
                        {
                            ProcedureCall.SetInputCtrlParamTuple(InputCtrlParms[j].S, InputOutputParms[NewName] as HTuple);
                        }
                    }
                    //2.运行
                    if (IsDebugMode == true)
                    {
                        ProcedureCall.SetWaitForDebugConnection(true);
                    }
                    ProcedureCall.Execute();

                    //3.取值
                    HTuple OutputIconicParms = hdevProcedure.GetOutputIconicParamNames();
                    for (int k = 0; k < OutputIconicParms.Length; k++)
                    {
                        FuncParamName = $"{SelectedFunNames[i]},{OutputIconicParms[k].S}";
                        InputOutputParms[FuncParamName] = ProcedureCall.GetOutputIconicParamObject(OutputIconicParms[k].S);
                    }

                    HTuple OutputCtrlParms = hdevProcedure.GetOutputCtrlParamNames();
                    for (int k = 0; k < OutputCtrlParms.Length; k++)
                    {
                        FuncParamName = $"{SelectedFunNames[i]},{OutputCtrlParms[k].S}";
                        InputOutputParms[FuncParamName] = ProcedureCall.GetOutputCtrlParamTuple(OutputCtrlParms[k].S);
                    }

                    ProcedureCall.Dispose();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine($" 运行函数异常，函数名： {FuncParamName} 错误信息：{err}");
                return $"执行Halcon函数出错，函数名和参数名为：{FuncParamName}\n\r{err}";
            }
            return "OK";
        }

        public string RunFunction(string SelectedFunName)
        {
            try
            {
                string FuncParamName = null;
                hdevProcedure.LoadProcedure(SelectedFunName);
                HDevProcedureCall ProcedureCall = hdevProcedure.CreateCall();

                //1.赋值
                HTuple InputIconicParms = hdevProcedure.GetInputIconicParamNames();
                for (int j = 0; j < InputIconicParms.Length; j++)
                {
                    FuncParamName = $"{SelectedFunName},{InputIconicParms[j].S}";

                    ProcedureCall.SetInputIconicParamObject(InputIconicParms[j].S, InputOutputParms[FuncParamName] as HObject);
                }

                HTuple InputCtrlParms = hdevProcedure.GetInputCtrlParamNames();
                for (int j = 0; j < InputCtrlParms.Length; j++)
                {
                    FuncParamName = $"{SelectedFunName},{InputCtrlParms[j].S}";

                    ProcedureCall.SetInputCtrlParamTuple(InputCtrlParms[j].S, InputOutputParms[FuncParamName] as HTuple);
                }

                //2.运行
                if (IsDebugMode == true)
                {
                    ProcedureCall.SetWaitForDebugConnection(true);
                }
                ProcedureCall.Execute();

                //3.取值
                HTuple OutputIconicParms = hdevProcedure.GetOutputIconicParamNames();
                for (int k = 0; k < OutputIconicParms.Length; k++)
                {
                    FuncParamName = $"{SelectedFunName},{OutputIconicParms[k].S}";
                    InputOutputParms[FuncParamName] = ProcedureCall.GetOutputIconicParamObject(OutputIconicParms[k].S);
                }

                HTuple OutputCtrlParms = hdevProcedure.GetOutputCtrlParamNames();
                for (int k = 0; k < OutputCtrlParms.Length; k++)
                {
                    FuncParamName = $"{SelectedFunName},{OutputCtrlParms[k].S}";
                    InputOutputParms[FuncParamName] = ProcedureCall.GetOutputCtrlParamTuple(OutputCtrlParms[k].S);
                }

                ProcedureCall.Dispose();
            }
            catch (Exception err)
            {

                return err.ToString();
            }
            return null;
        }


    }
}
