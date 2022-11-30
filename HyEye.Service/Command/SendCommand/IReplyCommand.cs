namespace HyEye.Services
{
    /// <summary>
    /// 回复指令
    /// <para>分正常指令（sendCommand）和异常指令（ErrorCommand）</para>
    /// </summary>
    public interface IReplyCommand
    {
        string ToString(bool format, int decimalPlaces);
    }
}
