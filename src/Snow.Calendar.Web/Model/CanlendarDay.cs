using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snow.Calendar.Web.Model
{
    /// <summary>
    /// 万年历
    /// </summary>
    public class CanlendarDayInfo
    {
        public int CurrentDay { get; set; }
        public string LunarDayText { get; set; }
        public DayType DayType { get; set; }
    }
}