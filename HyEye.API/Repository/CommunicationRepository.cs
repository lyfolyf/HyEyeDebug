using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface ICommunicationRepository
    {
        CommunicationInfoVO GetCommunicationInfo();

        void SetCommunicationInfo(CommunicationInfoVO communicationInfo);

        void Save();
    }

    public class CommunicationRepository : ICommunicationRepository
    {
        readonly IMapper mapper;
        readonly ICommandRepository commandRepo;
        readonly IGLog log;

        public CommunicationRepository(
            IMapper mapper,
            ICommandRepository commandRepo,
            IGLog log)
        {
            this.mapper = mapper;
            this.commandRepo = commandRepo;
            this.log = log;
        }

        public CommunicationInfoVO GetCommunicationInfo()
        {
            return mapper.Map<CommunicationInfoVO>(ApiConfig.CommunicationConfig.CommunicationInfo);
        }

        public void SetCommunicationInfo(CommunicationInfoVO communicationInfo)
        {
            if (communicationInfo.CommProtocol == GL.Kit.Net.CommProtocol.PLC)
            {
                commandRepo.SendCmdFormat = true;
            }

            ApiConfig.CommunicationConfig.CommunicationInfo = mapper.Map<CommunicationInfo>(communicationInfo);
        }

        public void Save()
        {
            ApiConfig.Save(ApiConfig.CommunicationConfig);

            log.Info(new ApiLogMessage("通讯设置", null, A_Save, R_Success));

            commandRepo.Save();
        }
    }
}
