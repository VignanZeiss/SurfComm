using SurfComm.Connect.Views;
using SurfComm.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SurfComm.Connect.ViewModels
{
    public class ParameterEditorViewModel : BaseViewModel
    {
        public ObservableCollection<MeasurementParameter> AvailableParameters { get; set; } =
            new ObservableCollection<MeasurementParameter>();

        public MeasurementParameter SelectedParameter { get; set; }

        private readonly ParameterPopupContext _ctx;

        public ProgramParameter Result { get; private set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ParameterEditorViewModel(ParameterPopupContext ctx)
        {
            _ctx = ctx;

            LoadParameters();

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void LoadParameters()
        {
            var std = _ctx.MeasureDef.Standards
                .FirstOrDefault(s => s.Name == _ctx.Standard);

            if (std == null) return;

            var profile = std.Profiles
                .FirstOrDefault(p => p.Type == _ctx.MeasurementType);

            if (profile == null) return;

            foreach (var p in profile.Param)
                AvailableParameters.Add(p);
        }

        private void Save()
        {
            if (SelectedParameter == null)
            {
                MessageBox.Show("Please select a parameter.");
                return;
            }

            // prevent duplicates
            if (_ctx.ExistingParameters.Any(p => p.Name == SelectedParameter.Name))
            {
                MessageBox.Show("Parameter already exists.");
                return;
            }

            Result = new ProgramParameter
            {
                Name = SelectedParameter.Name,
                Id = SelectedParameter.Id
                // Display is computed automatically — do NOT set it.
            };


            CloseDialog(true);
        }

        private void Cancel() => CloseDialog(false);

        private void CloseDialog(bool result)
        {
            var win = Application.Current.Windows
                        .OfType<ParameterPopupWindow>()
                        .FirstOrDefault();

            if (win != null)
            {
                win.DialogResult = result;
            }

        }
    }

}
