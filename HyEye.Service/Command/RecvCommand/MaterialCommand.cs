namespace HyEye.Services
{
    public class MaterialCommand
    {
        /// <summary>
        /// 指令头
        /// </summary>
        public string CommandHeader { get; private set; } = MaterialCommandHeader;

        static readonly string MaterialCommandHeader = CommandHeaderType.MC.ToString();

        public int Index { get; set; }

        public static bool IsMaterialCommand(string str, out MaterialCommand cmd)
        {
            string[] infos = str.Split(new char[] { ',', '，' });

            if (infos[0] == MaterialCommandHeader)
            {
                if (infos.Length == 2 && int.TryParse(infos[1], out int index))
                {
                    cmd = new MaterialCommand { Index = index };
                }
                else
                {
                    cmd = null;
                }

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
            return $"{MaterialCommandHeader},{Index}";
        }
    }
}
