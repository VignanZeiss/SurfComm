using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Core
{
    public class CommandQueue
    {
        public List<String> Commands { get; } = new();

        public void Add(string cmd)
        {
            if (!string.IsNullOrWhiteSpace(cmd))
                Commands.Add(cmd);
        }

        public void Clear()
        {
            Commands.Clear();
        }
    }
}
