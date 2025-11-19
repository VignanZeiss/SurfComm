using SurfComm.Connect.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SurfComm.Connect.Controls.ViewModels
{
    public class ResultItem
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double Nominal { get; set; }
        public double LowerTol { get; set; }
        public double UpperTol { get; set; }

        public string StatusText =>
            Value > Nominal + UpperTol ? "FAIL" :
            Value > Nominal + (UpperTol * 0.5) ? "WARN" :
            "PASS";

        public Brush StatusColor =>
            StatusText == "FAIL" ? Brushes.Red :
            StatusText == "WARN" ? Brushes.Goldenrod :
            Brushes.Green;
    }

    public class ResultsControlViewModel : BaseViewModel
    {
        public ObservableCollection<ResultItem> Results { get; } =
            new ObservableCollection<ResultItem>();

        public ResultsControlViewModel()
        {
            // Demo data until backend is connected
            Results.Add(new ResultItem { Name = "Ra", Value = 0.45, Nominal = 0.35, LowerTol = -0.05, UpperTol = 0.05 });
            Results.Add(new ResultItem { Name = "Rz", Value = 5.2, Nominal = 4.0, LowerTol = -0.4, UpperTol = 0.4 });
            Results.Add(new ResultItem { Name = "Rmax", Value = 7.8, Nominal = 6.0, LowerTol = -0.5, UpperTol = 0.5 });
        }
    }
}
