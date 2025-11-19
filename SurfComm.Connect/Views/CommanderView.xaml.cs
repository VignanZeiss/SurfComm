using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SurfComm.Connect.ViewModels;

namespace SurfComm.Connect.Views
{
    /// <summary>
    /// Interaction logic for CommanderView.xaml
    /// </summary>
    public partial class CommanderView : UserControl
    {
        public CommanderView()
        {
            InitializeComponent();
            DataContext = new CommanderViewModel();
        }
    }
}
