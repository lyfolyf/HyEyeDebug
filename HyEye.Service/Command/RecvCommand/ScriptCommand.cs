namespace HyEye.Services
{
    /// <summary>
    /// 脚本指令
    /// </summary>
    public class ScriptCommand
    {
        /// <summary>
        /// 指令头
        /// </summary>
        public string CommandHeader { get; private set; } = ScriptCommandHeader;

        public string[] Args { get; private set; }

        public static ScriptCommand EmptyArgs { get; } = new ScriptCommand(ScriptCommandHeader);

        readonly string scriptCommandString;

        private ScriptCommand(string scriptCommandString)
        {
            this.scriptCommandString = scriptCommandString;
        }

        static string ScriptCommandHeader = CommandHeaderType.RS.ToString();

        public static bool IsScriptCommand(string str, out ScriptCommand cmd)
        {
            if (str == ScriptCommandHeader)
            {
                cmd = EmptyArgs;
                return true;
            }

            if (str.StartsWith(ScriptCommandHeader + ","))
            {
                cmd = new ScriptCommand(str);
                cmd.Args = str.Substring(ScriptCommandHeader.Length + 1)
                    .Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                return true;
            }
            else
            {
                cmd = null;
                return false;
            }
        }

        public override string ToString()
        {
            return scriptCommandString;
        }
    }
}
