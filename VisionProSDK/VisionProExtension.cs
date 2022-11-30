using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VisionSDK._VisionPro
{
    public static class VisionProExtension
    {
        #region 输入输出

        public static void AddInput(this CogToolBlock toolBlock, ParamInfo input)
        {
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(input.Name, input.Type)
            {
                Description = input.Description,
                Value = input.Value?.ChanageType(input.Type)
            };

            toolBlock.Inputs.Add(terminal);
        }

        public static void SetInputValue(this CogToolBlock toolBlock, string paramName, object value)
        {
            CogToolBlockTerminal terminal = toolBlock.Inputs[paramName];
            terminal.Value = value;
        }

        public static void AddOutput(this CogToolBlock toolBlock, ParamInfo input)
        {
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(input.Name, input.Type)
            {
                Description = input.Description,
                Value = input.Value?.ChanageType(input.Type)
            };

            toolBlock.Outputs.Add(terminal);
        }

        public static LinkedDictionary<string, object> GetOutputValues(this CogToolBlock toolBlock)
        {
            LinkedDictionary<string, object> output = new LinkedDictionary<string, object>();

            foreach (CogToolBlockTerminal terminal in toolBlock.Outputs)
            {
                output.Add(terminal.Name, terminal.Value);
            }

            return output;
        }

        #endregion

        #region NPointToNPoint

        /// <summary>
        /// 清空 NPointToNPoint 中的点对
        /// </summary>
        public static void ClearPointPair(this CogCalibNPointToNPointTool nPointToNPoint)
        {
            int PointPairCount = nPointToNPoint.Calibration.NumPoints;
            while (PointPairCount > 0)
            {
                nPointToNPoint.Calibration.DeletePointPair(--PointPairCount);
            }
        }

        public static void Calibration(this CogCalibNPointToNPointTool nPointToNPoint,
            List<(double X1, double Y1, double X2, double Y2)> points)
        {
            nPointToNPoint.ClearPointPair();

            nPointToNPoint.Calibration.NumPoints = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                nPointToNPoint.Calibration.SetUncalibratedPoint(i, points[i].X1, points[i].Y1);
                nPointToNPoint.Calibration.SetRawCalibratedPoint(i, points[i].X2, points[i].Y2);
            }

            nPointToNPoint.Calibration.DOFsToCompute = CogNPointToNPointDOFConstants.ScalingAspectRotationSkewAndTranslation;

            nPointToNPoint.Calibration.Calibrate();
        }

        #endregion

        /// <summary>
        /// 获取 CogToolBlockEditV2 中的 CogRecordDisplay
        /// </summary>
        public static CogRecordDisplay GetRecordDisplay(this CogToolBlockEditV2 toolBlockEditV2)
        {
            Control[] controls = toolBlockEditV2.Controls.Find("recordDisplay", true);

            return (CogRecordDisplay)controls[0];
        }
    }
}
