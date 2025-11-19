using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SurfComm.Connect.Models
{
    [XmlRoot("DeviceList")]
    public class DeviceList
    {
        [XmlElement("Device")]
        public List<string> Devices { get; set; } = new();
    }
}
