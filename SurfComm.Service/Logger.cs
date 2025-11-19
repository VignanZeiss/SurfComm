using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Service
{
    public static class Logger
    {
        private static readonly List<string> _entries = new();

        public static void Log(string message)
        {
            string line = $"{DateTime.Now:HH:mm:ss} - {message}";
            _entries.Add(line);
            System.Diagnostics.Debug.WriteLine(line); // For now, print to console output
        }

        public static List<string> GetAll()
        {
            return new List<string>(_entries);
        }
    }
}
