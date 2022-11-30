using GL.Kit;

namespace HyEye.Services
{
    public class ServiceException : MyException
    {
        public ServiceException(string message)
            : base(message)
        {
            
        }
    }
}
