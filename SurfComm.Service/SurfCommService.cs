using Foundatio.Messaging;
using Foundatio.Resilience;
using Foundatio.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Service
{
    public class SurfCommService
    {
        private SerialPort _serialPort;

        public bool IsConnected { get; private set; }
        public string PortName { get; }
        public event Action<string>? DataReceived;
        public event Action? Connected;
        public event Action? Disconnected;
        public event Action<string>? FullMessageReceived;


        private CancellationTokenSource? _cts;
        private readonly List<string> _messageLines = new();
        private const char EOT = (char)4;


        public SurfCommService(string portName)
        {
            PortName = portName;
            _serialPort = new SerialPort(PortName, 115200, Parity.None, 8, StopBits.One);
        }


        public async Task<bool> ConnectAsync()
        {
            try
            {
                if (_serialPort?.IsOpen == true) return _serialPort.IsOpen;
                Logger.Log("Attempting to open serial port on " + PortName);
                _serialPort?.Open();
                if (_serialPort?.IsOpen == true)
                {
                    Logger.Log("Serial port has been opened");
                    IsConnected = true;
                    _cts = new CancellationTokenSource();
                    Task.Run(() => ReadLoop(_cts.Token));
                    Connected?.Invoke();
                    DataReceived += AssembleMessage;
                }


            }
            catch (Exception ex)
            {
                Logger.Log("Could not open serial port. \n " + ex.Message);
                return false;
            }

            return _serialPort.IsOpen;
        }

        public void Disconnect()
        {
            if (_serialPort?.IsOpen == false) return;
            _cts?.Cancel();
            _serialPort?.Close();

            IsConnected = false;

            Logger.Log("Serial port has been closed");
            Disconnected?.Invoke();


        }

        private async Task ReadLoop(CancellationToken token)
        {

            while (_serialPort?.IsOpen == true && !token.IsCancellationRequested)
            {
                try
                {
                    string response = _serialPort.ReadExisting();
                    if (!string.IsNullOrEmpty(response))
                    {
                        Logger.Log("<-- " + response);
                        DataReceived?.Invoke(response);

                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Serial Port ReadLoop broken \n" + ex.Message);

                }
                await Task.Delay(25);
            }
        }

        private void AssembleMessage(string chunk)
        {
            var lines = chunk.Split('\r');
            foreach (string ln in lines)
            {
                if (ln.Contains(EOT))
                    continue;

                if (!string.IsNullOrWhiteSpace(ln))
                    _messageLines.Add(ln);


            }
            if (chunk.Contains(EOT))
            {
                string fullMessage = string.Join("\r", _messageLines);
                FullMessageReceived?.Invoke(fullMessage);
                _messageLines.Clear();
            }
        }


        public Task<bool> SendCommandAsync(string command)

        {
            try
            {
                if (_serialPort?.IsOpen == true)
                {
                    if (string.IsNullOrWhiteSpace(command)) return Task.FromResult(false);

                    if (!command.EndsWith("\r")) command += "\r";
                    Logger.Log("--> " + command);
                    _serialPort?.Write(command);
                    return Task.FromResult(true);

                }
                else
                {
                    Logger.Log("Serial Port not connected. Could not send command");
                    return Task.FromResult(false);

                }
            }
            catch (Exception ex)
            {
                Logger.Log(" Error while sending command: " + ex.Message);
                return Task.FromResult(false);
            }

        }

        public async Task<string?> SendCommandAndWaitForMessageAsync(string command, int timeoutMs = 2000)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

           var sent = await SendCommandAsync(command);

            if (!sent) return null;

            void message(string msg)
            {
                tcs.TrySetResult(msg);
            }
          
            FullMessageReceived += message;

            try
            {
                var timeoutTask = Task.Delay(timeoutMs);
                var completed = await Task.WhenAny(timeoutTask, tcs.Task);

                if (completed == timeoutTask)
                    return "Response TimeOut";
                else
                    return tcs.Task.Result;

            }
            finally
            {
                FullMessageReceived -= message;
            }
            
        }



    }
}
