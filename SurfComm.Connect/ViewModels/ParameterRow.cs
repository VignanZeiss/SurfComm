using SurfComm.Core.Models;

namespace SurfComm.Connect.ViewModels
{
    public class ParameterRow : BaseViewModel
    {
        public bool IsSelected { get; set; }

        public string Name { get; set; } = "";
        public int Id { get; set; }

        // Optional numeric fields for later use
        public double? Min { get; set; }
        public double? Max { get; set; }
        public double? Default { get; set; }

        // Handy for binding if you want a label somewhere
        public string Display => $"{Name} (ID={Id})";
    }
}
