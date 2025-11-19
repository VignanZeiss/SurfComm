using SurfComm.Connect.ViewModels;

namespace SurfComm.Connect.Controls.ViewModels
{
    public class ParametersControlViewModel : BaseViewModel
    {
        private string _parameter1;
        public string Parameter1
        {
            get => _parameter1;
            set { _parameter1 = value; OnPropertyChanged(); }
        }

        private string _parameter2;
        public string Parameter2
        {
            get => _parameter2;
            set { _parameter2 = value; OnPropertyChanged(); }
        }

        private string _parameter3;
        public string Parameter3
        {
            get => _parameter3;
            set { _parameter3 = value; OnPropertyChanged(); }
        }

        private string _parameter4;
        public string Parameter4
        {
            get => _parameter4;
            set { _parameter4 = value; OnPropertyChanged(); }
        }

        private string _parameter5;
        public string Parameter5
        {
            get => _parameter5;
            set { _parameter5 = value; OnPropertyChanged(); }
        }
    }
}
