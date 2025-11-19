using SurfComm.Connect.ViewModels;
using SurfComm.Connect.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SurfComm.Connect
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer;
        public ICommand ShowDeviceSetUpView { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            App.Nav = MainContentHost;

            // MainWindow ViewModel
            var vm = new MainWindowViewModel();
            DataContext = vm;

            // Set default view correctly
            vm.CurrentView = new MainView();

            // Timer for status bar time
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (_, _) =>
            {
                vm.CurrentTime = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
            };
            _timer.Start();

            ShowDeviceSetUpView = new RelayCommand(_ => ShowSetUpView());
        }

        private void ShowSetUpView()
        {
            if (DataContext is MainWindowViewModel vm)
                vm.CurrentView = new SetupView();
        }




        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                SystemCommands.MaximizeWindow(this);
            else
                SystemCommands.RestoreWindow(this);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

    }
}
