using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SurfComm.Core.Models
{
    [XmlRoot("InspectionPlans")]
    public class InspectionPlanList
    {
        [XmlElement("InspectionPlan")]
        public List<InspectionPlan> Plans { get; set; } = new();
    }
    


}
