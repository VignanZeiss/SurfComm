using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Service
{
    public static class SystemState
    {
        public static bool ServiceRunning { get; set; }
        public static string StatusMessage { get; set; } = "Stopped";
        public static DeviceState SurfComm { get; internal set; } = new DeviceState();
        public static List<string> ErrorMessages { get; internal set; } = new();
    }
    public class DeviceState
    {
        public string Response = "";

        public bool IsConnected { get; internal set; }
        public bool IsErrored { get; internal set; }
        public bool IsBusy { get; internal set; }
        public StatusMessages Status { get; internal set; } = StatusMessages.Stopped;

    }

    public enum StatusMessages
    {
        Stopped,
        Connected,
        Busy,
        Error
    }
}
