using GL.Kit.Log;

namespace HyEye.Models
{
    public class BaseGeneralLogMessage : LogMessage
    {
        public string Subject1 { get; set; }

        public string Subject2 { get; set; }

        public double? Time { get; set; }

        public BaseGeneralLogMessage() { }

        public BaseGeneralLogMessage(string subject1, string subject2,
            string action, string actionResult,
            string message = null, double? time = null)
        {
            Subject1 = subject1;
            Subject2 = subject2;
            Action = action;
            ActionResult = actionResult;
            Message = message;
            Time = time;
        }

        public override string ToString(LogFormat format)
        {
            if (format == LogFormat.CSV)
                return $"{Module},{Subject1},{Subject2},{Action},{ActionResult},{Message},{Time} ms";
            else
                return $"[{Subject1}]{(Subject2 != null && Subject2.Length > 0 ? $"[{Subject2}]" : string.Empty)} {Action}{ActionResult}{(Message != null ? $"，{Message}" : string.Empty)}";
        }
    }

    public class ImageLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "Image";

        public ImageLogMessage(string taskName, string acqImageName, string action, string actionResult,
            string message = null, double? time = null)
            : base(taskName, acqImageName, action, actionResult, message, time) { }
    }

    public class TaskServerLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "TaskServer";

        public TaskServerLogMessage(string taskName, string acqImageName, string action, string actionResult,
            string message = null, double? time = null)
            : base(taskName, acqImageName, action, actionResult, message, time) { }
    }

    public class TaskVisionLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "TaskVision";

        public TaskVisionLogMessage(string taskName, string acqImageName, string action, string actionResult,
            string message = null, double? time = null)
            : base(taskName, acqImageName, action, actionResult, message, time) { }
    }

    public class CalibServerLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "CalibServer";

        public CalibServerLogMessage(string taskName, string calibName, string action, string actionResult,
            string message = null, double? time = null)
            : base(taskName, calibName, action, actionResult, message, time) { }
    }

    public class CalibVisionLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "CalibVision";

        public CalibVisionLogMessage(string taskName, string calibName, string action, string actionResult,
            string message = null, double? time = null)
            : base(taskName, calibName, action, actionResult, message, time) { }
    }

    public class ApiLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "API";

        public ApiLogMessage(string subject1, string subject2, string action, string actionResult, string message = null)
            : base(subject1, subject2, action, actionResult, message) { }
    }

    public class CameraApiLogMessage : ApiLogMessage
    {
        public override string Module => "API";

        public CameraApiLogMessage(string cameraName, string action, string actionResult, string message = null)
            : base(cameraName, null, action, actionResult, message) { }
    }

    public class AcqImageLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "AcqImage";

        public AcqImageLogMessage(string taskName, string action, string actionResult,
            string message = null, double? time = null)
        {
            Subject1 = taskName;
            Action = action;
            ActionResult = actionResult;
            Message = message;
            Time = time;
        }
    }

    public sealed class UserLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "User";

        public UserLogMessage(string username, string action, string actionResult, string message = null)
        {
            Subject1 = username;
            Action = action;
            ActionResult = actionResult;
            Message = message;
        }
    }

    public class CommLogMessage : BaseGeneralLogMessage
    {
        public override string Module => "Comm";

        public CommLogMessage(string subject, string action, string actionResult, string message = null)
        {
            Subject1 = subject;
            Action = action;
            ActionResult = actionResult;
            Message = message;
        }
    }

}
