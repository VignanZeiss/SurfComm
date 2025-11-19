using SurfComm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SurfComm.Connect.ViewModels
{
    public class CommanderViewModel:BaseViewModel
    {
        private readonly System.Timers.Timer _uiTimer;

        
        //----------Public  Properties___________
        public string Response
        {
            get => SystemState.SurfComm.Response;
            set { /* DO NOTHING but prevents exception */ }
        }
        public string Command { get; set; }

        //-------- ICommands -----------------
        public ICommand Execute { get; set; }

        //--------Constructor-------------
        public  CommanderViewModel()
        {
            _uiTimer = new System.Timers.Timer(200); // refresh UI every 200ms
            _uiTimer.Elapsed += (_, _) => OnPropertyChanged(nameof(Response));
            _uiTimer.Start();
            Execute = new RelayCommand(_ => CommandToQueue());
        }

        public void CommandToQueue()
        {
            if (!string.IsNullOrEmpty(Command))
                App.CommService.Enqueue(Command);
        }
    }
}
