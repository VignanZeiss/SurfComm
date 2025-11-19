using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SurfComm.Connect.Models
{
    [XmlRoot("ActionCommands")]
    public class ActionsConfig
    {
        [XmlElement("Command")]
        List<Command> Commands { get; set; } = new();
    }
    public class Command
    {
        [XmlAttribute ("name")] public string Name { get; set; } = string.Empty;
        [XmlAttribute("id")] public string ID { get; set; } = string.Empty;
        [XmlAttribute("code")] public string Cmd { get; set; } = string.Empty;
        [XmlAttribute("note")] public string Note { get; set; } = string.Empty;


    }

}
