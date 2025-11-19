using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfComm.Core.Models
{
    public static class Global
    {
        public static ObservableCollection<ProgramModel> ProgramsList { get; set; } = new();

    }
}
