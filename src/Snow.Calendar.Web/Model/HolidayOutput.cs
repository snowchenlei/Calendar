using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snow.Calendar.Web.Model
{
    /// <summary>
    /// 假日信息(年)
    /// </summary>
    public class HolidayYears
    {
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 假日信息(月)
        /// </summary>
        public IEnumerable<HolidayMonths> HolidayMonths { get; set; }
    }

    /// <summary>
    /// 假日信息(月)
    /// </summary>
    public class HolidayMonths
    {
        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 假日信息(日)
        /// </summary>
        public IEnumerable<HolidayDays> HolidayDays { get; set; }
    }

    /// <summary>
    /// 假日信息(日)
    /// </summary>
    public class HolidayDays
    {
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 日类型
        /// </summary>
        public DayType DayType { get; set; }
    }

    /// <summary>
    /// 万年历年
    /// </summary>
    public class CalendarYear
    {
        /// <summary>
        /// 阳历年
        /// </summary>
        public int CurrentYear { get; set; }

        /// <summary>
        /// 当年总天数
        /// </summary>
        public int DaysOfYear { get; set; }

        /// <summary>
        /// 阴历年份
        /// </summary>
        public int LunarYear { get; set; }

        /// <summary>
        /// 阴历年中文
        /// </summary>
        public string LunarYearText { get; set; }

        /// <summary>
        /// 是否闰年
        /// </summary>
        public bool IsLeapLunarYear { get; set; }

        /// <summary>
        /// 闰几月(0则不闰)
        /// </summary>
        public int LunarYearLeapMonth { get; set; }

        /// <summary>
        /// 干支年
        /// </summary>
        public string LunarYearSexagenary { get; set; }

        /// <summary>
        /// 年纳音五行
        /// </summary>
        public string LunarYearNaYinFiveElements { get; set; }

        /// <summary>

        /// 阴历年生肖
        /// </summary>
        public string LunarYearAnimal { get; set; }

        /// <summary>
        /// 阳历诞生石
        /// </summary>
        public string SolarBirthStone { get; set; }
    }

    /// <summary>
    /// 万年历月
    /// </summary>
    public class CalendarMonth
    {
        public int CurrentMonth { get; set; }

        public int LunarMonth { get; set; }

        public string LunarMonthText { get; set; }

        /// <summary>
        /// 是否闰月
        /// </summary>
        public bool IsLeapLunarMonth { get; set; }

        /// <summary>
        /// 干支月
        /// </summary>
        public string LunarMonthSexagenary { get; set; }

        /// <summary>
        /// 月纳音五行
        /// </summary>
        public string LunarMonthNaYinFiveElements { get; set; }

        /// <summary>
        /// 是否农历月大
        /// </summary>
        public bool IsBiglunarMonth { get; set; }

        /// <summary>
        /// 是否月大
        /// </summary>
        public bool IsBigMonth { get; set; }
    }

    public class CalendarDay
    {
        public int CurrentDay { get; set; }

        /// <summary>
        /// 阴历月中日期
        /// </summary>
        public int LunarDay { get; set; }

        /// <summary>
        /// 阴历月中日期字符串
        /// </summary>
        public string LunarDayText { get; set; }

        /// <summary>
        /// 一年的第几天
        /// </summary>
        public int DayOfYear { get; set; }

        /// <summary>
        /// 周几
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// 字符串
        /// </summary>
        public string DayOfWeekText { get; set; }

        /// <summary>
        /// 干支日
        /// </summary>
        public string LunarDaySexagenary { get; set; }

        /// <summary>
        /// 日纳音五行
        /// </summary>
        public string LunarDayNaYinFiveElements { get; set; }

        ///// <summary>
        ///// 农历节日
        ///// </summary>
        //public string LunarHoliday { get; set; }

        ///// <summary>
        ///// 公历节日
        ///// </summary>
        //public string SolarHoliday { get; set; }

        ///// <summary>
        ///// 按某月第几周第几日计算的节日
        ///// </summary>
        //public string WeekDayHoliday { get; set; }

        /// <summary>
        /// 节日
        /// </summary>
        public List<string> Holiday { get; set; }

        /// <summary>
        /// 中国农历节日
        /// </summary>
        public KeyValuePair<string, string> LunarHoliday { get; set; }

        /// <summary>
        /// 按公历日计算的节日
        /// </summary>
        public KeyValuePair<string, string> SolarHoliday { get; set; }

        /// <summary>
        /// 按某月第几周第几日计算的节日
        /// </summary>
        public string WeekDayHoliday { get; set; }

        /// <summary>
        /// 农历年中第几周
        /// </summary>
        public int LunarWeekOfYear { get; set; }

        /// <summary>
        /// 公历年中第几周
        /// </summary>
        public int WeekOfYear { get; set; }

        /// <summary>
        /// 24节气
        /// </summary>
        public string SolarTerm { get; set; }

        /// <summary>
        /// 24节气描述
        /// </summary>
        public string SolarTermDays { get; set; }

        /// <summary>
        /// 星座
        /// </summary>
        public string SolarConstellation { get; set; }

        /// <summary>
        /// 28星宿计算
        /// </summary>
        public string LunarConstellation { get; set; }

        /// <summary>
        /// 星宫
        /// </summary>
        public string SolarPalace { get; set; }

        /// <summary>
        /// 行星
        /// </summary>
        public string SolarPlanet { get; set; }

        public DayType DayType { get; set; }
    }

    /// <summary>
    /// 万年历日期描述
    /// </summary>
    public class CalendarDate
    {
        /// <summary>
        /// 阳历日期
        /// </summary>
        public string CurrentDate { get; set; }

        /// <summary>
        /// 农历日期
        /// </summary>
        public string LunarText { get; set; }

        /// <summary>
        /// 干支日
        /// </summary>
        public string LunarDateSexagenary { get; set; }

        public CalendarYear CalendarYear { get; set; }
        public CalendarMonth CalendarMonth { get; set; }
        public CalendarDay CalendarDay { get; set; }
    }
}