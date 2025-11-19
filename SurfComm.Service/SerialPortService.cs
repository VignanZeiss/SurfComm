using SurfComm.Core;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Service
{
    
    public class SerialPortServiceRun
    {
        private readonly Queue<string> _commandQueue = new();
        private string cmd = string.Empty;
        private StringBuilder _receiveBuffer = new();
        private SerialPort? _port;

        public bool IsConnected => _port?.IsOpen ?? false;
        public string? CurrentPortName => _port?.PortName;

        /*################################################
         *                 RUNS CONTINUOUSLY               
         *#################################################
         */
        public void Run()
        {
            if (!SystemState.SurfComm.IsConnected)
            {
                string? PortName = PortEnumerator.GetSurfComPortName();
                Connect(PortName);
            }
           
            if (SystemState.SurfComm.IsConnected)
            {

                if (_commandQueue.Count > 0)
                {
                    ProcessQueue();  
                }
                ClearSurfCommIsBusy(cmd);

                _port.DataReceived += Port_DataReceived;

                

            }

            UpdateSurfCommStatus();

            //System.Diagnostics.Debug.WriteLine(SystemState.SurfComm.IsConnected);
        }

        //-----------------Connect Disconnect------------------------
        /// <summary>
        /// Connects the specified port name.
        /// </summary>
        /// <param name="portName">Name of the port.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <returns></returns>
        public bool Connect(string portName, int baudRate = 115200)
        {
            try
            {
                _port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
                {
                    Handshake = Handshake.None,
                    DtrEnable = true,
                    RtsEnable = true,
                    ReadTimeout = 2000,
                    WriteTimeout = 1000,
                    NewLine = "\r"
                };

                _port.Open();
                Logger.Log($"✅ Connected to {portName}");
                SystemState.SurfComm.IsConnected = true;
                SystemState.StatusMessage = $"Connected to {portName }";
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"❌ Connection failed: {ex.Message}");
                SystemState.SurfComm.IsConnected = false;
                SystemState.SurfComm.IsErrored = true;

                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_port != null && _port.IsOpen)
                {
                    _port.Close();
                    Logger.Log($"🔌 Disconnected from {_port.PortName}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"⚠ Disconnect error: {ex.Message}");
            }

            SystemState.SurfComm.IsConnected = false;
            SystemState.StatusMessage = "Disconnected";
        }

        //-----------------Command Handling----------------------------
        /// <summary>
        /// Add the incomming Command to end of the queue to be executed
        /// </summary>
        /// <param name="cmd">The command.</param>
        public void Enqueue(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd))
                return;

            _commandQueue.Enqueue(cmd);
            Logger.Log($"📝 Enqueued: {cmd}");
        }
        private void ProcessQueue()
        {
            
            if (!SystemState.SurfComm.IsConnected) return;
            if (SystemState.SurfComm.IsErrored) _commandQueue.Clear();
           
            if (!SystemState.SurfComm.IsBusy && !SystemState.SurfComm.IsErrored)
            {
                cmd = _commandQueue.Dequeue();
                SystemState.SurfComm.Response = string.Empty;
                SurfCommExecute(cmd);
                SystemState.SurfComm.IsBusy = true;
                
            }
           
        }
        private void ClearSurfCommIsBusy(string cmd)
        {
            if (SystemState.SurfComm.IsBusy)
            {
                if (cmd.Contains("SMEAS") && SystemState.SurfComm.Response.Contains("SMEAS80")) SystemState.SurfComm.IsBusy = false;
                else if (cmd.Contains("SDATA"))
                {
                    if (SystemState.SurfComm.Response.Contains("\x04"))
                        if (SaveResult(SystemState.SurfComm.Response))
                            SystemState.SurfComm.IsBusy = false;
                }
                else SystemState.SurfComm.IsBusy = false;
            }
        }


        /// <summary>
        /// Executes a command string and returns raw response.
        /// </summary>
        public void SurfCommExecute(string command)
        {
            if (_port == null || !_port.IsOpen)
                return;

            try
            {
                _port.WriteLine(command);
                Logger.Log($"→ {command}");
                SystemState.SurfComm.Response = string.Empty;
                
            }
            catch (TimeoutException)
            {
                Logger.Log("⚠ Timeout waiting for response");
                SystemState.SurfComm.IsErrored = true;
                Logger.Log("Error :❌ Could not execute Command : " + command);
                SystemState.ErrorMessages.Add("❌ Could not execute Command : " + command);
                
            }
            catch (Exception ex)
            {
                Logger.Log($"⚠ Communication error: {ex.Message}");
                
            }
        }

        

        void UpdateSurfCommStatus()
        {
            SystemState.SurfComm.IsConnected = false; // _port.IsOpen;
            if (!SystemState.SurfComm.IsConnected)
            {
                var edge = new EdgeDetector();
                SystemState.SurfComm.IsErrored = true;
                if (edge.FallingEdge(SystemState.SurfComm.IsConnected))
                {
                    Logger.Log("Lost connection to SurfComm");
                    SystemState.ErrorMessages.Add("Lost connection to SurfComm  ");
                }

            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var size = _port.ReadBufferSize;
                
                string data = _port.ReadExisting().TrimEnd();
                if (!string.IsNullOrEmpty(data))
                {
                    Logger.Log($"DataReceived : {data}");
                    SystemState.SurfComm.Response += data;
                }
                
            }
            catch (Exception ex)
            {
                Logger.Log($"❌ DataReceived error: {ex.Message}");
            }

        }
        public void Dispose() => Disconnect();

   
        public static bool SaveResult(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
                return true;

            try
            {
                // Make sure folder exists
                string basePath = FilePath.DocumentsFolder;
                string folder = Path.Combine(basePath, "Zeiss", "SurfComConnect");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                // Build file name with timestamp
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                string filePath = Path.Combine(folder, $"Result_{timestamp}.txt");

                // Split lines
                var lines = response
                    .Replace("\r", "")
                    .Split('\n')
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .ToList();

               
                // Build output
                var output = "==== SurfComm Result ====\n";
                output += $"Timestamp: {DateTime.Now}\n\n";

                foreach (var line in lines)
                    output += line + "\n";

                output += "\n=========================\n";

                // Write file
                File.WriteAllText(filePath, SystemState.SurfComm.Response);

                Logger.Log($"📝 Result Saved: {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"❌ Error saving result: {ex.Message}");
                return true;
            }
        }

        private static string CleanData(string raw)
        {
            // Remove <DATA> tag
            string cleaned = raw.Replace("<DATA>", "").Trim();

            // Example: "Ra,0.124" → "Ra = 0.124"
            if (cleaned.Contains(","))
            {
                var parts = cleaned.Split(',');
                if (parts.Length == 2)
                    return $"{parts[0].Trim()} = {parts[1].Trim()}";
            }

            return cleaned;
        }
    }

}


