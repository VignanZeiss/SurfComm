using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SurfComm.Connect.Models
{
    [XmlRoot("SerialPort")]
    public class SerialPortClass
    {
        [XmlElement("Name")]
        public string? Name { get; set; }
        [XmlElement("BaudRate")]
        public int BaudRate { get; set; } = 115200;
        [XmlElement("Parity")]
        public string Parity { get; set; } = "None";
        [XmlElement("StopBits")]
        public string StopBits { get; set; } = "One";
        [XmlElement("Handshake")]
        public string Handshake { get; set; } = "None";
        [XmlElement("Description")]
        public string Description { get; set; } = string.Empty;
    }
}
