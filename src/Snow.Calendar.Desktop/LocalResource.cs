using Snow.Calendar.Common;
using Snow.Calendar.Common.Model;

namespace Snow.Calendar.Desktop;

public class LocalResource : Resource
{
    public override Dictionary<int, Dictionary<int, int[]>> Holidays => [];
    public override Dictionary<int, Dictionary<int, int[]>> Workdays => [];
    public override SolarHoliday[] SolarHoliday => [];
    public override LunarHoliday[] LunarHoliday => [];
    public override WeekHoliday[] WeekHoliday => [];
    public override SolarModel[] SolarTerms => [];
}