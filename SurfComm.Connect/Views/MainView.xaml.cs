using SurfComm.Connect.Controls;
using SurfComm.Connect.Controls.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SurfComm.Connect.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            InspectionInfoControl.DataContext = new InspectionInfoViewModel();
            ParametersControl.DataContext = new ParametersControlViewModel();
            IOStatusControl.DataContext = new IOStatusControlViewModel();
            ResultsControl.DataContext = new ResultsControlViewModel();
        }

    }
}
