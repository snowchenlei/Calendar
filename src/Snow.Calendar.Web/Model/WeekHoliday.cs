using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snow.Calendar.Web.Model
{
    /// <summary>
    /// 按某月第几个星期几
    /// </summary>
    public class WeekHoliday
    {
        public int Month { get; set; }
        public int WeekAtMonth { get; set; }
        public int WeekDay { get; set; }
        public string HolidayName { get; set; }
    }
}