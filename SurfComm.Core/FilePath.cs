using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Core
{
    public static class FilePath
    {
        public static string DocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        public static string ConfigFile = "DeviceConfig.xml";
        public static string DeviceListFile = "DeviceList.xml";
        public static string DeviceDefinitionFile = "DeviceDefinition.xml";
        public static string SerialPortConfigFile = "SerialPort.xml";
        public static string ProgramsFolder = "ProgramsList";

    }
}
