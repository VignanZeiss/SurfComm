namespace SurfComm.Core.Models
{
    /// <summary>
    /// Represents a single row in the parameter settings table.
    /// This is a sub-model used by MeasurementProgram.
    /// </summary>
    public class ParameterSetting
    {
        public string Name { get; set; } // e.g., "Ra", "Rz"
        public string Units { get; set; } // e.g., "µm", "%"
        public double? LowerLimit { get; set; } // Nullable double for no limit
        public double? UpperLimit { get; set; } // Nullable double for no limit
        public string AdvancedSettings { get; set; } // For "Rmr0: 5.0%, Depth: 1.0 µm"
    }
}