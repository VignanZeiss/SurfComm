using SurfComm.Core;
using SurfComm.Core.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SurfComm.Connect.ViewModels
{
    public class ProgramCreationViewModel : BaseViewModel
    {
        private const string ProgramDefinitionFile = "ProgramDefinition.xml";
        private const string MeasurementDefinitionFile = "MeasurementStandardDefinition.xml";

        public ProgramDefinition ProgramDef { get; private set; }
        public MeasurementStandardDefinition MeasureDef { get; private set; }

        private string _selectedStandard;
        public string SelectedStandard
        {
            get => _selectedStandard;
            set
            {
                _selectedStandard = value;

                // If Program.Settings has a CalculationStandard property, keep it in sync
                if (Program?.Settings != null)
                {
                    Program.Settings.CalculationStandard = value;
                }

                OnPropertyChanged();
            }
        }

        private string _selectedMeasurementType;
        public string SelectedMeasurementType
        {
            get => _selectedMeasurementType;
            set
            {
                _selectedMeasurementType = value;

                // If Program.Settings has MeasurementType, sync it too
                if (Program?.Settings != null)
                {
                    Program.Settings.MeasurementType = value;
                }

                OnPropertyChanged();
            }
        }

        public ProgramModel Program { get; set; }

        // DROPDOWN LISTS
        public ObservableCollection<string> StandardsList { get; set; } = new();
        public ObservableCollection<string> MeasurementTypeList { get; set; } = new();
        public ObservableCollection<string> FormRemovalList { get; set; } = new();
        public ObservableCollection<string> FilterList { get; set; } = new();
        public ObservableCollection<string> LambdaCList { get; set; } = new();
        public ObservableCollection<string> LambdaSList { get; set; } = new();
        public ObservableCollection<string> LambdaFList { get; set; } = new();

        // INLINE PARAMETER TABLE
        public ObservableCollection<ParameterRow> AvailableParameterRows { get; } = new();

        // COMMANDS
        public ICommand OpenParameterPopupCommand { get; }   // now: "Load parameters into table"
        public ICommand SaveProgramCommand { get; }

        public ProgramCreationViewModel(ProgramModel program)
        {
            Program = program;

            LoadDefinitions();
            PopulateDropdowns();

            // Reuse this command: now it loads parameter rows instead of opening a popup
            OpenParameterPopupCommand = new RelayCommand(_ => LoadAvailableParameters());
            SaveProgramCommand = new RelayCommand(_ => SaveProgram());

            // Optional: if Program already has settings, pre-select dropdowns
            if (!string.IsNullOrWhiteSpace(Program?.Settings?.CalculationStandard))
                SelectedStandard = Program.Settings.CalculationStandard;

            if (!string.IsNullOrWhiteSpace(Program?.Settings?.MeasurementType))
                SelectedMeasurementType = Program.Settings.MeasurementType;
        }

        public ProgramCreationViewModel()
        {
        }

        private void LoadDefinitions()
        {
            ProgramDef = XmlHandler.LoadFromXml<ProgramDefinition>(ProgramDefinitionFile);
            MeasureDef = XmlHandler.LoadFromXml<MeasurementStandardDefinition>(MeasurementDefinitionFile);
        }

        private void PopulateDropdowns()
        {
            foreach (var s in ProgramDef.Settings)
            {
                switch (s.Name)
                {
                    case "CalculationStandard":
                        foreach (var o in s.Options)
                            StandardsList.Add(o.Name);
                        break;

                    case "MeasurementType":
                        foreach (var o in s.Options)
                            MeasurementTypeList.Add(o.Name);
                        break;

                    case "FormRemoval":
                        foreach (var o in s.Options)
                            FormRemovalList.Add(o.Name);
                        break;

                    case "FilterType":
                        foreach (var o in s.Options)
                            FilterList.Add(o.Name);
                        break;
                }
            }

            // Cutoff options
            foreach (var cutoff in ProgramDef.Cutoffs)
            {
                switch (cutoff.Name)
                {
                    case "Cutoff_lc":
                        foreach (var o in cutoff.Options)
                            LambdaCList.Add(o.Name);
                        break;

                    case "Cutoff_ls":
                        foreach (var o in cutoff.Options)
                            LambdaSList.Add(o.Name);
                        break;

                    case "Cutoff_lf":
                        foreach (var o in cutoff.Options)
                            LambdaFList.Add(o.Name);
                        break;
                }
            }
        }

        /// <summary>
        /// Previously: OpenParameterPopup()
        /// Now: load available parameters into an inline table.
        /// </summary>
        private void LoadAvailableParameters()
        {
            if (string.IsNullOrWhiteSpace(SelectedStandard))
            {
                MessageBox.Show("Please select a Calculation Standard first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedMeasurementType))
            {
                MessageBox.Show("Please select a Measurement Type first.");
                return;
            }

            AvailableParameterRows.Clear();

            // Find the selected standard in MeasurementStandardDefinition
            var std = MeasureDef.Standards
                .FirstOrDefault(s => s.Name == SelectedStandard);

            if (std == null)
            {
                MessageBox.Show($"Standard '{SelectedStandard}' not found in MeasurementStandardDefinition.");
                return;
            }

            // Find the profile corresponding to the selected measurement type
            var profile = std.Profiles
                .FirstOrDefault(p => p.Type == SelectedMeasurementType);

            if (profile == null)
            {
                MessageBox.Show($"Measurement Type '{SelectedMeasurementType}' not defined for standard '{SelectedStandard}'.");
                return;
            }

            // profile.Param is List<MeasurementParameter>
            foreach (var p in profile.Param)
            {
                AvailableParameterRows.Add(new ParameterRow
                {
                    Name = p.Name,
                    Id = p.Id,
                    IsSelected = false
                    // Min/Max/Default remain null and can be edited in the grid
                });
            }
        }

        private void SaveProgram()
        {
            // Map selected rows from UI -> Program.Parameters
            Program.Parameters = AvailableParameterRows
                .Where(r => r.IsSelected)
                .Select(r => new ProgramParameter
                {
                    Name = r.Name,
                    Id = r.Id
                    // Extend ProgramParameter later for Min/Max/Default if needed
                })
                .ToList();

            // Build the parameter display text
            Program.ParameterListDisplay =
                string.Join(", ", Program.Parameters.Select(p => p.Display));

            // Save file
            string filePath = System.IO.Path.Combine(FilePath.ProgramsFolder, $"Program{Program.Id}.xml");
            XmlHandler.SaveToXml(Program, filePath);

            // Update global program list after saving
            SurfCommHelper.UpdateGlobalProgramList();
        }
    }
}
