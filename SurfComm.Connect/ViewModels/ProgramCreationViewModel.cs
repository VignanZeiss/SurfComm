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

        public ProgramModel Program { get; set; }

        // DROPDOWN LISTS
        public ObservableCollection<string> StandardsList { get; set; } = new();
        public ObservableCollection<string> MeasurementTypeList { get; set; } = new();
        public ObservableCollection<string> FormRemovalList { get; set; } = new();
        public ObservableCollection<string> FilterList { get; set; } = new();
        public ObservableCollection<string> LambdaCList { get; set; } = new();
        public ObservableCollection<string> LambdaSList { get; set; } = new();
        public ObservableCollection<string> LambdaFList { get; set; } = new();

        // FOR PARAMETER POPUP
        public ICommand OpenParameterPopupCommand { get; }
        public ICommand SaveProgramCommand { get; }

        public ProgramCreationViewModel(ProgramModel program)
        {
            Program = program;

            LoadDefinitions();
            PopulateDropdowns();

            OpenParameterPopupCommand = new RelayCommand(_ => OpenParameterPopup());
            SaveProgramCommand = new RelayCommand(_ => SaveProgram());
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

        private void OpenParameterPopup()
        {
            // TODO: Create popup later
            MessageBox.Show("Parameter Popup not yet implemented.");
        }

        private void SaveProgram()
        {
            // Build the parameter display text
            Program.ParameterListDisplay =
                string.Join(", ", Program.Parameters.Select(p => p.Display));

            // Save file
            string filePath = System.IO.Path.Combine(FilePath.ProgramsFolder, $"Program{Program.Id}.xml");

            XmlHandler.SaveToXml<ProgramModel>( Program,filePath);

            // Update global program list after saving
            SurfCommHelper.UpdateGlobalProgramList();
        }
    }
}
