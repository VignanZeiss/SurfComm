using System.Collections.Generic;
using System.Xml.Serialization;

namespace SurfComm.Core.Models
{
    [XmlRoot("ProgramParameters")]
    public class ProgramDefinition
    {
        [XmlElement("Setting")]
        public List<ProgramSetting> Settings { get; set; } = new();

        [XmlElement("Cutoff")]
        public List<ProgramCutoff> Cutoffs { get; set; } = new();
        
        
    }

    public class ProgramSetting
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        [XmlAttribute("command")]
        public string Command { get; set; } = "";

        [XmlElement("Option")]
        public List<ProgramOption> Options { get; set; } = new();
    }

    public class ProgramCutoff
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        [XmlAttribute("command")]
        public string Command { get; set; } = "";

        [XmlElement("Option")]
        public List<ProgramOption> Options { get; set; } = new();
    }

    public class ProgramOption
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        [XmlAttribute("value")]
        public string Value { get; set; } = "";

        [XmlAttribute("n")]
        public int NValue { get; set; }
    }
}
