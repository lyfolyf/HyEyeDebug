using GL.Kit.Log;
using HyVision.Models;
using HyVision.Tools.Classifier.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyVision.Tools.Classifier.BL
{
    [Serializable]
    public class ClassifierBL : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(ClassifierUI);
        LogPublisher log;

        public const string ADVANCE_RULE_KEY_AND = " 并且 ";
        public const string ADVANCE_RULE_KEY_OR = " 或者 ";
        public const string RULE_SUB_KEY = ".";

        private List<(bool, string, string, double)> lstOriginalRuleData;
        public List<(bool, string, string, double)> LstOriginalRuleData { get => lstOriginalRuleData; set => lstOriginalRuleData = value; }

        private List<(bool, string)> lstAdvanceRuleData;
        public List<(bool, string)> LstAdvanceRuleData { get => lstAdvanceRuleData; set => lstAdvanceRuleData = value; }

        private List<(string, string, string)> dicOutputRuleData;
        public List<(string, string, string)> DicOutputRuleData { get => dicOutputRuleData; set => dicOutputRuleData = value; }

        public override object Clone(bool containsData)
        {
            throw new NotImplementedException();
        }

        public override bool Initialize()
        {
            return true;
        }

        public override void Save()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            
        }

        protected override void Run2(string subName)
        {
            try
            {
                Process();
            }
            catch (Exception ex)
            {
                OnException($"分类器 运行出错！错误信息：{ex.Message}", new Exception($"分类器运行失败!"));
            }
        }

        //private void Process()
        //{
        //    if (DicOutputRuleData == null)
        //        OnException($"分类器 模块没有定义输出规则！", new Exception($"分类器 模块运行失败!"));

        //    foreach ((string strOutputName, string strRule, string strNGResult) in DicOutputRuleData)
        //    {
        //        if (string.IsNullOrEmpty(strRule) || string.IsNullOrEmpty(strNGResult))
        //            OnException($"分类器 模块中名称为{strOutputName}的输出未定义的规则或判定结果！", new Exception($"分类器 模块运行失败!"));

        //        bool bResult = true;

        //        //如果是“并且”的情况，则当各个规则的条件都满足的情况下，才输出缺陷类型Result，只要任何一个不满足，就是OK的
        //        string[] arrRuleSplitByAnd = strRule.Split(ClassifierBL.ADVANCE_RULE_KEY_AND.ToCharArray());
        //        if (arrRuleSplitByAnd != null && arrRuleSplitByAnd.Length > 0)
        //        {
        //            //先初始化各个规则的状态
        //            bool[] bAndResult = new bool[arrRuleSplitByAnd.Length];
        //            for (int i = 0; i < bAndResult.Length; i++)
        //                bAndResult[i] = false;

        //            //需要在现在的“并且”规则中，进一步分割出最终的子规则，用于做判定，如果只有一个规则，这个逻辑也是满足的
        //            for (int i = 0; i < arrRuleSplitByAnd.Length; i++)
        //            {
        //                if (string.IsNullOrEmpty(arrRuleSplitByAnd[i]))
        //                    continue;

        //                //如果是“或者”的情况，则只要其中一个规则的条件满足，就更新当前规则的状态
        //                string[] arrRuleSplitByOr = arrRuleSplitByAnd[i].Split(ClassifierBL.ADVANCE_RULE_KEY_OR.ToCharArray());
        //                foreach (string strRuleSplitByOr in arrRuleSplitByOr)
        //                {
        //                    string[] arrRule = strRule.Split(ClassifierBL.RULE_SUB_KEY.ToCharArray());
        //                    if (arrRule == null && arrRule.Length != 3)
        //                        OnException($"分类器 模块中含有不合法的规则：{strRuleSplitByOr}！", new Exception($"分类器 模块运行失败!"));

        //                    if (!Inputs.Contains(arrRule[0]))
        //                        OnException($"分类器 模块中的规则 {strRuleSplitByOr} 中的缺陷的输入名称在输入列表中找不到！", new Exception($"分类器 模块运行失败!"));

        //                    if (string.IsNullOrEmpty(arrRule[1]))
        //                        OnException($"分类器 模块中的规则 {strRuleSplitByOr} 没有设定算术操作符！", new Exception($"分类器 模块运行失败!"));

        //                    double dThreshold = 0;
        //                    if (string.IsNullOrEmpty(arrRule[2]) || !double.TryParse(arrRule[2], out dThreshold))
        //                        OnException($"分类器 模块中的规则 {strRuleSplitByOr} 设定的阈值不合法！", new Exception($"分类器 模块运行失败!"));

        //                    HyTerminal input = Inputs.First(a => a.Name.Equals(arrRule[0]));
        //                    if (input == null || input.Value == null || (input.ValueType != typeof(int) && input.ValueType != typeof(List<int>) &&
        //                        input.ValueType != typeof(double) && input.ValueType != typeof(List<double>)))
        //                        OnException($"分类器 模块中名称为 {arrRule[0]} 的输入没有数据或是数据类型不合法！", new Exception($"分类器 模块运行失败!"));

        //                    //这里处理int或double类型输入的规则
        //                    if (input.ValueType == typeof(int) || input.ValueType == typeof(double))
        //                    {
        //                        if (!double.TryParse(input.Value.ToString(), out double dInputVal))
        //                            OnException($"分类器 模块中名称为 {arrRule[0]} 的输入数据转换成double失败！", new Exception($"分类器 模块运行失败!"));

        //                        //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                        if (arrRule[1].Equals(">") && dInputVal > dThreshold)
        //                        {
        //                            bAndResult[i] = true;
        //                            break;
        //                        }
        //                        //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                        else if (arrRule[1].Equals("<") && dInputVal < dThreshold)
        //                        {
        //                            bAndResult[i] = true;
        //                            break;
        //                        }
        //                        //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                        else if (arrRule[1].Equals(">=") && dInputVal >= dThreshold)
        //                        {
        //                            bAndResult[i] = true;
        //                            break;
        //                        }
        //                        //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                        else if (arrRule[1].Equals("<=") && dInputVal <= dThreshold)
        //                        {
        //                            bAndResult[i] = true;
        //                            break;
        //                        }
        //                        //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                        else if (arrRule[1].Equals("=") && dInputVal == dThreshold)
        //                        {
        //                            bAndResult[i] = true;
        //                            break;
        //                        }
        //                    }
        //                    //这里处理List<int>类型输入的规则，对于List类型的输入，需要判断里面每一个缺陷是否满足规则
        //                    else if (input.ValueType == typeof(List<int>))
        //                    {
        //                        List<int> lstInputVal = (List<int>)input.Value;
        //                        foreach (int inputVal in lstInputVal)
        //                        {
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            if (arrRule[1].Equals(">") && inputVal > dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            else if (arrRule[1].Equals("<") && inputVal < dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            else if (arrRule[1].Equals(">=") && inputVal >= dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            else if (arrRule[1].Equals("<=") && inputVal <= dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            else if (arrRule[1].Equals("=") && inputVal == dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    //这里处理List<double>类型输入的规则
        //                    else if (input.ValueType == typeof(List<double>))
        //                    {
        //                        List<double> lstInputVal = (List<double>)input.Value;
        //                        foreach (int inputVal in lstInputVal)
        //                        {
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            if (arrRule[1].Equals(">") && inputVal > dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            else if (arrRule[1].Equals("<") && inputVal < dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            else if (arrRule[1].Equals(">=") && inputVal >= dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            else if (arrRule[1].Equals("<=") && inputVal <= dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                            //在“或者”的情况下，只要其中一个规则满足条件，则代表满足缺陷的定义，退出循环即可
        //                            else if (arrRule[1].Equals("=") && inputVal == dThreshold)
        //                            {
        //                                bAndResult[i] = true;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            //在这里判断是否“并且”的所有情况都满足，如果都满足，则输出缺陷定义，否则，就定义为OK，这里如果只有一个元素，也可以用这个逻辑进行判定
        //            bool bIsNG = true;
        //            for (int i = 0; i < bAndResult.Length; i++)
        //            {
        //                if (!bAndResult[i])
        //                    bIsNG = false;
        //            }
        //            string strOutputResult = "OK";
        //            if (bIsNG)
        //                strOutputResult = strNGResult;
        //            if (Outputs.First(a => a.Name.Equals($"{strOutputName}.Rule")) != null)
        //                Outputs.First(a => a.Name.Equals($"{strOutputName}.Rule")).Value = strRule;
        //            if (Outputs.First(a => a.Name.Equals($"{strOutputName}.Result")) != null)
        //                Outputs.First(a => a.Name.Equals($"{strOutputName}.Result")).Value = strOutputResult;
        //        }
        //    }

        //    for (int i = 0; i < Inputs.Count; i++)
        //    {
        //        if (Inputs[i] == null || Inputs[i].Value == null)
        //            continue;

        //        if (Inputs[i].ValueType != typeof(int) && Inputs[i].ValueType != typeof(List<int>) &&
        //                        Inputs[i].ValueType == typeof(double) && Inputs[i].ValueType == typeof(List<double>))
        //            continue;


        //    }
        //}

        private void Process()
        {
            if (DicOutputRuleData == null)
                OnException($"分类器 模块没有定义输出规则！", new Exception($"分类器 模块运行失败!"));

            //每一行输出，就是一个输出的Output
            foreach ((string strOutputName, string strRule, string strNGResult) in DicOutputRuleData)
            {
                if (string.IsNullOrEmpty(strOutputName))
                    OnException($"分类器 模块中有的规则没有定义输出名称！", new Exception($"分类器 模块运行失败!"));

                if (string.IsNullOrEmpty(strRule) || string.IsNullOrEmpty(strNGResult))
                    OnException($"分类器 模块中名称为{strOutputName}的输出未定义的规则或判定结果！", new Exception($"分类器 模块运行失败!"));

                if (Outputs.First(a => a.Name.Equals($"{strOutputName}.Rule")) == null)
                    OnException($"分类器 模块中名称为{strOutputName}的输出未定义规则内容的输出参数！", new Exception($"分类器 模块运行失败!"));

                if (Outputs.First(a => a.Name.Equals($"{strOutputName}.Result")) == null)
                    OnException($"分类器 模块中名称为{strOutputName}的输出未定义判定结果的输出参数！", new Exception($"分类器 模块运行失败!"));

                if (strRule.Contains(ADVANCE_RULE_KEY_AND) && strRule.Contains(ADVANCE_RULE_KEY_OR))
                    OnException($"分类器 模块中名称为{strOutputName}的输出即包含{ADVANCE_RULE_KEY_AND}的情况，又包含{ADVANCE_RULE_KEY_OR}的情况，目前不支持这种逻辑的处理！", new Exception($"分类器 模块运行失败!"));

                string[] arrRuleSplit;
                List<bool>[] arrLstRuleResult;
                List<string> lstOutputRule = new List<string>();
                List<string> lstOutputResult = new List<string>();
                //如果是“并且”的情况，则当各个规则的条件都满足的情况下，才输出缺陷类型Result，只要任何一个不满足，就是OK的
                //单个规则的也可以用这个逻辑来判断
                if (strRule.Contains(ClassifierBL.ADVANCE_RULE_KEY_AND) || 
                    (!strRule.Contains(ClassifierBL.ADVANCE_RULE_KEY_AND) && !strRule.Contains(ClassifierBL.ADVANCE_RULE_KEY_OR)))
                {
                    arrRuleSplit = strRule.Split(new string[] { ClassifierBL.ADVANCE_RULE_KEY_AND }, StringSplitOptions.None);
                    arrLstRuleResult = new List<bool>[arrRuleSplit.Length];
                    JudgeGroup(strOutputName, arrRuleSplit, arrLstRuleResult, out int iInputCount);

                    //外层循环是遍历所有的输入，这里有可能会出现“并且”里面，有的规则只有一个输入，有的规则有多个输入的情况，
                    //如果是多个输入的，这个数量要与其它的多个输入的数量保持一致，这个在前面已经做了判断
                    //在这里判断的时候，如果是单个输入的话，就取List中的第一个元素的值就好了
                    for (int i = 0; i < iInputCount; i++)
                    {
                        bool bIsNG = true;
                        //在这里判断是否“并且”的所有情况都满足，如果都满足，则输出缺陷定义，否则，就定义为OK，这里如果只有一个元素，也可以用这个逻辑进行判定
                        foreach (List<bool> lstRuleResult in arrLstRuleResult)
                        {
                            if(lstRuleResult == null || lstRuleResult.Count < 1)
                                OnException($"分类器 模块中名称为{strOutputName}的输出判定失败！", new Exception($"分类器 模块运行失败!"));
                            else if(lstRuleResult.Count == 1)
                            {
                                if (!lstRuleResult[0])
                                {
                                    bIsNG = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (!lstRuleResult[i])
                                {
                                    bIsNG = false;
                                    break;
                                }
                            }
                        }

                        string strOutputResult = "OK";
                        if (bIsNG)
                            strOutputResult = strNGResult;
                        lstOutputRule.Add(strRule);
                        lstOutputResult.Add(strOutputResult);
                    }
                }
                //如果是“或者”的情况，则只要其中一个规则的条件满足，就更新当前规则的状态
                else if (strRule.Contains(ClassifierBL.ADVANCE_RULE_KEY_OR))
                {
                    arrRuleSplit = strRule.Split(new string[] { ClassifierBL.ADVANCE_RULE_KEY_OR }, StringSplitOptions.None);
                    arrLstRuleResult = new List<bool>[arrRuleSplit.Length];
                    JudgeGroup(strOutputName, arrRuleSplit, arrLstRuleResult, out int iInputCount);

                    //外层循环是遍历所有的输入，这里有可能会出现“或者”里面，有的规则只有一个输入，有的规则有多个输入的情况，
                    //如果是多个输入的，这个数量要与其它的多个输入的数量保持一致，这个在前面已经做了判断
                    //在这里判断的时候，如果是单个输入的话，就取List中的第一个元素的值就好了
                    for (int i = 0; i < iInputCount; i++)
                    {
                        bool bIsNG = false;
                        //在这里判断是否“或者”的是否有一个情况满足，有一个满足，则输出缺陷定义，否则，就定义为OK，这里如果只有一个元素，也可以用这个逻辑进行判定
                        foreach (List<bool> lstRuleResult in arrLstRuleResult)
                        {
                            if (lstRuleResult == null || lstRuleResult.Count < 1)
                                OnException($"分类器 模块中名称为{strOutputName}的输出判定失败！", new Exception($"分类器 模块运行失败!"));
                            else if (lstRuleResult.Count == 1)
                            {
                                if (lstRuleResult[0])
                                {
                                    bIsNG = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (lstRuleResult[i])
                                {
                                    bIsNG = true;
                                    break;
                                }
                            }
                        }

                        string strOutputResult = "OK";
                        if (bIsNG)
                            strOutputResult = strNGResult;
                        lstOutputRule.Add(strRule);
                        lstOutputResult.Add(strOutputResult);
                    }
                }
                
                if (Outputs.First(a => a.Name.Equals($"{strOutputName}.Rule")) != null)
                    Outputs.First(a => a.Name.Equals($"{strOutputName}.Rule")).Value = lstOutputRule;
                if (Outputs.First(a => a.Name.Equals($"{strOutputName}.Result")) != null)
                    Outputs.First(a => a.Name.Equals($"{strOutputName}.Result")).Value = lstOutputResult;
            }
        }

        private void DoJudge(string strRule, List<bool> lstRuleResult)
        {
            string[] arrRule = strRule.Split(new string[] { ClassifierBL.RULE_SUB_KEY }, StringSplitOptions.None);
            if (arrRule == null && arrRule.Length != 3)
                OnException($"分类器 模块中含有不合法的规则：{strRule}！", new Exception($"分类器 模块运行失败!"));

            if (!Inputs.Contains(arrRule[0]))
                OnException($"分类器 模块中的规则 {strRule} 中的缺陷的输入名称在输入列表中找不到！", new Exception($"分类器 模块运行失败!"));

            if (string.IsNullOrEmpty(arrRule[1]))
                OnException($"分类器 模块中的规则 {strRule} 没有设定算术操作符！", new Exception($"分类器 模块运行失败!"));

            double dThreshold = 0;
            if (string.IsNullOrEmpty(arrRule[2]) || !double.TryParse(arrRule[2], out dThreshold))
                OnException($"分类器 模块中的规则 {strRule} 设定的阈值不合法！", new Exception($"分类器 模块运行失败!"));

            HyTerminal input = Inputs.First(a => a.Name.Equals(arrRule[0]));
            if (input == null || input.Value == null || (input.ValueType != typeof(int) && input.ValueType != typeof(List<int>) &&
                input.ValueType != typeof(double) && input.ValueType != typeof(List<double>)))
                OnException($"分类器 模块中名称为 {arrRule[0]} 的输入没有数据或是数据类型不合法！", new Exception($"分类器 模块运行失败!"));

            //这里处理int或double类型输入的规则
            if (input.ValueType == typeof(int) || input.ValueType == typeof(double))
            {
                if (!double.TryParse(input.Value.ToString(), out double dInputVal))
                    OnException($"分类器 模块中名称为 {arrRule[0]} 的输入数据转换成double失败！", new Exception($"分类器 模块运行失败!"));

                //true代表满足缺陷规则定义，是个缺陷
                if (arrRule[1].Equals(">") && dInputVal > dThreshold)
                    lstRuleResult.Add(true);
                //true代表满足缺陷规则定义，是个缺陷
                else if (arrRule[1].Equals("<") && dInputVal < dThreshold)
                    lstRuleResult.Add(true);
                //true代表满足缺陷规则定义，是个缺陷
                else if (arrRule[1].Equals(">=") && dInputVal >= dThreshold)
                    lstRuleResult.Add(true);
                //true代表满足缺陷规则定义，是个缺陷
                else if (arrRule[1].Equals("<=") && dInputVal <= dThreshold)
                    lstRuleResult.Add(true);
                //true代表满足缺陷规则定义，是个缺陷
                else if (arrRule[1].Equals("=") && dInputVal == dThreshold)
                    lstRuleResult.Add(true);
                //false代表不满足缺陷规则定义，不是个缺陷
                else
                    lstRuleResult.Add(false);
            }
            //这里处理List<int>类型输入的规则，对于List类型的输入，需要判断里面每一个缺陷是否满足规则
            else if (input.ValueType == typeof(List<int>))
            {
                List<int> lstInputVal = (List<int>)input.Value;
                foreach (int dInputVal in lstInputVal)
                {
                    //true代表满足缺陷规则定义，是个缺陷
                    if (arrRule[1].Equals(">") && dInputVal > dThreshold)
                        lstRuleResult.Add(true);
                    //true代表满足缺陷规则定义，是个缺陷
                    else if (arrRule[1].Equals("<") && dInputVal < dThreshold)
                        lstRuleResult.Add(true);
                    //true代表满足缺陷规则定义，是个缺陷
                    else if (arrRule[1].Equals(">=") && dInputVal >= dThreshold)
                        lstRuleResult.Add(true);
                    //true代表满足缺陷规则定义，是个缺陷
                    else if (arrRule[1].Equals("<=") && dInputVal <= dThreshold)
                        lstRuleResult.Add(true);
                    //true代表满足缺陷规则定义，是个缺陷
                    else if (arrRule[1].Equals("=") && dInputVal == dThreshold)
                        lstRuleResult.Add(true);
                    //false代表不满足缺陷规则定义，不是个缺陷
                    else
                        lstRuleResult.Add(false);
                }
            }
            //这里处理List<double>类型输入的规则
            else if (input.ValueType == typeof(List<double>))
            {
                List<double> lstInputVal = (List<double>)input.Value;
                foreach (int dInputVal in lstInputVal)
                {
                    //true代表满足缺陷规则定义，是个缺陷
                    if (arrRule[1].Equals(">") && dInputVal > dThreshold)
                        lstRuleResult.Add(true);
                    //true代表满足缺陷规则定义，是个缺陷
                    else if (arrRule[1].Equals("<") && dInputVal < dThreshold)
                        lstRuleResult.Add(true);
                    //true代表满足缺陷规则定义，是个缺陷
                    else if (arrRule[1].Equals(">=") && dInputVal >= dThreshold)
                        lstRuleResult.Add(true);
                    //true代表满足缺陷规则定义，是个缺陷
                    else if (arrRule[1].Equals("<=") && dInputVal <= dThreshold)
                        lstRuleResult.Add(true);
                    //true代表满足缺陷规则定义，是个缺陷
                    else if (arrRule[1].Equals("=") && dInputVal == dThreshold)
                        lstRuleResult.Add(true);
                    //false代表不满足缺陷规则定义，不是个缺陷
                    else
                        lstRuleResult.Add(false);
                }
            }
        }

        /// <summary>
        /// 处理“并且”情况的判定
        /// </summary>
        /// <param name="strOutputName"></param>
        /// <param name="strRule"></param>
        /// <param name="strNGResult"></param>
        private void JudgeGroup(string strOutputName, string[] arrRuleSplitByAnd, List<bool>[] arrLstRuleResult, out int iInputCount)
        {
            for(int i = 0; i < arrLstRuleResult.Length; i++)
            {
                arrLstRuleResult[i] = new List<bool>();

                if (string.IsNullOrEmpty(arrRuleSplitByAnd[i]))
                    continue;

                //在单一的规则内，根据分割符分割出逻辑判断语句进行判断
                DoJudge(arrRuleSplitByAnd[i], arrLstRuleResult[i]);
            }

            //判断一下各个规则中，存在多个输入的时候，是否有数量不一致的情况，如果有，是没办法找到对应关系的，就要报错
            iInputCount = 0;
            foreach (List<bool> lstRuleResult in arrLstRuleResult)
            {
                if (lstRuleResult == null || lstRuleResult.Count < 1)
                    OnException($"分类器 模块中名称为{strOutputName}的输出判定失败！", new Exception($"分类器 模块运行失败!"));
                else if (iInputCount < lstRuleResult.Count)
                    iInputCount = lstRuleResult.Count;

                if (lstRuleResult.Count != 1 && iInputCount != 1 && lstRuleResult.Count != iInputCount)
                    OnException($"分类器 模块中名称为{strOutputName}的输出判定规则中，存在2个规则中都含有多个输入，但是输入的数量不一致的情况！", new Exception($"分类器 模块运行失败!"));
            }
        }
    }
}
