using System.Collections.Generic;
using System.Xml.Serialization;

namespace SurfComm.Core.Models
{
    [XmlRoot("Standards")]
    public class MeasurementStandardDefinition
    {
        [XmlElement("Standard")]
        public List<MeasurementStandard> Standards { get; set; } = new();
    }

    public class MeasurementStandard
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        [XmlAttribute("code")]
        public string Code { get; set; } = "";

        [XmlElement("Profile")]
        public List<MeasurementProfile> Profiles { get; set; } = new();
    }

    public class MeasurementProfile
    {
        [XmlAttribute("type")]
        public string Type { get; set; } = "";

        [XmlElement("Parameter")]
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
