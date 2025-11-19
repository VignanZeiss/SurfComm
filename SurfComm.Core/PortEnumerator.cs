
using System.IO.Ports;
using System.Management;
using System.Runtime.CompilerServices;

namespace SurfComm.Core
{
    public class PortEnumerator
    {
        /**
         * Description:
        *  ------------------------------------------------------------------------------
        *  Provides full management of serial (COM) ports for SurfComm devices.
        *  This class allows:
        *    • Enumerating all available COM ports on the system
        *    • Detecting SurfComm devices (based on VID/PID)
        *    • Establishing and closing serial connections
        *    • Sending commands asynchronously and reading device responses
        */


        private SerialPort? _port ;

        public bool IsConnected => _port?.IsOpen ?? false;
        public string? PortName => _port?.PortName;



        /// <summary>
        /// Returns a list of available COM port names (e.g., "COM3", "COM5").
        /// </summary>
        public static List<string> GetListOfSerialPorts()
        {
            string[] ports = SerialPort.GetPortNames();

            var portList = new List<string>(ports);
            return portList;
        }


        /// <summary>
        /// Gets all COM ports.
        /// </summary>
        /// <returns></returns>
        public static List<ComPortInfo> GetAllComPorts()
        {
            var ports = new List<ComPortInfo>();

            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'");

            foreach (var obj in searcher.Get())
            {
                string name = obj["Name"]?.ToString() ?? string.Empty;
                string deviceId = obj["DeviceId"]?.ToString() ?? string.Empty;
                string? portName = ExtractPortNumberFrom(name);
                bool isSurfCom;
                if (deviceId.Contains("VID_1A49") && deviceId.Contains("PID_00FF"))
                    isSurfCom = true;
                else
                    isSurfCom = false;

                if (!string.IsNullOrEmpty(portName))
                {
                    ports.Add(new ComPortInfo
                    {
                        PortName = portName,
                        FriendlyName = name,
                        DeviceId = deviceId,
                        LookLikeSurfComm = isSurfCom
                    });
                }
                
            }
            return ports;
            
        }

        /// <summary>
        /// Extracts the port number from.
        /// </summary>
        /// <param name="Friendly Name">The name.</param>
        /// <returns></returns>
        private static string ExtractPortNumberFrom(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            int start = name.LastIndexOf("(COM");
            int end = name.IndexOf(")", start);
            if (start < 0) return string.Empty;
            else if (start > 0)
                return name.Substring(start + 1, end - start - 1);
            else
                return string.Empty;
        }

        /// <summary>
        /// Determines whether [is surf comm pluged in].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is surf comm pluged in]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSurfCommPlugedIn()
        {
            var ports = GetAllComPorts();
            foreach (var port in ports)
            {
                if (port.LookLikeSurfComm)
                    return true;
            }
            return false;
        }

        public static string GetSurfComPortName()
        {
            var ports = GetAllComPorts();
            foreach (var port in ports)
            {
                if (port.LookLikeSurfComm)
                    return port.PortName;
            }
            return null;
        }
        //
        


        /// <summary>
        /// Sends the command asynchronous.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>Responce String</returns>
        public  async Task<string> SendCommandAsync(string command)
        {
            
            if (_port == null || !_port.IsOpen)
                return " Not connected.";

            try
            {
                _port.WriteLine(command);
                await Task.Delay(100); // allow device to process
                string response = _port.ReadExisting();
                return string.IsNullOrWhiteSpace(response) ? "No response." : response.Trim();
            }
            catch (TimeoutException)
            {
                return " Timeout waiting for response.";
            }
            catch (Exception ex)
            {
                return $" Error: {ex.Message}";
            }
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            if (_port != null)
            {
                _port.Close();
                _port.Dispose();
            }
        }

    }
}
