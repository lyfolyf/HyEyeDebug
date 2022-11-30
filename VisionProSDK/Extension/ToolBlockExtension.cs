using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;

namespace VisionSDK._VisionPro.Extension
{
    /// <summary>
    /// ToolBlock 扩展方法
    /// </summary>
    public static class ToolBlockExtension
    {
        // 这个方法要推敲一下，类型转换是否应该在 ParamInfo 中进行
        public static void AddInput(this CogToolBlock toolBlock, ParamInfo input)
        {
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(input.Name, input.Type)
            {
                Description = input.Description,
                Value = input.Value?.ChanageType(input.Type)
            };

            toolBlock.Inputs.Add(terminal);
        }

        public static void AddInput(this CogToolBlock toolBlock, string inputName, Type inputType)
        {
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(inputName, inputType);

            toolBlock.Inputs.Add(terminal);
        }

        public static void AddInputs(this CogToolBlock toolBlock, string[] inputNames, Type inputType)
        {
            foreach (string inputName in inputNames)
            {
                CogToolBlockTerminal terminal = new CogToolBlockTerminal(inputName, inputType);

                toolBlock.Inputs.Add(terminal);
            }
        }

        public static void AddOutput(this CogToolBlock toolBlock, string outputName, Type outputType)
        {
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(outputName, outputType);

            toolBlock.Outputs.Add(terminal);
        }

        public static void AddOutputs(this CogToolBlock toolBlock, string[] outputNames, Type outputType)
        {
            foreach (string outputName in outputNames)
            {
                CogToolBlockTerminal terminal = new CogToolBlockTerminal(outputName, outputType);

                toolBlock.Outputs.Add(terminal);
            }
        }

        public static LinkedDictionary<string, object> GetOutputs(this CogToolBlock toolBlock)
        {
            LinkedDictionary<string, object> output = new LinkedDictionary<string, object>();

            foreach (CogToolBlockTerminal terminal in toolBlock.Outputs)
            {
                output.Add(terminal.Name, terminal.Value);
            }

            return output;
        }

        public static LinkedDictionary<string, object> GetErrorOutputs(this CogToolBlock toolBlock)
        {
            LinkedDictionary<string, object> output = new LinkedDictionary<string, object>();

            foreach (CogToolBlockTerminal terminal in toolBlock.Outputs)
            {
                output.Add(terminal.Name, "999");
            }

            return output;
        }
    }
}
