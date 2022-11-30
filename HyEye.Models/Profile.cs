using AutoMapper;
using CameraSDK.Models;
using HyEye.Models.VO;

namespace HyEye.Models
{
    #region

    public class CalibrationInfoProfile : Profile
    {
        public CalibrationInfoProfile()
        {
            CreateMap<CalibrationInfo, CalibrationInfoVO>();
            CreateMap<CalibrationInfoVO, CalibrationInfo>();

            CreateMap<CheckerboardInfo, CheckerboardInfoVO>();
            CreateMap<CheckerboardInfoVO, CheckerboardInfo>();

            CreateMap<HandEyeInfo, HandEyeInfoVO>();
            CreateMap<HandEyeInfoVO, HandEyeInfo>();

            CreateMap<HandEyeSingleInfo, HandEyeSingleInfoVO>();
            CreateMap<HandEyeSingleInfoVO, HandEyeSingleInfo>();

            CreateMap<JointInfo, JointInfoVO>();
            CreateMap<JointInfoVO, JointInfo>();

            CreateMap<TaskAcqImage, TaskAcqImageVO>();
            CreateMap<TaskAcqImageVO, TaskAcqImage>();
        }
    }

    public class CalibrationVerifyInfoProfile : Profile
    {
        public CalibrationVerifyInfoProfile()
        {
            CreateMap<CheckerboardVerifyInfo, CheckerboardVerifyInfoVO>();
            CreateMap<CheckerboardVerifyInfoVO, CheckerboardVerifyInfo>();

            CreateMap<HandeyeVerifyInfo, HandeyeVerifyInfoVO>();
            CreateMap<HandeyeVerifyInfoVO, HandeyeVerifyInfo>();
        }
    }

    public class CameraInfoProfile : Profile
    {
        public CameraInfoProfile()
        {
            CreateMap<CameraInfo, CameraInfoVO>();
            CreateMap<CameraInfoVO, CameraInfo>();

            CreateMap<ComCameraInfo, CameraInfoVO>();
            CreateMap<CameraInfoVO, ComCameraInfo>();

            CreateMap<ComCameraInfo, CameraInfo>();
            CreateMap<CameraInfo, ComCameraInfo>();
        }
    }

    public class CameraParamInfoProfile : Profile
    {
        public CameraParamInfoProfile()
        {
            CreateMap<CameraParamInfo, CameraParamInfoVO>();
            CreateMap<CameraParamInfoVO, CameraParamInfo>();

            CreateMap<CameraParamInfoList, CameraParamInfoListVO>();
            CreateMap<CameraParamInfoListVO, CameraParamInfoList>();
        }
    }

    public class CommunicationInfoProfile : Profile
    {
        public CommunicationInfoProfile()
        {
            CreateMap<CommunicationInfo, CommunicationInfoVO>();
            CreateMap<CommunicationInfoVO, CommunicationInfo>();
        }
    }

    public class CommandInfoProfile : Profile
    {
        public CommandInfoProfile()
        {
            CreateMap<CommandFieldInfo, CommandFieldInfoVO>();
            CreateMap<CommandFieldInfoVO, CommandFieldInfo>();

            CreateMap<ReceiveCommandInfo, ReceiveCommandInfoVO>();
            CreateMap<ReceiveCommandInfoVO, ReceiveCommandInfo>();

            CreateMap<SendCommandInfo, SendCommandInfoVO>();
            CreateMap<SendCommandInfoVO, SendCommandInfo>();
        }
    }

    public class PlcCommandInfoProfile : Profile
    {
        public PlcCommandInfoProfile()
        {
            CreateMap<PlcCommandInfo, PlcCommandInfoVO>();
            CreateMap<PlcCommandInfoVO, PlcCommandInfo>();

            CreateMap<PlcCommandFieldInfo, PlcCommandFieldInfoVO>();
            CreateMap<PlcCommandFieldInfoVO, PlcCommandFieldInfo>();
        }
    }

    public class LightControllerInfoProfile : Profile
    {
        public LightControllerInfoProfile()
        {
            CreateMap<ChannelLightInfo, ChannelLightInfoVO>();
            CreateMap<ChannelLightInfoVO, ChannelLightInfo>();

            CreateMap<LightControllerInfo, LightControllerInfoVO>();
            CreateMap<LightControllerInfoVO, LightControllerInfo>();
        }
    }

    public class OpticsInfoProfile : Profile
    {
        public OpticsInfoProfile()
        {
            CreateMap<OpticsInfo, OpticsInfoVO>();
            CreateMap<OpticsInfoVO, OpticsInfo>();

            CreateMap<LightControllerValueInfo, LightControllerValueInfoVO>();
            CreateMap<LightControllerValueInfoVO, LightControllerValueInfo>();
        }
    }

    public class TaskInfoProfile : Profile
    {
        public TaskInfoProfile()
        {
            CreateMap<TaskInfo, TaskInfoVO>();
            CreateMap<TaskInfoVO, TaskInfo>();

            CreateMap<CameraAcquireImageInfo, CameraAcquireImageInfoVO>();
            CreateMap<CameraAcquireImageInfoVO, CameraAcquireImageInfo>();

            CreateMap<AcquireImageInfo, AcquireImageInfoVO>();
            CreateMap<AcquireImageInfoVO, AcquireImageInfo>();
        }
    }

    public class TaskVisionMappingProfile : Profile
    {
        public TaskVisionMappingProfile()
        {
            CreateMap<TaskVisionMapping, TaskVisionMappingVO>();
            CreateMap<TaskVisionMappingVO, TaskVisionMapping>();

            CreateMap<NameMapper, NameMapperVO>();
            CreateMap<NameMapperVO, NameMapper>();
        }
    }

    public class ImageSaveInfoProfile : Profile
    {
        public ImageSaveInfoProfile()
        {
            CreateMap<ImageSaveInfo, ImageSaveInfoVO>();
            CreateMap<ImageSaveInfoVO, ImageSaveInfo>();

            CreateMap<ImageDeleteInfo, ImageDeleteInfoVO>();
            CreateMap<ImageDeleteInfoVO, ImageDeleteInfo>();
        }
    }

    #endregion

    #region

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVO>();
            CreateMap<UserVO, User>();
        }
    }

    public class ControlRoleProfile : Profile
    {
        public ControlRoleProfile()
        {
            CreateMap<ControlPermission, ControlPermissionVO>();
            CreateMap<ControlPermissionVO, ControlPermission>();
        }
    }

    public class DisplayLayoutInfoProfile : Profile
    {
        public DisplayLayoutInfoProfile()
        {
            CreateMap<DisplayLayoutInfo, DisplayLayoutInfoVO>();
            CreateMap<DisplayLayoutInfoVO, DisplayLayoutInfo>();
        }
    }

    #endregion

    public class UserBlockInfoProfile : Profile
    {
        public UserBlockInfoProfile()
        {
            CreateMap<UserBlockInfo, UserBlockInfoVO>();
            CreateMap<UserBlockInfoVO, UserBlockInfo>();
        }
    }

    public class MaterialInfoProfile : Profile
    {
        public MaterialInfoProfile()
        {
            CreateMap<MaterialInfo, MaterialInfoVO>();
            CreateMap<MaterialInfoVO, MaterialInfo>();
        }
    }

    public class RecordShowInfoProfile : Profile
    {
        public RecordShowInfoProfile()
        {
            CreateMap<RecordShowInfo, RecordShowInfoVO>();
            CreateMap<RecordShowInfoVO, RecordShowInfo>();
        }
    }

}
