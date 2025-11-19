using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SurfComm.Connect.Models
{
    [XmlRoot("DeviceConfig")]
    public class DeviceConfig
    {
        [XmlElement("Device Name")]
        public string DeviceName { get; set; } = string.Empty;
        [XmlElement("StrartTrigger")]
        public string StartTrigger { get; set; } = string.Empty;
        [XmlArray("SelectedSettings")]
        [XmlArrayItem("Settings")]
            public List<SelectedSetting> SelectedSettings { get; set; } = new();
        
    }
    public class SelectedSetting
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        [XmlAttribute("command")]
        public string Command { get; set; } = string.Empty;
        [XmlAttribute("cmdName")]
        public string CmdName { get; set; } = string.Empty;
        [XmlAttribute("value")]
        public string Value { get; set; } = string.Empty;

    }
}
