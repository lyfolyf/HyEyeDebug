using AutoMapper;
using GL.Kit.Log;

namespace HyEye.API.Repository
{
    public interface IUserBlockRepository
    {
        void Save();
    }

    public class UserBlockRepository
    {
        readonly IGLog log;
        readonly IMapper mapper;

        public UserBlockRepository(IMapper mapper, IGLog log)
        {
            this.mapper = mapper;
            this.log = log;
        }

        public void Save()
        {

        }
    }
}
