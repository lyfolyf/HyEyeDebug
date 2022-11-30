using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyEye.Test
{
    public delegate void SetPBDelegate();
    public delegate void ShowMSGDelegate(string fn);
    public delegate void SetBtnDelegate();
    public delegate void ShowCheckDelegate(string ck);
    public partial class ucProcessLog : UserControl
    {
        ShowMSGDelegate showHandle;
        SetBtnDelegate setbtnHandle;
        SetPBDelegate setpbHandle;
        ShowCheckDelegate showCheckHandle;

        public ucProcessLog()
        {
            showHandle += ShowMsg;
            setbtnHandle += SetBtn;
            setpbHandle += SetPB;
            showCheckHandle += ShowCheck;
            InitializeComponent();
            pb1.Visible = false;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            if (od.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = od.FileName;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            txtShow.Text = "";

            if (string.IsNullOrEmpty(txtFileName.Text))
                return;

            btnSelect.Enabled = false;

            StartProgress();

            FileInfo fl = new FileInfo(txtFileName.Text);
            string saveFile = fl.DirectoryName + "\\" + fl.Name.Split('.')[0] + "_已筛选_" + DateTime.Now.ToString("HHmmss") + fl.Extension;

            List<string> paras = new List<string>();
            foreach (Control item in this.Controls)
            {
                if (item is CheckBox)
                {
                    CheckBox cb = (CheckBox)item;
                    if (cb.Checked)
                    {
                        switch (cb.Text)
                        {
                            case "A":
                                paras.Add(",A");
                                break;
                            case "R":
                                paras.Add(",R");
                                break;
                            case "AR":
                                paras.Add(",AR");
                                break;
                            default:
                                paras.Add(cb.Text);
                                break;
                        }
                    }
                }
            }

            Thread thd = new Thread(() => SelectFile(txtFileName.Text, saveFile, paras));
            thd.IsBackground = true;
            thd.Start();
        }

        private void SelectFile(string oldfile, string newfile, List<string> paras)
        {
            try
            {
                using (StreamReader sr = new StreamReader(oldfile, Encoding.Default))
                {
                    string info;
                    uint count = 0;
                    while ((info = sr.ReadLine()) != null)
                    {
                        foreach (string temp in paras)
                        {
                            if (info.Contains(temp))
                            {
                                count++;
                                SaveFileAndSetPB(newfile, info);
                            }

                        }
                    }
                    if (count > 0)
                        txtShow.Invoke(showHandle, newfile);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnSelect.Invoke(setbtnHandle);
            }
        }

        private void SaveFileAndSetPB(string filepath, string info)
        {
            FileStream fs = !File.Exists(filepath) ?
              new FileStream(filepath, FileMode.Create, FileAccess.Write)
            : new FileStream(filepath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(info);
            sw.Flush();
            sw.Close();
            fs.Close();
            pb1.Invoke(setpbHandle);
        }

        private void SaveFile(string filepath, string info)
        {
            FileStream fs = !File.Exists(filepath) ?
              new FileStream(filepath, FileMode.Create, FileAccess.Write)
            : new FileStream(filepath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(info);
            sw.Flush();
            sw.Close();
            fs.Close();
        }




        private void btnReceive_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName.Text))
                return;
            if (string.IsNullOrEmpty(textBox1.Text))
                return;

            ERcount = 0;
            DicQ = new Dictionary<string, Queue<string>>();
            txtShow.Text = string.Empty;

            string[] paramTask = textBox1.Text.Split(',');
            foreach (var item in paramTask)
            {
                DicQ.Add(item, new Queue<string>());
            }

            Thread thd = new Thread(() => CheckRec(txtFileName.Text, paramTask));
            thd.IsBackground = true;
            thd.Start();
        }

        bool checkStop;
        int ERcount = 0;

        private void CheckRec(string srcFile, string[] paramTask)
        {
            FileInfo fl = new FileInfo(srcFile);
            string saveFile = fl.DirectoryName + "\\" + fl.Name.Split('.')[0] + "_校验接收指令记录_" + DateTime.Now.ToString("HHmmss") + fl.Extension;

            try
            {
                using (StreamReader sr = new StreamReader(srcFile, Encoding.Default))
                {
                    string info;
                    while ((info = sr.ReadLine()) != null)
                    {
                        if (!info.Contains("接收指令") || info.Contains(",R;"))
                            continue;

                        if (info.Contains("P,RE"))
                        {
                            ClearData(paramTask);
                            continue;
                        }

                        if (info.Contains("P,S"))
                        {
                            checkStop = true;
                            continue;
                        }
                        if (info.Contains("P,R"))
                        {
                            checkStop = false;
                            continue;
                        }
                        if (checkStop && !info.Contains("RS,"))
                        {
                            ERcount++;
                            txtShow.Invoke(showCheckHandle, $"{ERcount}==》暂停中接收其他指令：{info}");
                            continue;
                        }

                        CheckTask(saveFile, info, paramTask);
                    }
                    //循环结束，检查是否有剩余数据
                    CheckLeft(paramTask);
                    MessageBox.Show("OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnSelect.Invoke(setbtnHandle);
            }
        }

        Dictionary<string, Queue<string>> DicQ;

        private void CheckTask(string file, string info, string[] paramTask)
        {
            try
            {
                //第一个直接进入
                if (info.Contains(paramTask[0] + ",1,"))
                {
                    DicQ[paramTask[0]].Enqueue(info);
                    return;
                }

                //第二到倒数第二个，相同处理
                for (int i = 1; i < paramTask.Length - 1; i++)
                {
                    if (info.Contains(paramTask[i] + ",1,"))
                    {
                        //先检查上一个有无数据
                        if (DicQ[paramTask[i - 1]].Count == 0)
                        {
                            //上一个无数据，该指令多出
                            ERcount++;
                            txtShow.Invoke(showCheckHandle, $"{ERcount}==》{paramTask[i]}多出：{info}");
                            return;
                        }
                        else
                        {
                            //上一个有数据，该指令进队列
                            DicQ[paramTask[i]].Enqueue(info);
                            return;
                        }
                    }
                }

                //最后一个
                if (info.Contains(paramTask[paramTask.Length - 1] + ",1,"))
                {
                    //先检查上一个有无数据
                    if (DicQ[paramTask[paramTask.Length - 2]].Count == 0)
                    {
                        //上一个无数据，最后一个为多出指令
                        ERcount++;
                        txtShow.Invoke(showCheckHandle, $"{ERcount}==》{paramTask[paramTask.Length - 1]}多出：{info}");
                        return;
                    }
                    else
                    {
                        //上一个有数据，该指令进队列
                        DicQ[paramTask[paramTask.Length - 1]].Enqueue(info);
                        //之前都有数据，最后一个与之前几个能组成同一组数据,并将数据取出
                        foreach (string key in paramTask)
                        {
                            if (DicQ[key].Count == 0)
                                continue;
                            SaveFile(file, DicQ[key].Dequeue());
                        }
                        SaveFile(file, "");
                        return;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CheckLeft(string[] paramTask)
        {
            try
            {
                foreach (string key in paramTask)
                {
                    if (DicQ[key].Count > 0)
                    {
                        foreach (var item in DicQ[key])
                        {
                            ERcount++;
                            txtShow.Invoke(showCheckHandle, $"{ERcount}==》{key}指令剩余：{item}");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ClearData(string[] paramTask)
        {
            checkStop = false;
            foreach (string key in paramTask)
            {
                DicQ[key].Clear();
            }
        }

        private void ShowCheck(string ck)
        {
            txtShow.Text += "\r\n" + ck + "\r\n";
        }









        Dictionary<string, List<(string, bool)>> DicTB;

        private void btnTB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName.Text))
                return;
            if (string.IsNullOrEmpty(txtTB.Text))
                return;

            DicTB = new Dictionary<string, List<(string, bool)>>();
            txtShow.Text = string.Empty;

            string[] paramTask = txtTB.Text.Split(',');
            int lvl = 1;
            foreach (var item in paramTask)
            {
                var temp = item.Split('#');
                List<(string, bool)> templist = new List<(string, bool)>();
                for (int i = 0; i < Convert.ToInt32(temp[1]); i++)
                {
                    templist.Add(($"拍照{lvl++}", false));
                }
                DicTB.Add(temp[0], templist);
            }

            txtShow.Text = string.Empty;

            //Thread thd = new Thread(() => CheckTB(txtFileName.Text));
            //thd.IsBackground = true;
            //thd.Start();
        }


        private void CheckTB(string srcFile)
        {

            FileInfo fl = new FileInfo(srcFile);
            string saveFile = fl.DirectoryName + "\\" + fl.Name.Split('.')[0] + "_校验ToolBlock_" + DateTime.Now.ToString("HHmmss") + fl.Extension;

            try
            {
                using (StreamReader sr = new StreamReader(srcFile, Encoding.Default))
                {
                    string info;
                    while ((info = sr.ReadLine()) != null)
                    {
                        if (!info.Contains("发送指令") || !info.Contains("运行") || info.Contains("运行指令") || info.Contains("A,"))
                            continue;

                        //  CheckTask(saveFile, info, paramTask);
                    }
                    //循环结束，检查是否有剩余数据
                    //  CheckLeft(paramTask);
                    MessageBox.Show("OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnSelect.Invoke(setbtnHandle);
            }
        }








        //         if (info.Contains("T7"))
        //                        {
        //                            var tempT7 = info.Split(',');
        //        //先判断T7和之前T6是不是一组数据
        //        bool hasT7Key = false;
        //                            foreach (var t6 in T6)
        //                            {
        //                                hasT7Key = t6.Contains(tempT7[tempT7.Length - 3].Split(']')[0].TrimStart('['));
        //                            }
        //                            if (hasT7Key)
        //                            {
        //                                //T6、T7是一组就把T6先提出来
        //                                var temp6 = T6.Dequeue();

        //                                if (T1.Count == 0)//再对T1进行判断
        //                                {
        //                                    //T1是0，说明T6、T7是多出来的
        //                                    count++;
        //                                    txtShow.Invoke(showCheckHandle, $"{count}》》T6指令多出：{temp6}");
        //                                    txtShow.Invoke(showCheckHandle, $"{count}》》T7指令多出：{info}");
        //                                }
        //                                else
        //{
        //    //T1有，则该T1和T6、T7是一组正确日志，T1也取出来
        //    T1.Dequeue();
        //}
        //                            }
        //                            else
        //{
        //    //T6、T7不是一组就提示
        //    count++;
        //    txtShow.Invoke(showCheckHandle, $"{count}》》T7指令多出：{info}");
        //}
        //                        }





        private void StartProgress()
        {
            pb1.Visible = true;
            pb1.Minimum = 0;
            pb1.Maximum = 10000;
            pb1.Value = 0;
            pb1.Step = 1;
        }

        private void SetPB()
        {
            if (pb1.Value < 9999)
                pb1.PerformStep();
        }

        private void ShowMsg(string fn)
        {
            int count = pb1.Maximum - pb1.Value;
            if (count > 0)
                pb1.Increment(count);

            txtShow.Text = "处理完成:" + fn;
        }

        private void SetBtn()
        {
            btnSelect.Enabled = true;
            pb1.Visible = false;
        }





    }
}
