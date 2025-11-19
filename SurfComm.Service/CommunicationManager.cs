using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Service
{
    public class CommunicationManager : IDisposable
    {
        private Thread? _workerThread; // background worker
        private bool _running;         // flag to keep the loop alive
        private readonly SerialPortServiceRun _serialServiceHandle = new();


        private void RunLoop()
        {
            while (_running)
            {
                _serialServiceHandle.Run();
                Thread.Sleep(1000);
            }
        }

        //Forwars Enque Request to SerialServiceRun
        public void Enqueue(string cmd)
        {
            _serialServiceHandle.Enqueue(cmd);
            //Serial Service Run then Add it to end of the Queue
        }




        public void Start()
        {
            if (_running)
                return;
            _running = true;
            SystemState.ServiceRunning = true;
            SystemState.StatusMessage = "Running";
            _workerThread = new Thread(() => RunLoop())
            {
                IsBackground = true
            };
            _workerThread.Start();
        }



        public void Stop()
        {
            _running = false;
            SystemState.ServiceRunning = false;
            SystemState.StatusMessage = "Stopped";

            Logger.Log(" CommunicationManager stopped.");

            
        }

        
        public void Dispose()
        {
            Stop();
            _workerThread = null;
        }

    }
}
