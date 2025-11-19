using SurfComm.Connect.ViewModels;
using System.Windows.Controls;


namespace SurfComm.Connect.View
{
    /// <summary>
    /// Interaction logic for ProgramMappingView.xaml
    /// </summary>
    public partial class ProgramMappingView : UserControl
    {
        public ProgramMappingView()
        {
            InitializeComponent();
            DataContext = new ProgramMappingViewModel();
        }
    }
}
