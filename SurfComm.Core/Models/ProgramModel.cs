using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SurfComm.Core.Models
{
    [XmlRoot("Program")]
    public class ProgramModel
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        public ProgramInfo Info { get; set; } = new();
        public ProgramSettings Settings { get; set; } = new();

        [XmlArray("Parameters")]
        [XmlArrayItem("Parameter")]
        public List<ProgramParameter> Parameters { get; set; } = new();

        [XmlArray("CommandList")]
        [XmlArrayItem("Command")]
        public List<string> CommandList { get; set; } = new();

        [XmlIgnore]
        public string ParameterListDisplay { get; set; }
    }


    public class ProgramInfo
    {
        public string Name { get; set; } = "New Program";
        public string Description { get; set; } = "";
        public string Created { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }


    public class ProgramSettings
    {
        // SMEAC-related
        public string CalculationStandard { get; set; } = "";
        public string MeasurementType { get; set; } = "";
        public string FormRemoval { get; set; } = "";
        public string AutoCutoff { get; set; } = "";

        // SCUTC-related
        public string FilterType { get; set; } = "";
        public string Cutoff_lc { get; set; } = "";
        public string Cutoff_ls { get; set; } = "";
        public string Cutoff_lf { get; set; } = "";

        // SLENG / STRVL
        public string EvaluationLength { get; set; } = "";
        public string TravelLength { get; set; } = "";
    }


    public class ProgramParameter
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("Min")]
        public double? Min { get; set; }

        [XmlElement("Max")]
        public double? Max { get; set; }

        [XmlElement("Unit")]
        public string Unit { get; set; } = "µm"; // default

        // UI-only (not serialized)
        [XmlIgnore]
        public string Display =>
            $"{Name} ({Min?.ToString() ?? "-"}–{Max?.ToString() ?? "-"})";
    }
}
