using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using PlcSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using static HyEye.WForm.Settings.PLCRegSetting.PLCRegView;

namespace HyEye.WForm.Settings.PLCRegSetting
{
    public delegate void delegateSave();
    public delegate void delegateCancel();
    public delegate void delegateSetCount();
    public delegate void delegateSetHead(string head, string rId, int modeIndex);

    public partial class FrmPLCRegSetPage : Form
    {
        public delegateSave saveHandle;
        public delegateSave cancelHandle;
        public delegateSetCount setCountHandle;
        public delegateSetHead setHeadHandle;
        readonly IPlcRepository plcRepo;
        readonly ITaskRepository taskRepo;
        readonly IGLog log;

        public int index { get; set; }

        public CommunicationInfoVO CommunicationInfo { get; set; }

        public FrmPLCRegSetPage(
            int index,
            CommunicationInfoVO communicationInfo,
            ITaskRepository taskRepo,
            IPlcRepository plcRepo,
            IGLog log)
        {
            InitializeComponent();

            this.index = index;
            this.CommunicationInfo = communicationInfo;
            this.taskRepo = taskRepo;
            this.plcRepo = plcRepo;
            this.log = log;

            loadData();
        }

        private void FrmPLCRegSetPage_Load(object sender, EventArgs e)
        {

        }

        public ExpandableDataGridView masterDetail;

        public void loadData()
        {
            clearFields();
            createMasterDetailView();
        }

        public void clearFields()
        {
            panel1.Controls.Clear();
            masterDetail = null;
            Refresh();
        }

        public void createMasterDetailView()
        {
            try
            {
                #region 使用方法一
                var oDataSet = GetDataSet();

                masterDetail = new ExpandableDataGridView(oDataSet, controlType.middle, index);
                masterDetail.childView.CommunicationInfo = CommunicationInfo;
                masterDetail.SetAllHandle += SetAllForAgg;
                masterDetail.SetHead += SetHeadForAgg;
                #endregion

                #region 使用方法二
                //var dicRelateData1 = new Dictionary<string, string>();
                //var dicRelateData2 = new Dictionary<string, string>();
                //dicRelateData1.Add("Menu_ID", "Menu_ID");
                //dicRelateData2.Add("Menu_Name2", "Menu_Name2");
                //masterDetail = new MasterControl(GetDataSource(), GetDataSource2(), GetDataSource3(),
                //                               dicRelateData1, dicRelateData2, controlType.outside);
                #endregion
                panel1.Controls.Add(masterDetail);

                //设置单击下拉模式(否则需要点两下才能显示下拉框)
                masterDetail.EditMode = DataGridViewEditMode.EditOnEnter;

                cModule.modeindex = index;

                cModule.setParentGrid(masterDetail);
                if (masterDetail.childView.childGrid.Count > 0)
                    cModule.setSonGrid(masterDetail.childView.childGrid[0]);
            }
            catch (Exception ex)
            {

            }
        }

        private void SetHeadForAgg(string head, string rId, int modeIndex)
        {
            setHeadHandle?.Invoke(head, rId, modeIndex);
        }

        private void SetAllForAgg()
        {
            setCountHandle?.Invoke();
        }

        private (List<PLCTask> lp, List<PLCTaskDetail> ls) GetData()
        {
            List<PlcCommandInfoVO> plist = new List<PlcCommandInfoVO>();

            switch (index)
            {
                case 0:
                    plist = plcRepo.GetTaskRecvCommandInfos();
                    break;
                case 1:
                    plist = plcRepo.GetTaskSendCommandInfos();
                    break;
                case 2:
                    plist = plcRepo.GetCalibRecvCommandInfos();
                    break;
                case 3:
                    plist = plcRepo.GetCalibSendCommandInfos();
                    break;
            }

            List<PLCTask> lp = new List<PLCTask>();
            List<PLCTaskDetail> ls = new List<PLCTaskDetail>();

            for (int i = 0; i < plist.Count; i++)
            {
                int id = i + 1;
                lp.Add(new PLCTask()
                {
                    ID = id,
                    Task_Name = plist[i].TaskName,
                    Image_Name = plist[i].AcqName,
                    Command_Header = plist[i].CommandHeader,
                    Image_Index = plist[i].AcqIndex,
                    Reg_StartPath = plist[i].StartDeviceName,
                    Order_Count = plist[i].DeviceLength
                });
                PlcDeviceName tempPath = plist[i].StartDeviceName;

                string headString = plist[i].CommandHeader.Index.ToString();
                //ToString().Replace("T", "").Replace("CB", "").Replace("HE", "");
                switch (index)
                {
                    case 0:
                        {
                            foreach (var item in DefaultFieldInfo)
                            {
                                string tempvalue = item.Value;
                                string detail = item.Description;
                                if (item.Name == "指令头")
                                    tempvalue = headString;
                                if (item.Name == "拍照索引")
                                    tempvalue = plist[i].AcqIndex.ToString();
                                if (item.Name == "指令类型")
                                    detail = "A:1; R:2; AR:3";
                                ls.Add(new PLCTaskDetail()
                                {
                                    ID = id,
                                    Reg_Path = tempPath,
                                    Value_Type = res(item.ValueType),
                                    Reg_Len = item.DeviceLength,
                                    Reg_Value = tempvalue,
                                    Reg_rat = item.ValueRatio.ToString(),
                                    Sub_Name = item.Name,
                                    Sub_Use = resUse(item.Use),
                                    Detail = detail,
                                });
                                tempPath = tempPath + 1;
                            }
                        }
                        break;
                    case 1:
                        {
                            foreach (var item in DefaultSendFieldInfo)
                            {
                                string tempvalue = item.Value;
                                if (item.Name == "指令头")
                                    tempvalue = headString;
                                if (item.Name == "拍照索引")
                                    tempvalue = plist[i].AcqIndex.ToString();
                                ls.Add(new PLCTaskDetail()
                                {
                                    ID = id,
                                    Reg_Path = tempPath,
                                    Value_Type = res(item.ValueType),
                                    Reg_Len = item.DeviceLength,
                                    Reg_Value = tempvalue,
                                    Reg_rat = item.ValueRatio.ToString(),
                                    Sub_Name = item.Name,
                                    Sub_Use = resUse(item.Use),
                                    Detail = item.Description,
                                });
                                tempPath += 1;
                            }
                        }
                        break;
                    case 2:
                        {
                            foreach (var item in DefaultFieldInfo)
                            {
                                string tempvalue = item.Value;
                                string detail = item.Description;
                                if (item.Name == "指令头")
                                    tempvalue = headString;
                                if (item.Name == "拍照索引")
                                    tempvalue = plist[i].AcqIndex.ToString();
                                if (item.Name == "指令类型")
                                    detail = "S:4; C:8";
                                ls.Add(new PLCTaskDetail()
                                {
                                    ID = id,
                                    Reg_Path = tempPath,
                                    Value_Type = res(item.ValueType),
                                    Reg_Len = item.DeviceLength,
                                    Reg_Value = tempvalue,
                                    Reg_rat = item.ValueRatio.ToString(),
                                    Sub_Name = item.Name,
                                    Sub_Use = resUse(item.Use),
                                    Detail = detail,
                                });
                                tempPath += 1;
                            }
                        }
                        break;
                    case 3:
                        {
                            foreach (var item in DefaultSendFieldInfo)
                            {
                                string tempvalue = item.Value;
                                if (item.Name == "指令头")
                                    tempvalue = headString;
                                if (item.Name == "拍照索引")
                                    tempvalue = plist[i].AcqIndex.ToString();
                                ls.Add(new PLCTaskDetail()
                                {
                                    ID = id,
                                    Reg_Path = tempPath,
                                    Value_Type = res(item.ValueType),
                                    Reg_Len = item.DeviceLength,
                                    Reg_Value = tempvalue,
                                    Reg_rat = item.ValueRatio.ToString(),
                                    Sub_Name = item.Name,
                                    Sub_Use = resUse(item.Use),
                                    Detail = item.Description,
                                });
                                tempPath += 1;
                            }
                        }
                        break;
                }
                foreach (var item in plist[i].Fields)
                {
                    ls.Add(new PLCTaskDetail()
                    {
                        ID = id,
                        Reg_Path = item.DeviceName,
                        Value_Type = res(item.ValueType),
                        Reg_Len = item.DeviceLength,
                        Reg_Value = item.Value,
                        Reg_rat = item.ValueRatio.ToString(),
                        Sub_Name = item.Name,
                        Sub_Use = resUse(item.Use),
                        Detail = item.Description,
                    });
                }
            }
            return (lp, ls);
        }

        public DataSet GetDataSet()
        {
            var oDataSet = new DataSet();

            (List<PLCTask> lp, List<PLCTaskDetail> ls) = GetData();

            var oTable1 = Fill<PLCTask>(lp);
            //var oTable1 = Fill<PLCTask>(GetDataSource());
            oTable1.TableName = "PLCTask";

            oTable1.Columns["Task_Name"].ColumnName = "任务名称";
            if (index < 2)
                oTable1.Columns["Image_Name"].ColumnName = "拍照名称";
            else
                oTable1.Columns["Image_Name"].ColumnName = "标定名称";
            oTable1.Columns["Command_Header"].ColumnName = "指令头";
            oTable1.Columns["Image_Index"].ColumnName = "拍照索引";
            oTable1.Columns["Reg_StartPath"].ColumnName = "起始地址";
            oTable1.Columns["Order_Count"].ColumnName = "总指令长度";

            var oTable2 = Fill<PLCTaskDetail>(ls);
            //var oTable2 = Fill<PLCTaskDetail>(GetDataSource2());
            oTable2.TableName = "PLCTaskDetail";

            oTable2.Columns["Reg_Path"].ColumnName = "寄存器地址";
            oTable2.Columns["Value_Type"].ColumnName = "值类型";
            oTable2.Columns["Reg_Len"].ColumnName = "寄存器长度";
            oTable2.Columns["Reg_Value"].ColumnName = "值";
            oTable2.Columns["Reg_rat"].ColumnName = "系数";
            oTable2.Columns["Sub_Name"].ColumnName = "字段名称";
            oTable2.Columns["Sub_Use"].ColumnName = "字段用途";
            oTable2.Columns["Detail"].ColumnName = "说明";

            oDataSet.Tables.AddRange(new DataTable[] { oTable1, oTable2 });
            //这是对应关系的时候主键必须唯一
            oDataSet.Relations.Add("1", oDataSet.Tables["PLCTask"].Columns["ID"], oDataSet.Tables["PLCTaskDetail"].Columns["ID"]);

            return oDataSet;
        }

        public DataTable Fill<T>(List<T> objlist)
        {
            if (objlist == null || objlist.Count <= 0)
            {
                return null;
            }
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;
            PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (T t in objlist)
            {
                if (t == null)
                {
                    continue;
                }
                row = dt.NewRow();
                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    PropertyInfo pi = myPropertyInfo[i];
                    string name = pi.Name;
                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }
                    row[name] = pi.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public bool SaveCheckPath(PlcDeviceName rd, PlcDeviceName wd)
        {
            bool res = false;

            DataRow[] rddr = masterDetail._cDataset.Tables["PLCTaskDetail"].Select($"寄存器地址='{rd}'");
            if (rddr.Length > 0)
                res = true;

            DataRow[] wddr = masterDetail._cDataset.Tables["PLCTaskDetail"].Select($"寄存器地址='{wd}'");
            if (wddr.Length > 0)
                res = true;

            return res;
        }

        public int GetCurrentCount()
        {
            int count = 0;
            DataTable dtParent = masterDetail._cDataset.Tables["PLCTask"];
            foreach (DataRow item in dtParent.Rows)
            {
                count += Convert.ToInt32(item["总指令长度"]);
            }
            return count;
        }

        public void ChangeHead(string head, string rId)
        {
            masterDetail?.ChangeHeadMaster(head, rId);
        }

        public void SetSave()
        {
            List<PlcCommandInfoVO> listP = new List<PlcCommandInfoVO>();
            DataTable dtParent = masterDetail._cDataset.Tables["PLCTask"];
            DataTable dtSon = masterDetail._cDataset.Tables["PLCTaskDetail"];

            string[] conString = { "指令头", "拍照索引", "指令类型", "执行结果" };

            for (int i = 0; i < dtParent.Rows.Count; i++)
            {
                int id = (int)dtParent.Rows[i]["ID"];
                string acqname;
                if (index < 2)
                    acqname = dtParent.Rows[i]["拍照名称"].ToString();
                else
                    acqname = dtParent.Rows[i]["标定名称"].ToString();
                PlcCommandInfoVO temp = new PlcCommandInfoVO()
                {
                    TaskName = dtParent.Rows[i]["任务名称"].ToString(),
                    CommandHeader = dtParent.Rows[i]["指令头"].ToString(),
                    AcqName = acqname,
                    AcqIndex = Convert.ToInt32(dtParent.Rows[i]["拍照索引"]),
                    StartDeviceName = dtParent.Rows[i]["起始地址"].ToString(),
                    DeviceLength = Convert.ToInt32(dtParent.Rows[i]["总指令长度"]),
                    Fields = new List<PlcCommandFieldInfoVO>()
                };
                DataRow[] drarr = dtSon.Select($"ID={id}");
                foreach (DataRow item in drarr)
                {
                    if (((IList)conString).Contains(item["字段名称"]))
                    {
                        continue;
                    }
                    temp.Fields.Add(new PlcCommandFieldInfoVO()
                    {
                        DeviceName = item["寄存器地址"].ToString(),
                        ValueType = gettype(item["值类型"].ToString()),
                        DeviceLength = Convert.ToInt32(item["寄存器长度"].ToString()),
                        Value = item["值"].ToString(),
                        ValueRatio = Convert.ToInt32(item["系数"].ToString()),
                        Description = item["说明"].ToString(),
                        Name = item["字段名称"].ToString(),
                        Use = getUse(item["字段用途"].ToString())
                    });
                }
                listP.Add(temp);
            }

            switch (index)
            {
                case 0:
                    plcRepo.ResetTaskRecvCommands(listP);
                    break;
                case 1:
                    plcRepo.ResetTaskSendCommands(listP);
                    break;
                case 2:
                    plcRepo.ResetCalibRecvCommands(listP);
                    break;
                case 3:
                    plcRepo.ResetCalibSendCommands(listP);
                    break;
            }
            //plcRepo.Save();
        }

        public string res(Type type)
        {
            switch (type.ToString())
            {
                case "System.Int16":
                    return "Short";
                case "System.Int32":
                    return "Int";
                case "System.String":
                    return "String";
                default:
                    return "Short";
            }
        }

        public Type gettype(string type)
        {
            switch (type.ToString())
            {
                case "Short":
                    return typeof(short);
                case "Int":
                    return typeof(int);
                case "String":
                    return typeof(string);
                default:
                    return typeof(short);
            }
        }

        public string resUse(CommandFieldUse use)
        {
            switch (use)
            {
                case CommandFieldUse.None:
                    return "None";
                case CommandFieldUse.ToolBlock:
                    return "ToolBlock";
                case CommandFieldUse.Script:
                    return "Script";
                case CommandFieldUse.SaveImage:
                    return "SaveImage";
                default:
                    return "None";
            }
        }

        public CommandFieldUse getUse(string use)
        {
            switch (use)
            {
                case "None":
                    return CommandFieldUse.None;
                case "ToolBlock":
                    return CommandFieldUse.ToolBlock;
                case "Script":
                    return CommandFieldUse.Script;
                case "SaveImage":
                    return CommandFieldUse.SaveImage;
                default:
                    return CommandFieldUse.None;
            }
        }





        public List<PlcCommandFieldInfo> DefaultFieldInfo
        {
            get
            {
                return new List<PlcCommandFieldInfo>()
                {
                    new PlcCommandFieldInfo (){
                        Name = "指令头",
                        DeviceName = new PlcDeviceName {Name = "D",Address = 0 },
                        ValueType = typeof(short),
                        DeviceLength = 1,
                        Value = "",
                        ValueRatio = 1,
                        Description = "",
                    },
                     new PlcCommandFieldInfo (){
                        Name = "拍照索引",
                        DeviceName = new PlcDeviceName {Name = "D",Address = 1 },
                        ValueType = typeof(short),
                        DeviceLength = 1,
                        Value = "",
                        ValueRatio = 1,
                        Description = "",
                    },
                      new PlcCommandFieldInfo (){
                        Name = "指令类型",
                        DeviceName = new PlcDeviceName {Name = "D",Address = 2 },
                        ValueType = typeof(short),
                        DeviceLength = 1,
                        Value = "",
                        ValueRatio = 1,
                        Description = "",
                    },
                };
            }
        }


        public List<PlcCommandFieldInfo> DefaultSendFieldInfo
        {
            get
            {
                return new List<PlcCommandFieldInfo>()
                {
                    new PlcCommandFieldInfo (){
                        Name = "指令头",
                        DeviceName = new PlcDeviceName {Name = "D",Address = 0 },
                        ValueType = typeof(short),
                        DeviceLength = 1,
                        Value = "",
                        ValueRatio = 1,
                        Description = "",
                    },
                     new PlcCommandFieldInfo (){
                        Name = "拍照索引",
                        DeviceName = new PlcDeviceName {Name = "D",Address = 1 },
                        ValueType = typeof(short),
                        DeviceLength = 1,
                        Value = "",
                        ValueRatio = 1,
                        Description = "",
                    },
                      new PlcCommandFieldInfo (){
                        Name = "指令类型",
                        DeviceName = new PlcDeviceName {Name = "D",Address = 2 },
                        ValueType = typeof(short),
                        DeviceLength = 1,
                        Value = "",
                        ValueRatio = 1,
                        Description = "",
                    },
                      new PlcCommandFieldInfo (){
                        Name = "执行结果",
                        DeviceName = new PlcDeviceName {Name = "D",Address = 3 },
                        ValueType = typeof(short),
                        DeviceLength = 1,
                        Value = "",
                        ValueRatio = 1,
                        Description = "0/非0",
                    }
                };
            }
        }







    }
}
