using SurfComm.Core;
using SurfComm.Core.Models;
using System.Collections.Generic;

public static class InspectionPlanService
{
    public static List<InspectionPlan> LoadPlans()
    {
        var wrapper = XmlHandler.LoadFromXml<InspectionPlanList>("InspectionPlans.xml");

        if (wrapper?.Plans == null)
            return new List<InspectionPlan>();

        return wrapper.Plans;
    }

    public static void SavePlans(List<InspectionPlan> plans)
    {
        var wrapper = new InspectionPlanList { Plans = plans };
        XmlHandler.SaveToXml(wrapper, "InspectionPlans.xml");
    }
}
