using System.Collections.Generic;

namespace SurfComm.Core.Models
{
    /// <summary>
    /// Represents a single "Measurement Program" or "Recipe."
    /// This is the file that a user would "Save Settings" or "Load Settings" for.
    /// This is the "Program Number" the robot will call.
    /// </summary>
    public class MeasurementProgram
    {
        // --- NEW PROPERTIES ---
        public int ProgramNumber { get; set; }
        public string ProgramName { get; set; } // e.g., "Cylinder Head Gasket Surface"

        // --- Core "Recipe" Settings ---
        // From SMEAC (Sec 57)
        public string CalculationStandard { get; set; } = "ISO1997/2009";
        public string MeasurementType { get; set; } = "Roughness";
        public string FormRemoval { get; set; } = "Straight";

        // From SLENG (Sec 61)
        public int SamplingLengths_N { get; set; } = 16;
        public double UserLength_mm { get; set; } = 12.80;

        // --- Filtering Settings ---
        // From SCUTC (Sec 59)
        public string FilterType { get; set; } = "Gaussian";
        public double Cutoff_lc { get; set; } = 0.80; // The main cutoff
        public double Cutoff_ls { get; set; } = 0.0025; // The short filter (λs)
        public double Cutoff_lf { get; set; } = 2.5; // The long filter (λf)

        // --- Parameter Table ---
        // From SOUTP (Sec 36) and SJUDG (Sec 55)
        public List<ParameterSetting> Parameters { get; set; }

        public MeasurementProgram()
        {
            // Initialize the list when a new program is created
            Parameters = new List<ParameterSetting>();
        }
    }
}