using System.Xml.Serialization;

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
    public string Unit { get; set; } = "µm";

    [XmlIgnore]
    public bool IsSelected { get; set; }

    [XmlIgnore]
    public string Display =>
        $"{Name} ({Min?.ToString() ?? "-"}–{Max?.ToString() ?? "-"})";
}
