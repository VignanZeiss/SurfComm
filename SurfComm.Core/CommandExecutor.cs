using System;
using System.IO.Ports;
using System.Text;
using System.Threading;


namespace SurfComm.Core
{
    public class CommandExecutor
    {
        private readonly SerialPort _serial;

        public CommandExecutor(SerialPort serial)
        {
            _serial = serial;
        }

        public string ExecuteAll(CommandQueue queue)
        {
            StringBuilder log = new StringBuilder();

            foreach (var cmd in queue.Commands)
            {
                //string response = PortEnumerator.SendCommandAsync(cmd).Result;  // keep it simple for now
                log.AppendLine($"→ {cmd}");
                //log.AppendLine($"← {response}");
                Thread.Sleep(50); // small delay between commands
            }

            return log.ToString();
        }
    }
}
