using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Core
{

    public class ComPortInfo
    {
        public string PortName { get; set; } = string.Empty;
        public string FriendlyName { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public bool LookLikeSurfComm { get; set; }
    }
}
   