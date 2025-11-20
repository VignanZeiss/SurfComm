using SurfComm.Connect.Views;
using SurfComm.Core;
using SurfComm.Core.Models;
using System.Collections.ObjectModel;

using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SurfComm.Connect.ViewModels
{
    public class ProgramMappingViewModel : BaseViewModel
    {
        public ObservableCollection<ProgramModel> Programs { get; set; } = new();

        public ICommand EditProgramCommand { get; }
        public ICommand CreateNewProgramCommand { get; }

        public ProgramMappingViewModel()
        {
            LoadPrograms();
            EditProgramCommand = new RelayCommand(EditProgram);
            CreateNewProgramCommand = new RelayCommand(_ => CreateNewProgram());
        }

        private void LoadPrograms()
        {
            Programs.Clear();
            try
            {
                SurfCommHelper.UpdateGlobalProgramList();
                Programs = Global.ProgramsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Load Program Files \n" + ex);
            }
            
        }

        private void EditProgram(object p)
        {
            ProgramModel prog = p as ProgramModel;
            if (prog == null) return;

            // TODO: navigate to ProgramCreation page
            Navigator.Navigate(new ProgramCreationView(), new ProgramCreationViewModel(prog));


        }

        private void CreateNewProgram()
        {
            int newId = Programs.Count + 1;
            var newProgram = new ProgramModel { Id = newId };

            Navigator.Navigate(new ProgramCreationView(), new ProgramCreationViewModel(newProgram));
        }
    }
}
