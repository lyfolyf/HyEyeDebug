using System;
using System.Text.RegularExpressions;

namespace HyEye.Models
{
    [Serializable]
    public struct CommandHeader
    {
        public string Name { get; set; }

        public int Index { get; set; }

        public CommandHeader(string name, int index)
        {
            if (index < 0)
                throw new Exception("无效的指令头");

            Name = name;
            Index = index;
        }

        static readonly Regex regex = new Regex("^([A-Za-z]+)(\\d+)$");

        public static implicit operator string(CommandHeader header)
        {
            return header.Name + header.Index.ToString();
        }

        public static implicit operator CommandHeader(string str)
        {
            Match match = regex.Match(str);

            if (!match.Success)
                throw new Exception("无效的指令头");

            return new CommandHeader(match.Groups[1].Value.ToUpper(), int.Parse(match.Groups[2].Value));
        }

        public override string ToString()
        {
            return Name + Index.ToString();
        }
    }
}
