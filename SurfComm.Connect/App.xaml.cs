using SurfComm.Core;
using SurfComm.Service;
using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace SurfComm.Connect
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static CommunicationManager? CommService;
        public static ContentControl? Nav;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize and start the background communication service
            CommService = new CommunicationManager();
            CommService.Start();

            // Optional: Log to verify
            Logger.Log("App started → CommunicationManager initialized.");

            //CommService.Enqueue("SMEAS");
            SurfCommHelper.UpdateGlobalProgramList();
            

        }
        

        protected override void OnExit(ExitEventArgs e)
        {
            // Stop and clean up the service
            CommService?.Stop();
            CommService?.Dispose();

            Logger.Log("App exiting → CommunicationManager stopped.");
            
            base.OnExit(e);
        }
    }

}
