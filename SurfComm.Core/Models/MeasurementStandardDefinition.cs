using System.Collections.Generic;
using System.Xml.Serialization;

namespace SurfComm.Core.Models
{
    // FIX 1: Match the XML Root element "ParameterMappings"
    [XmlRoot("ParameterMappings")]
    public class MeasurementStandardDefinition
    {
        [XmlElement("Standard")]
        public List<MeasurementStandard> Standards { get; set; } = new();
    }

    public class MeasurementStandard
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        // Optional: The XML doesn't have a "code" attribute, but keeping this is fine (it will just be empty)
        [XmlAttribute("code")]
        public string Code { get; set; } = "";

        [XmlElement("Profile")]
        public List<MeasurementProfile> Profiles { get; set; } = new();
    }

    public class MeasurementProfile
    {
        [XmlAttribute("type")]
        public string Type { get; set; } = "";

        // FIX 2: Match the XML child element "Param" instead of "Parameter"
        [XmlElement("Param")]
        public List<MeasurementParameter> Parameters { get; set; } = new();
    }

    public class MeasurementParameter
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}