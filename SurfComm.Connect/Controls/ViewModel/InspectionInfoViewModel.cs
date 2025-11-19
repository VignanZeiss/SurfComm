using SurfComm.Core.Models;
using SurfComm.Service;
using System.Collections.Generic;
using System.Windows;

namespace SurfComm.Connect.Controls.ViewModels
{
    public class InspectionInfoViewModel : BaseViewModel
    {
        private List<InspectionPlan> _inspectionPlans;
        public List<InspectionPlan> InspectionPlans
        {
            get => _inspectionPlans;
            set { _inspectionPlans = value; OnPropertyChanged(); }
        }

        private InspectionPlan _selectedPlan;
        public InspectionPlan SelectedPlan
        {
            get => _selectedPlan;
            set { _selectedPlan = value; OnPropertyChanged(); }
        }

        public InspectionInfoViewModel()
        {
            try
            {
                // Load XML
                InspectionPlans = InspectionPlanService.LoadPlans();

                // Auto-select first item if available
                if (InspectionPlans != null && InspectionPlans.Count > 0)
                    SelectedPlan = InspectionPlans[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("XML Load Error: " + ex.Message);
                InspectionPlans = new List<InspectionPlan>();
            }
        }
    }
}
