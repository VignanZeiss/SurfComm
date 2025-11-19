using SurfComm.Core.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SurfComm.Connect.ViewModels
{
    public class ParameterSelectionViewModel : BaseViewModel
    {
        public ObservableCollection<ProgramParameter> Parameters { get; set; }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        private Window _window;

        public ParameterSelectionViewModel(
            Window window,
            IEnumerable<ProgramParameter> availableParams,
            IEnumerable<ProgramParameter> alreadySelected)
        {
            _window = window;

            // Clone list so editing does not affect the original until OK is clicked
            Parameters = new ObservableCollection<ProgramParameter>(
                availableParams.Select(p => new ProgramParameter
                {
                    Name = p.Name,
                    Id = p.Id,
                    Min = alreadySelected.FirstOrDefault(x => x.Id == p.Id)?.Min,
                    Max = alreadySelected.FirstOrDefault(x => x.Id == p.Id)?.Max,
                    IsSelected = alreadySelected.Any(x => x.Id == p.Id)
                })
            );

            ConfirmCommand = new RelayCommand(_ => Confirm());
            CancelCommand = new RelayCommand(_ => _window.Close());
        }

        private void Confirm()
        {
            _window.DialogResult = true;
            _window.Close();
        }
    }
}
