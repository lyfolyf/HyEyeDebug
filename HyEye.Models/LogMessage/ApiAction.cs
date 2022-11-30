namespace HyEye.Models
{
    public static class ApiAction
    {
        public const string A_Check          = "校验";

        public const string A_Add            = "新增";

        public const string A_Delete         = "删除";

        public const string A_Update         = "修改";

        public const string A_Rename         = "重命名";

        public const string A_Enable         = "启用";

        public const string A_Disable        = "禁用";

        public const string A_MoveUp         = "上移";

        public const string A_MoveDown       = "下移";

        public const string A_SetCamera      = "设置相机";

        public const string A_GetCamera      = "获取相机";

        public const string A_SetParams      = "设置参数";

        public const string A_Get            = "获取";

        public const string A_Save           = "保存";

        public const string A_SaveImage      = "保存图像";

        public const string A_Load           = "加载";

        public const string A_Reset          = "重置";

        public const string A_Run            = "运行";

        public const string A_RunToolBlock   = "运行 ToolBlock";

        public const string A_GetRecord      = "获取Record";

        public const string A_RunScript      = "执行脚本";

        public const string A_DiscardOutput  = "丢弃数据";

        public const string A_Calibration    = "标定";

        public const string A_AutoCalib      = "自动标定";

        public const string A_GetCircle      = "计算旋转中心";

        public const string A_Start          = "启动";

        public const string A_Stop           = "停止";

        public const string A_ShowImage      = "显示图像";

        public const string A_AcqImage       = "取像";

        public const string A_DiscardImage   = "丢弃图像";

        public const string A_Begin          = "开始";

        public const string A_GetPose        = "获取坐标";

        public const string A_RunCmd         = "运行指令";

        public const string A_Distort        = "畸变矫正";

        public const string A_GetConfig      = "获取配置";

        public const string A_AddTool        = "添加工具";

        public const string A_DelTool        = "删除工具";

        public const string A_RenameTool     = "重命名工具";

        public const string A_LoadVpp        = "加载VPP";

        public const string A_Wait           = "等待";

        public const string A_Backup         = "备份";

        public const string A_Change         = "切换";

        public const string A_ShowGraphic    = "显示 Graphic";

        public const string A_CreateRetImage = "创建结果图";
    }

    public static class UserAction
    {
        public const string A_Login          = "登录";

        public const string A_Logout         = "登出";

        public const string A_Add            = "新增";

        public const string A_Delete         = "删除";

        public const string A_ChangePassword = "修改密码";

        public const string A_ResetPassword  = "重置密码";
    }

    public static class CommServerAction
    {
        public const string A_Check      = "检查";

        public const string A_Listion    = "监听";

        public const string A_Connection = "连接";

        public const string A_Send       = "发送指令";

        public const string A_Recv       = "接收指令";

        public const string A_CmdAnalyze = "指令解析";
    }

}
