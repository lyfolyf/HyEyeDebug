using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    // 取像调度类，为了防止任务抢相机
    public class AcqScheduler
    {
        // key: cameraSN
        Dictionary<string, ConcurrentQueue<AcquireImage>> queues;

        // key: cameraSN
        Dictionary<string, object> syncs;

        readonly ITaskRepository taskRepo;
        readonly IGLog log;

        public AcqScheduler(ITaskRepository taskRepo, IGLog log)
        {
            this.taskRepo = taskRepo;
            this.log = log;
        }

        public void Init()
        {
            queues = new Dictionary<string, ConcurrentQueue<AcquireImage>>();
            syncs = new Dictionary<string, object>();

            List<TaskInfoVO> taskInfos = taskRepo.GetTasks();

            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                if (!queues.ContainsKey(taskInfo.CameraAcquireImage.CameraSN))
                {
                    queues[taskInfo.CameraAcquireImage.CameraSN] = new ConcurrentQueue<AcquireImage>();
                    syncs[taskInfo.CameraAcquireImage.CameraSN] = new object();
                }
            }
        }

        public void Reset()
        {
            if (queues == null) return;

            foreach (string cameraSN in syncs.Keys)
            {
                lock (syncs[cameraSN])
                {
                    if (queues[cameraSN].Count > 0)
                        queues[cameraSN] = new ConcurrentQueue<AcquireImage>();
                }
            }
        }

        internal void InitOneTask(TaskInfoVO taskInfo)
        {
            if (queues == null)
            {
                queues = new Dictionary<string, ConcurrentQueue<AcquireImage>>();
                syncs = new Dictionary<string, object>();
            }

            if (!queues.ContainsKey(taskInfo.CameraAcquireImage.CameraSN))
            {
                queues[taskInfo.CameraAcquireImage.CameraSN] = new ConcurrentQueue<AcquireImage>();
                syncs[taskInfo.CameraAcquireImage.CameraSN] = new object();
            }
        }

        public bool Request(string cameraSN, AcquireImage acquireImage)
        {
            lock (syncs[cameraSN])
            {
                var queue = queues[cameraSN];
                bool noWait = queue.IsEmpty;

                queue.Enqueue(acquireImage);
                if (!noWait)
                {
                    log.Info(new AcqImageLogMessage(acquireImage.taskInfo.Name, A_Wait, R_Start));
                }

                return noWait;
            }
        }

        public void Completed(string cameraSN, AcquireImage acquireImage)
        {
            var queue = queues[cameraSN];

            // 这里取出来的必定是要跟参数传入的 AcquireImage 是同一个对象
            queue.TryDequeue(out _);

            if (queue.TryPeek(out AcquireImage next))
            {
                log.Info(new AcqImageLogMessage(next.taskInfo.Name, A_Wait, R_End, "GoGoGo"));

                next.Go();
            }
        }

    }
}
