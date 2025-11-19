//using SurfComm.Connect.Navigation;
using SurfComm.Connect.View;
using SurfComm.Connect.ViewModels;
using SurfComm.Connect.Views;
using SurfComm.Core.Models;
using SurfComm.Service;
using System.Windows.Input;

namespace SurfComm.Connect.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _connectionStatus = "Disconnected";
        private readonly System.Timers.Timer _uiTimer;
        private string _currentTime = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }
        public string CurrentTime

        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }


        public string ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenCommanderCommand { get; }
        public ICommand OpenProgramMappingCommand { get; }
        public ICommand OpenSetupCommand { get; }
        public ICommand OpenOutputConnectionCommand { get; }

        public MainWindowViewModel()
        {
            _uiTimer = new System.Timers.Timer(300);
            _uiTimer.Elapsed += (_, _) => UpdateStatus();
            _uiTimer.Start();

            OpenCommanderCommand = new RelayCommand(_ =>
                Navigator.Navigate(new CommanderView(), new CommanderViewModel()));

            OpenProgramMappingCommand = new RelayCommand(_ =>
                Navigator.Navigate(new ProgramMappingView(), new ProgramMappingViewModel()));

            OpenSetupCommand = new RelayCommand(_ =>
                Navigator.Navigate(new SetupView(), new SetupViewModel()));

            //OpenOutputConnectionCommand = new RelayCommand(_ =>
            //  Navigator.Navigate(new OutputConnectionView(), new OutputConnectionViewModel()));

            //Time Dispaly 
            var clock = new System.Timers.Timer(1000);
            clock.Elapsed += (_, _) =>
            {
                CurrentTime = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
            };
            clock.Start();
        }
        private void UpdateStatus()
        {
            if (SystemState.SurfComm == null)
            {
                ConnectionStatus = "Disconnected";
                return;
            }

            if (SystemState.SurfComm.IsErrored)
                ConnectionStatus = "⚠️ Error";
            else if (SystemState.SurfComm.IsBusy)
                ConnectionStatus = "🟡 Busy";
            else if (SystemState.SurfComm.IsConnected)
                ConnectionStatus = "🟢 Connected";
            else
                ConnectionStatus = "🔴 Disconnected";
        }
    }
}
