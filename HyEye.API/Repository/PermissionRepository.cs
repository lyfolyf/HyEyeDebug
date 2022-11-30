using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System.Collections.Generic;
using System.Linq;

namespace HyEye.API.Repository
{
    public interface IPermissionRepository
    {
        List<ControlPermissionVO> GetControlPermissions(string formName);
    }

    public class PermissionRepository : IPermissionRepository
    {
        readonly IMapper mapper;
        readonly IGLog log;

        readonly List<ControlPermission> controlPermissions;

        public PermissionRepository(IMapper mapper, IGLog log)
        {
            this.mapper = mapper;
            this.log = log;

            controlPermissions = ApiConfig.PermissionList.ControlPermissions;
        }

        public List<ControlPermissionVO> GetControlPermissions(string formName)
        {
            return mapper.Map<List<ControlPermissionVO>>(controlPermissions.Where(a => a.FormName == formName));
        }
    }
}
