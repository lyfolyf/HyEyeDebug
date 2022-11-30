using Autofac;
using CameraSDK;
using GL.Kit.Log;
using HyEye.API;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using HyEye.Services;
using HyVision.SDK;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using VisionSDK;
using VisionSDK._VisionPro;

namespace HyEye.WForm
{
    public class ConfigRegistration
    {
        public static void RegisterAutoFac()
        {
            AutoFacContainer.Register((builder) =>
            {
                Assembly models = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}HyEye.Models.dll");
                Assembly forms = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}HyEye.Form.dll");
                Assembly api = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}HyEye.API.dll");
                Assembly services = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}HyEye.Service.dll");
                Assembly visionFac = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}VisionFactory.dll");
                Assembly ivision = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}IVisionSDK.dll");
                Assembly visionPro = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}VisionProSDK.dll");
                Assembly visionHy = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}HyVision.dll");
                Assembly visionHySDK = Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}HyVision.SDK.dll");

                builder.RegisterModule(new AutoMapperModule(models));

                builder.RegisterAssemblyTypes(forms).Where(t => t.Name.StartsWith("Frm"));
                builder.RegisterAssemblyTypes(forms).Where(t => t.Name.StartsWith("Form")).SingleInstance();

                builder.RegisterAssemblyTypes(api)
                    .Where(t => t.Name.EndsWith("Repository"))
                    .SingleInstance()
                    .AsImplementedInterfaces();

                builder.RegisterAssemblyTypes(services)
                    .Where(t => t.Name.EndsWith("Service") || t.Name == "TaskUtils")
                    .SingleInstance()
                    .AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(services).Where(t => t.Name.EndsWith("Runner")).AsSelf().AsImplementedInterfaces();

                builder.RegisterType<AcquireImage>().As<IAcquireImage>();
                builder.RegisterType<AcqScheduler>().SingleInstance();

                //add by LuoDian @ 20211214 用于子料号的快速切换时，获取当前选择的子料号
                builder.RegisterType<MaterialRepository>().SingleInstance();

                builder.RegisterAssemblyTypes(ivision, visionPro, visionHy, visionHySDK)
                    .Where(t => t.Name.EndsWith("Component")).AsSelf().AsImplementedInterfaces();

                builder.Register<IToolBlockComponent>((c, p) =>
                {
                    TaskInfoVO task = p.Named<TaskInfoVO>("task");
                    if (task.Type == TaskType.VP)
                    {
                        return c.Resolve<VisionProToolBlockComponent>(p);
                    }
                    else if (task.Type == TaskType.HY || task.Type == TaskType.Extern)
                    {
                        return c.Resolve<HyToolBlockComponent>(p);
                    }
                    else
                    {
                        return null;
                    }
                });

                builder.RegisterAssemblyTypes(visionFac).Where(t => t.Name.EndsWith("Set")).SingleInstance();

                builder.RegisterAssemblyTypes(
                        Assembly.LoadFrom($@"{AppDomain.CurrentDomain.BaseDirectory}HyEye.exe")
                    ).Where(t => t.Name == "HyEyeAdvancedScript").SingleInstance().AsImplementedInterfaces();

                builder.RegisterType<Permission>().As<IPermission>().SingleInstance();
                builder.RegisterInstance(new LogPublisher()).As<IGLog>().AsSelf().SingleInstance();

                builder.RegisterType<CameraFactory>().As<ICameraFactoryCollection>().SingleInstance();
                builder.RegisterType<CameraSDKLog>().As<ICameraSDKLog>();
                builder.RegisterType<ControllerCollection>().As<IControllerCollection>().SingleInstance();

                builder.RegisterType<ComponentInitialization>();
            });

            AutoFacContainer.AfterResolve((entity) =>
            {
                if (entity is Form form)
                {
                    form.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                }
            });
        }

        public static void RegisterLog()
        {
            ILogRepository logConfig = AutoFacContainer.Resolve<ILogRepository>();

            LogPublisher log = AutoFacContainer.Resolve<LogPublisher>();

            //IGLogger log4netLog = Log4NetCreator.GetLog("HyEye1",
            //    extension: ".log.csv",
            //    maximumFileSize: logConfig.MaximumFileSize,
            //    maxSizeRollBackups: logConfig.MaxSizeRollBackups,
            //    level: logConfig.FileLevel,
            //    datePattern: "_yyyy-MM-dd",
            //    conversionPattern: "%date{yyyy-MM-dd HH-mm-ss.fff},%level,%thread,%message%newline");
            //log.AddLogger(log4netLog);

            DisplayAdapter displayAdapter = new DisplayAdapter(logConfig.DisplayMaxCount, LogFormat.General);
            IGLogger displayLog = GLogger.CreateLog("form", logConfig.DisplayLevel, displayAdapter);
            log.AddLogger(displayLog);
            AutoFacContainer.Resolve<FormLog>(new TypedParameter(typeof(DisplayAdapter), displayAdapter));

            GLogAdapter logAdapter = new GLogAdapter("HyEye", PathUtils.CurrentDirectory + "Log", 7, LogFormat.CSV);
            IGLogger fileLog = GLogger.CreateLog("HyEye", logConfig.FileLevel, logAdapter);
            log.AddLogger(fileLog);

            logConfig.FileLevelChanged += level =>
            {
                fileLog.Level = level;
            };
        }
    }
}
