using GL.Kit;

namespace HyEye.API
{
    public class ApiException : MyException
    {
        public ApiException(string message)
            : base(message)
        {

        }
    }
}
