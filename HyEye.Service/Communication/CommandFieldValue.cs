using HyEye.Models;

namespace HyEye.Services
{
    public class CommandFieldValue
    {
        public string Name { get; set; }

        public CommandFieldUse Use { get; set; }

        public object Value { get; set; }
    }
}
