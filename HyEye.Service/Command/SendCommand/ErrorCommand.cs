using HyEye.Models;

namespace HyEye.Services
{
    /// <summary>
    /// 异常回复指令
    /// </summary>
    public class ErrorCommand : IReplyCommand
    {
        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public static ErrorCommand Create(int errCode)
        {
            return new ErrorCommand
            {
                ErrCode = errCode,
                ErrMsg = ErrorCodeConst.ErrorMessage(errCode)
            };
        }

        public override string ToString()
        {
            return $"{ErrCode},{ErrMsg}";
        }

        public string ToString(bool format, int decimalPlaces)
        {
            return $"{ErrCode},{ErrMsg}";
        }
    }
}
