using GL.Kit.Log;
using GL.Kit.Native;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    public interface IImageService : IDisposable
    {
        bool Running { get; }

        void Start();

        void Stop();

        void SaveImage(HyImageInfo hyImage, bool isSrc, bool isCalibration, ImageFlag flag);
    }

    public class ImageService : IImageService
    {
        readonly IImageSaveRepository imgSaveRepo;
        readonly IGLog log;

        AbstractImageSave imageSave;
        AbstractImageDelete imageDelete;

        const string DELETEFILE = "DeleteInfo";
        const long GSize = 1024 * 1024 * 1024;

        List<ImageSaveInfoVO> saveInfos;

        public ImageService(
            IImageSaveRepository imgSaveRepo,
            IGLog log)
        {
            this.imgSaveRepo = imgSaveRepo;
            this.log = log;

            this.imgSaveRepo.AfterSave += ImgSaveRepo_AfterSave;
        }

        private void ImgSaveRepo_AfterSave()
        {
            if (!Running) return;

            Stop();
            Start();
        }

        public bool Running { get; private set; }

        public void Start()
        {
            if (Running) return;

            CreateDirectory();

            saveInfos = imgSaveRepo.GetSaveInfos();
            ImageDeleteInfoVO deleteInfo = imgSaveRepo.GetDeleteInfo();

            imageSave = AbstractImageSave.Create(imgSaveRepo, log);
            imageSave.Start();
            //ImageService开始，才开始扫描服务。AbstractImageSave本身的Start不开始扫描服务
            imageSave.StartScan();

            if (deleteInfo.DeleteMode != ImageDeleteMode.NoDelete)
            {
                imageDelete = AbstractImageDelete.Create(imgSaveRepo.Root, deleteInfo, log);
                imageDelete.Start();
            }

            Running = true;
        }

        void CreateDirectory()
        {
            Directory.CreateDirectory(imgSaveRepo.Root);
            // 新建隐藏文件夹，用于存放删除信息文件
            string hiddenDire = PathUtils.GetAbsolutePath(imgSaveRepo.Root + "\\" + DELETEFILE);
            Directory.CreateDirectory(hiddenDire);
            File.SetAttributes(hiddenDire, FileAttributes.Hidden);
        }

        public void Stop()
        {
            if (!Running) return;

            imageSave.Stop();
            //ImageService暂停，才暂停扫描服务。AbstractImageSave本身的Stop不暂停扫描服务（停止存图服务时，扫描服务能保持运行）
            imageSave.StopScan();

            if (imageDelete != null)
            {
                imageDelete.Stop();
                imageDelete = null;
            }

            Running = false;
        }

        public void SaveImage(HyImageInfo hyImage, bool isSrc, bool isCalibration, ImageFlag flag)
        {
            if (!Running) return;

            ImageSaveInfoVO saveInfo = saveInfos.FirstOrDefault(a => a.TaskName == hyImage.TaskName);
            if (saveInfo == null) return;

            ImageInfo imageInfo = new ImageInfo
            {
                HyImage = hyImage,
                IsSrc = isSrc,
                IsCalibration = isCalibration,
                Flag = flag,
                SaveInfo = saveInfo,
            };

            imageSave.SaveImage(imageInfo);
        }

        public void Dispose()
        {
            if (imageDelete != null)
            {
                imageDelete.Dispose();

                imageDelete = null;
            }
        }

        #region ImageSave

        abstract class AbstractImageSave
        {
            public static AbstractImageSave Create(IImageSaveRepository imgSaveRepo, IGLog log)
            {
                ImageDeleteInfoVO deleteInfo = imgSaveRepo.GetDeleteInfo();

                // 不管何种删除方式，图像都是以日期文件夹存放的

                if (deleteInfo.DeleteMode == ImageDeleteMode.NoDelete || deleteInfo.DeleteMode == ImageDeleteMode.DefiniteTime)
                {
                    // 定时删除以天为删除单位，删除信息文件一天一个
                    // 不删除的话，不用写删除信息文件
                    return new ImageSaveByDate(imgSaveRepo, log);
                }
                else if (deleteInfo.DeleteMode == ImageDeleteMode.Cycle)
                {
                    // 周期删除每次删除指定大小，一个删除信息文件内所包含的图片大小就是要删除的大小
                    return new ImageSaveBySize(imgSaveRepo, log);
                }
                else
                {
                    return null;
                }
            }

            protected IGLog log;

            // 写文件时用的锁对象
            protected object async = new object();

            readonly bool lowestPriority;

            readonly int ScanTime;

            readonly int MinSpace;

            readonly int CacheNum;

            readonly IImageSaveRepository ImgSaveRepo;

            protected string Root { get; }

            protected readonly SubDireOfSaveImage SubDire;

            protected readonly ImageDeleteInfoVO DeleteInfo;

            Thread saveThread;

            Thread scanSpace;

            readonly BlockingCollection<ImageInfo> blocking = new BlockingCollection<ImageInfo>();

            // 仅有一个 SN 时，Key 为 TaskName
            // 每次拍照都有 SN 时，Key 为 TaskName+AcqIndex
            // value 为 SN
            Dictionary<string, string> snCache = new Dictionary<string, string>();
            // key 同上，value 是暂时没有获取到 SN 的图片信息
            Dictionary<string, ConcurrentQueue<ImageInfo>> imgCache = new Dictionary<string, ConcurrentQueue<ImageInfo>>();

            public AbstractImageSave(IImageSaveRepository imgSaveRepo, IGLog log)
            {
                lowestPriority = imgSaveRepo.LowestPriority;
                ScanTime = imgSaveRepo.ScanTime;
                MinSpace = imgSaveRepo.MinSpace;
                CacheNum = imgSaveRepo.CacheNum;

                Root = PathUtils.GetAbsolutePath(imgSaveRepo.Root);
                SubDire = imgSaveRepo.SubDirectory;
                DeleteInfo = imgSaveRepo.GetDeleteInfo();

                this.ImgSaveRepo = imgSaveRepo;
                this.log = log;
            }

            /// <summary>
            /// 存图服务是否在运行
            /// </summary>
            bool imgSaveIsRun;

            //存图线程开启
            public void Start()
            {
                imgSaveIsRun = true;

                imgSaveIsAccept = true;

                saveThread = new Thread(Running);
                saveThread.IsBackground = true;
                if (lowestPriority)
                    saveThread.Priority = ThreadPriority.Lowest;
                saveThread.Start();

                log.Info(new ImageLogMessage("图像保存服务", string.Empty, A_Start, R_Success, lowestPriority ? "优先级：低" : null));
            }

            //存图线程停止
            public void Stop()
            {
                imgSaveIsRun = false;

                blocking.CompleteAdding();

                log.Info(new ImageLogMessage("图像保存服务", string.Empty, A_Stop, R_Success));
            }

            //存图线程代码运行
            public void StartRun()
            {
                imgSaveIsRun = true;

                log.Info(new ImageLogMessage("图片保存", string.Empty, A_Start, R_Tip, "开启存图"));
            }

            //存图线程代码不运行
            public void StopRun()
            {
                imgSaveIsRun = false;

                log.Info(new ImageLogMessage("图片保存", string.Empty, A_Stop, R_Tip, "停止存图"));
            }

            /// <summary>
            /// 扫描服务是否运行
            /// </summary>
            bool imgSaveScanIsRun;

            //开启扫描磁盘线程
            public void StartScan()
            {
                imgSaveScanIsRun = true;

                scanSpace = new Thread(ScanSaveSpace);
                scanSpace.IsBackground = true;
                if (lowestPriority)//扫描服务也加上优先级，跟存图一样
                    scanSpace.Priority = ThreadPriority.Lowest;
                scanSpace.Start();
                log.Info(new ImageLogMessage("存图磁盘扫描服务", string.Empty, A_Start, R_Success, lowestPriority ? "优先级：低" : null));
            }

            //停止扫描磁盘线程
            public void StopScan()
            {
                imgSaveScanIsRun = false;

                log.Info(new ImageLogMessage("存图磁盘扫描服务", string.Empty, A_Stop, R_Success));
            }

            void Running()
            {
                while (!blocking.IsCompleted)
                {
                    //通过存图标志位来判断是否执行存图代码。这样线程不断，因磁盘空间或图片缓存触发的开启/关闭存图，可用此来实现。
                    if (imgSaveIsRun)
                    {
                        ImageInfo info;

                        try
                        {
                            info = blocking.Take();
                        }
                        catch (InvalidOperationException)
                        {
                            break;
                        }

                        try
                        {
                            if (info != null)
                            {
                                if (info.HyImage.Bitmap == null) continue;

                                string directory = Root + info.GetDirectory(SubDire);
                                Directory.CreateDirectory(directory);

                                string fn = info.GetFilename(SubDire);

                                string fullname = $@"{directory}\{fn}";

                                if (DeleteInfo.DeleteMode != ImageDeleteMode.NoDelete)
                                {
                                    lock (async)
                                    {
                                        using (StreamWriter sw = new StreamWriter(GetDeleteFilename(info), true, Encoding.UTF8))
                                        {
                                            sw.WriteLine(fullname);
                                        }
                                    }
                                }

                                long size = info.ImageSize();
                                GetSize(size);

                                TimeSpan ts = FuncWatch.ElapsedTime(() =>
                                {
                                    info.Save(Root, SubDire);
                                    info.Dispose();
                                });

                                if (ts.TotalMilliseconds > 200)
                                    log?.Warn(new ImageLogMessage(info.HyImage.TaskName, info.HyImage.AcqOrCalibName, A_SaveImage, R_Success, $"[{info.HyImage.CmdID}] {fn}，剩余 {blocking.Count}", ts.TotalMilliseconds));
                                else
                                    log?.Warn(new ImageLogMessage(info.HyImage.TaskName, info.HyImage.AcqOrCalibName, A_SaveImage, R_Success, $"[{info.HyImage.CmdID}] {fn}，剩余 {blocking.Count}", ts.TotalMilliseconds));
                                info = null;

                                //只要缓存超过设置的值，便不再缓存图片。不论是否SN命名，再调此方法，将停止存图，且不进行缓存
                                if (blocking.Count > CacheNum)
                                {
                                    imgSaveIsAccept = false;
                                    log.Error(new ImageLogMessage("存图缓存", string.Empty, A_Stop, R_Tip, $"缓存队列数{blocking.Count}，超过设置的{CacheNum}。缓存队列不再接收图片。"));
                                }

                                //缓存恢复为0的时候，且之前是不接收图片的状态。则缓存队列重新接收存图。
                                if (blocking.Count == 0 && !imgSaveIsAccept)
                                {
                                    imgSaveIsAccept = true;
                                    log.Info(new ImageLogMessage("存图缓存", string.Empty, A_Start, R_Tip, "缓存队列继续接收图片。"));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            log?.Error(new ImageLogMessage("图像保存服务", string.Empty, A_SaveImage, R_Fail, $"\"{e}\""));
                        }
                    }
                    else
                    {
                        //在暂停执行存图的方法时，线程间隔50ms
                        Thread.Sleep(50);
                    }
                }

                log?.Debug(new ImageLogMessage("图像保存服务", string.Empty, A_Stop, R_Success, "存图服务结束"));
            }

            bool imgSaveIsAccept;

            void ScanSaveSpace()
            {
                //存图服务运行，则执行扫描磁盘
                while (imgSaveScanIsRun)
                {
                    string diskName = Root.Substring(0, 1);
                    long freeSpace = DriveUtils.GetTotalFreeSpace(diskName);
                    int _freeSpace = (int)(freeSpace / 1024 / 1024 / 1024);

                    //小于设置的最小空间剩余，则停止存图。
                    if (_freeSpace < MinSpace)
                    {
                        log.Error(new ImageLogMessage("图片保存", string.Empty, A_Stop, R_Tip, $"剩余空间大小:{_freeSpace}GB，小于设置的最小提醒空间:{MinSpace}GB。停止存图方法。"));
                        log.Error(new ImageLogMessage("图片保存", string.Empty, A_Stop, R_Tip, $"剩余空间大小:{_freeSpace}GB，小于设置的最小提醒空间:{MinSpace}GB。停止存图方法。"));
                        log.Error(new ImageLogMessage("图片保存", string.Empty, A_Stop, R_Tip, $"剩余空间大小:{_freeSpace}GB，小于设置的最小提醒空间:{MinSpace}GB。停止存图方法。"));

                        //暂存，以免又要改成这样
                        ////将存图设置全部取消勾选，即所有图片不存。
                        //List<ImageSaveInfoVO> saveinfos = ImgSaveRepo.GetSaveInfos();
                        //foreach (ImageSaveInfoVO item in saveinfos)
                        //{
                        //    item.SrcSaveMode = ImageSaveMode.None;
                        //    item.ResultSaveMode = ImageSaveMode.None;
                        //}

                        //ImgSaveRepo.SetInfo(saveinfos, ImgSaveRepo.GetDeleteInfo());
                        ////原Save方法有AfterSave回调，会反复触发ImageService里的Stop()和Start()，然后再反复触发本循环，进入一个死循环
                        //ImgSaveRepo.SaveForImgNoAfterSave();
                        //log.Error(new ImageLogMessage("图片保存设置", string.Empty, A_SetParams, R_Tip, $"所有图像已设置为不保存。"));

                        //如果尚未停止存图，则停止存图服务。
                        //（在开启的前提下，关闭）
                        if (imgSaveIsRun)
                            StopRun();
                    }
                    else
                    {
                        //大于等于设置的最小空间剩余，则自动重新开启存图服务。
                        //（已经关闭的前提下，开启）
                        if (!imgSaveIsRun)
                            StartRun();
                    }

                    Thread.Sleep(TimeSpan.FromMinutes(ScanTime));
                }
            }



            public void SaveImage(ImageInfo imageInfo)
            {
                //只有存图服务在运行时
                //允许接收图片时
                //才添加图片
                if (imgSaveIsRun && imgSaveIsAccept)
                {
                    if (imageInfo.SaveInfo.NamedBySN)
                    {
                        SaveImageBySN(imageInfo);
                    }
                    else
                    {
                        AddToQueue(imageInfo);
                    }
                }
            }

            void SaveImageBySN(ImageInfo imageInfo)
            {
                string key;
                if (imageInfo.SaveInfo.OnlyOneSN)
                {
                    // 仅有一个 SN
                    key = imageInfo.HyImage.TaskName;

                    // 清 SN 缓存
                    if (imageInfo.IsSrc && imageInfo.HyImage.AcqOrCalibIndex == 1)
                    {
                        snCache[key] = null;
                    }

                    if (imageInfo.HyImage.AcqOrCalibIndex < imageInfo.SaveInfo.AcqIndexHasSN)
                    {
                        imageInfo.HyImage.SN = null;
                    }
                    else if (imageInfo.HyImage.AcqOrCalibIndex == imageInfo.SaveInfo.AcqIndexHasSN)
                    {
                        GetOrCacheSN(key, imageInfo);
                    }
                    else
                    {
                        imageInfo.HyImage.SN = snCache[key];
                    }
                }
                else
                {
                    // 每次拍照都有 SN

                    key = imageInfo.HyImage.TaskName + imageInfo.HyImage.AcqOrCalibIndex.ToString();

                    // 清 SN 缓存
                    if (imageInfo.IsSrc)
                        snCache[key] = null;

                    GetOrCacheSN(key, imageInfo);
                }

                if (imageInfo.HyImage.SN == null)
                {
                    imgCache.GetValue(key).Enqueue(imageInfo);
                }
                else
                {
                    SaveCacheImage(key);

                    AddToQueue(imageInfo);
                }
            }

            void GetOrCacheSN(string key, ImageInfo imageInfo)
            {
                if (string.IsNullOrEmpty(imageInfo.HyImage.SN))
                {
                    // 结果图不带 SN，则原图必须带 SN，否则认为解码失败，SN 赋 999
                    if (snCache.ContainsKey(key) && snCache[key] != null)
                        imageInfo.HyImage.SN = snCache[key];
                    else
                    {
                        imageInfo.HyImage.SN = InputOutputConst.ErrorValue_String;
                        snCache[key] = InputOutputConst.ErrorValue_String;
                    }
                }
                else
                {
                    snCache[key] = imageInfo.HyImage.SN;
                }
            }

            void AddToQueue(ImageInfo imageInfo)
            {
                if (!blocking.IsAddingCompleted)
                {
                    blocking.Add(imageInfo);
                }
            }

            void SaveCacheImage(string key)
            {
                string sn = snCache[key];
                if (imgCache.Count > 0)
                {
                    while (imgCache[key].TryDequeue(out ImageInfo imageInfo))
                    {
                        imageInfo.HyImage.SN = sn;

                        AddToQueue(imageInfo);
                    }
                }
            }

            //统计图片总大小
            protected abstract void GetSize(long size);

            protected abstract string GetDeleteFilename(ImageInfo info);
        }

        class ImageSaveByDate : AbstractImageSave
        {
            public ImageSaveByDate(IImageSaveRepository imgSaveRepo, IGLog log)
                : base(imgSaveRepo, log) { }

            protected override string GetDeleteFilename(ImageInfo info)
            {
                return $@"{Root}\{DELETEFILE}\{info.HyImage.Timestamp:yyyy-MM-dd}.txt";
            }

            protected override void GetSize(long size) { }
        }

        class ImageSaveBySize : AbstractImageSave
        {
            long deleteSize;
            long curSize;

            string delFilename;

            public ImageSaveBySize(IImageSaveRepository imgSaveRepo, IGLog log)
                : base(imgSaveRepo, log)
            {
                deleteSize = DeleteInfo.CycleDelete.DeleteSize * GSize;
            }

            protected override string GetDeleteFilename(ImageInfo info)
            {
                if (delFilename == null)
                {
                    delFilename = $@"{Root}\{DELETEFILE}\{info.HyImage.Timestamp:yyyy-MM-dd HH-mm}.txt";
                }
                else
                {
                    if (curSize > deleteSize)
                    {
                        delFilename = $@"{Root}\{DELETEFILE}\{info.HyImage.Timestamp:yyyy-MM-dd HH-mm}.txt";
                        curSize = 0;
                    }
                }

                return delFilename;
            }

            protected override void GetSize(long size)
            {
                curSize += size;
            }
        }

        #endregion

        #region ImageDelete

        abstract class AbstractImageDelete : IDisposable
        {
            public static AbstractImageDelete Create(string root, ImageDeleteInfoVO deleteInfo, IGLog log)
            {
                if (deleteInfo.DeleteMode == ImageDeleteMode.Cycle)
                    return new ImageCycleDelete(root, deleteInfo.CycleDelete, log);
                else if (deleteInfo.DeleteMode == ImageDeleteMode.DefiniteTime)
                    return new ImageDefiniteTimeDelete(root, deleteInfo.DefiniteTimeDelete, log);
                else
                    return null;
            }

            protected string DeleteFilePath { get; set; }

            protected Timer timer;

            protected IGLog log;

            public AbstractImageDelete(string root, IGLog log)
            {
                DeleteFilePath = $@"{PathUtils.GetAbsolutePath(root)}\{DELETEFILE}";

                this.log = log;
            }

            public abstract void Start();

            public void Stop()
            {
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;

                    log.Info(new ImageLogMessage("图像删除服务", string.Empty, A_Stop, R_Success));
                }
            }

            public void DeleteMethod(string delFilename)
            {
                using (StreamReader sr = new StreamReader(delFilename, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            if (File.Exists(line))
                                File.Delete(line);
                        }
                        catch (Exception)
                        {

                        }

                        Thread.Sleep(10);
                    }
                }

                File.Delete(delFilename);
            }

            public void Dispose()
            {
                Stop();
            }
        }

        // 周期删除
        class ImageCycleDelete : AbstractImageDelete
        {
            readonly CycleDeleteInfo config;

            // 磁盘总大小
            readonly long totalSize;

            public ImageCycleDelete(string root, CycleDeleteInfo config, IGLog log)
                : base(root, log)
            {
                this.config = config;

                if (config.Condition == ImageDeleteCondition.DiskUsageExceeds)
                    totalSize = DriveUtils.GetTotalSize(Path.GetPathRoot(DeleteFilePath));
            }

            public override void Start()
            {
                timer = new Timer(CallBack, config, TimeSpan.Zero, TimeSpan.FromMinutes(config.CycleMin));

                log.Info(new ImageLogMessage("图像删除服务", string.Empty, A_Start, R_Success, "删除模式：周期删除"));
            }

            private void CallBack(object state)
            {
                while (CheckCondition())
                {
                    string[] delFilenames = Directory.GetFiles(DeleteFilePath).OrderBy(s => s).ToArray();
                    if (delFilenames.Length == 1) return;

                    DeleteMethod(delFilenames[0]);
                }
            }

            private bool CheckCondition()
            {
                long freeSize = DriveUtils.GetTotalFreeSpace(Path.GetPathRoot(DeleteFilePath));

                if (config.Condition == ImageDeleteCondition.DiskFreeLess)
                {
                    return freeSize < config.CriticalValue * GSize;
                }
                else if (config.Condition == ImageDeleteCondition.DiskUsageExceeds)
                {
                    return (totalSize - freeSize) > config.CriticalValue * GSize;
                }

                return false;
            }
        }

        // 定时删除：图片按日期保存
        class ImageDefiniteTimeDelete : AbstractImageDelete
        {
            readonly DefiniteTimeDeleteInfo config;

            public ImageDefiniteTimeDelete(string root, DefiniteTimeDeleteInfo config, IGLog log)
                : base(root, log)
            {
                this.config = config;
            }

            public override void Start()
            {
                // 定时删除每天执行一次
                DateTime begintime = DateTime.Today.Add(config.StartTime());

                // 这里是计算下次执行的时间
                int dueTime;
                if (begintime >= DateTime.Now)
                    dueTime = (int)(begintime - DateTime.Now).TotalMilliseconds;
                else
                    dueTime = (int)(begintime.AddDays(1) - DateTime.Now).TotalMilliseconds;

                timer = new Timer(CallBack, config, dueTime, (int)TimeSpan.FromDays(1).TotalMilliseconds);

                log.Info(new ImageLogMessage("图像删除服务", string.Empty, A_Start, R_Success, "删除模式：定时删除"));
            }

            private void CallBack(object state)
            {
                string[] delFilenames = Directory.GetFiles(DeleteFilePath).OrderBy(s => s).ToArray(); ;

                foreach (string fn in delFilenames)
                {
                    DateTime writeDay = DateTime.Parse(Path.GetFileNameWithoutExtension(fn));

                    if ((DateTime.Now - writeDay).TotalDays > config.RetentionDays)
                    {
                        DeleteMethod(fn);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        #endregion
    }
}
