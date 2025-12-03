using System.Collections.Generic;
using SurfComm.Core.Models;

namespace SurfComm.Connect.ViewModels
{
    public class ParameterPopupContext
    {
        public string Standard { get; set; }
        public string MeasurementType { get; set; }

        public MeasurementStandardDefinition MeasureDef { get; set; }

        public List<ProgramParameter> ExistingParameters { get; set; }
    }
}
