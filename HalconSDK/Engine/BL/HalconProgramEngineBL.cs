using HalconSDK.Engine.UI;
using HyVision.Tools;
using HyVision.Tools.ImageDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyVision.Models;
using HalconDotNet;
using System.Drawing;
using HyVision;
using System.Drawing.Imaging;
using System.Xml.Serialization;



namespace HalconSDK.Engine.BL
{

    [Serializable]
    public class HalconProgramEngineBL : BaseHyUserTool
    {

        public override Type ToolEditType => typeof(HalconEngineUI_1);

        public string HalconFilePath { get; set; }

        [XmlIgnore]
        public HyImage ShowImage { get; set; }

        public List<BaseHyROI> HyROIs { get; set; } = new List<BaseHyROI>();

        public List<SettingsBL> Settings { get; set; } = new List<SettingsBL>();


        //存放Halcon引擎所有变量信息，包括每个算子（一级算子）的输入和输出
        public List<HyTerminal> AllParms { get; set; } = new List<HyTerminal>();

        //存放Halcon程序中间变量，界面配置的参数，需要连线的参数保存在Inputs和Outputs
        //根据AllParms来初始化
        [XmlIgnore]
        public Dictionary<string, HObject> Val1 { get; set; } = new Dictionary<string, HObject>();

        [XmlIgnore]
        public Dictionary<string, HTuple> Val2 { get; set; } = new Dictionary<string, HTuple>();

        [XmlIgnore]
        public Dictionary<string, string> ParamNameMapping { get; set; } = new Dictionary<string, string>();

        [XmlIgnore]
        private  HDevProgram hDevProgram = new HDevProgram();

        protected override void Run2(string subName)
        {
            try
            {
                if (Inputs != null && !string.IsNullOrEmpty(HalconFilePath))
                {
                    //HDevProgram hDevProgram = new HDevProgram();
                    hDevProgram.LoadProgram(HalconFilePath);
                    HalconDataConvert dataConvert = new HalconDataConvert();

                    //1.从Inputs或者预设值中设置Halcon引擎输入参数的值（输出参数不用设置）
                    for (int i = 0; i < Inputs.Count; i++)
                    {
                        string[] InputInfo = Inputs[i].Description.Split(',');
                        HDevProcedure hDevProcedure = new HDevProcedure(hDevProgram, InputInfo[1]);
                        HDevProcedureCall hDevProcedureCall = hDevProcedure.CreateCall();
                        //输入端有连线了，用连线过来的值作为输入值
                        if (!string.IsNullOrEmpty(Inputs[i].From))
                        {
                            if (InputInfo[4] == "Image")
                            {
                                HObject value = dataConvert.ConvertToHObject(Inputs[i]);
                                hDevProcedureCall.SetInputIconicParamObject(InputInfo[3], value);
                            }
                            else if (InputInfo[4] == "Region")
                            {
                                HObject value = dataConvert.HyDefectRegionToHobject((HyDefectRegion)Inputs[i].Value);
                                hDevProcedureCall.SetInputIconicParamObject(InputInfo[3], value);
                            }
                            else if (InputInfo[4] == "XLD")
                            {

                            }
                            else
                            {
                                HTuple hTuple = dataConvert.ConvertToHTuple(Inputs[i]);
                                hDevProcedureCall.SetInputCtrlParamTuple(InputInfo[3], hTuple);
                            }
                        }
                        //输入端没有连线，用原来设置好的值作为输入值
                        else
                        {
                            if (InputInfo[4] == "Image")
                            {

                                //Bitmap bmp = ((HyImage)Inputs[i].Value).Image;
                                //Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                                //BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                                //HOperatorSet.GenImage1(out HObject image, "byte", bitmapData.Width, bitmapData.Height, bitmapData.Scan0);


                                HOperatorSet.ReadImage(out HObject Img, "C:\\Users\\29092\\Desktop\\Test Images\\Tbmp.bmp");

                                hDevProcedureCall.SetInputIconicParamObject(InputInfo[3], Img);
                                //bmp.UnlockBits(bitmapData);


                                //HObject value = dataConvert.ConvertToHObject(Inputs[i]);    //这个转换有问题
                                //hDevProcedureCall.SetInputIconicParamObject(InputInfo[3], value);
                            }
                            else if (InputInfo[4] == "Region")
                            {
                                SettingsBL RegionSetting = Settings.FirstOrDefault(s =>
                                s.FunName == InputInfo[1] && s.ParmName == InputInfo[3]);
                                hDevProcedureCall.SetInputIconicParamObject(InputInfo[3], RegionSetting.ResultHObjectROI);
                            }
                            else
                            {
                                HTuple hTuple = dataConvert.ConvertToHTuple(Inputs[i]);
                                hDevProcedureCall.SetInputCtrlParamTuple(InputInfo[3], hTuple);
                            }
                        }
                    }
                    //2.运行程序
                    HDevProgramCall hDevProgramCall = hDevProgram.Execute();
                    hDevProgramCall.Execute();
                    //3.获取输出参数的值，赋值到Outputs
                    if (Outputs != null)
                    {
                        for (int i = 0; i < Outputs.Count; i++)
                        {
                            string[] OutputInfo = Outputs[i].Description.Split(',');
                            HDevProcedure hDevProcedure = new HDevProcedure(hDevProgram, OutputInfo[1]);
                            HDevProcedureCall hDevProcedureCall = hDevProcedure.CreateCall();



                            if (OutputInfo[4] == HalconDataType.Image.ToString())
                            {
                                HObject value = hDevProcedureCall.GetOutputIconicParamObject(OutputInfo[3]);
                                Bitmap bitMap = dataConvert.HObject2Bitmap8(value);
                                HyImage image = new HyImage(bitMap, true);
                                Outputs[i].Value = image;
                            }
                            else if (OutputInfo[4] == HalconDataType.Region.ToString())
                            {

                                HObject v1 = hDevProgramCall.GetIconicVarObject(OutputInfo[3]);

                                HObject value = hDevProcedureCall.GetOutputIconicParamObject(OutputInfo[3]);
                                HyDefectRegion hyDefectRegion = dataConvert.HobjectToHyDefectRegion(value);

                                Outputs[i].Value = hyDefectRegion;
                            }
                            else if (OutputInfo[4] == HalconDataType.XLD.ToString())
                            {
                                HXLD value = (HXLD)hDevProcedureCall.GetOutputIconicParamObject(OutputInfo[3]);
                                HyDefectXLD hyDefectXLD = dataConvert.HXLDContToHyDefectXLD(value);
                                Outputs[i].Value = hyDefectXLD;
                            }
                            else if (OutputInfo[4] == HalconDataType.Int.ToString())
                            {
                                HTuple hTuple = hDevProcedureCall.GetOutputCtrlParamTuple(OutputInfo[3]);
                                Outputs[i].Value = hTuple.I;
                            }
                            else if (OutputInfo[4] == HalconDataType.Double.ToString())
                            {
                                HTuple v1 = hDevProgramCall.GetCtrlVarTuple(OutputInfo[3]);

                                HTuple hTuple = hDevProcedureCall.GetOutputCtrlParamTuple(OutputInfo[3]);
                                Outputs[i].Value = hTuple.D;
                            }
                            else if (OutputInfo[4] == HalconDataType.Bool.ToString())
                            {
                                HTuple hTuple = hDevProcedureCall.GetOutputCtrlParamTuple(OutputInfo[3]);
                                Outputs[i].Value = hTuple.S;
                            }
                            else if (OutputInfo[4] == HalconDataType.String.ToString())
                            {
                                HTuple hTuple = hDevProcedureCall.GetOutputCtrlParamTuple(OutputInfo[3]);
                                Outputs[i].Value = hTuple.S;
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnException($"运行[{Name}]时发生异常", new HyVisionException(ex.Message));
            }

        }


        protected override void Dispose(bool disposing)
        {

        }

        public override object Clone(bool containsImage)
        {
            throw new NotImplementedException();
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


    public enum HalconDataType
    {
        Int = 1,
        //Float = 2,
        Double = 3,
        //Long = 4,
        Bool = 5,
        String = 6,

        Image = 7,
        Region = 8,
        XLD = 9,
        内部输出 = 10,

        //add by LuoDian @ 20220118 添加一个byte[]类型的输出
        ImagePointer = 11,

        List_Int = 12,
        List_Double = 13,
        List_Bool = 14,
        List_String = 15,
        List_Image = 16,

    }


}
