using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;
using Snow.Calendar.Web.Interceptor;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public interface IDateHelper
    {
        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        DateTime[] GetDates(string str);

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        DateTime GetDate(string str);

        /// <summary>
        /// 获取月的日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>某年某月的所有日期</returns>
        List<DateTime> GetDatesByMonth(int year, int month);

        /// <summary>
        /// 获取年日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>某年的所有日期</returns>
        List<DateTime> GetDaysByYear(int year);

        /// <summary>
        /// 获取节日
        /// </summary>
        /// <param name="calendarDate">万年历日期</param>
        /// <returns></returns>
        List<string> GetHoliday(CalendarDate calendarDate);

        /// <summary>
        /// 公历日期转万年历日期
        /// </summary>
        /// <param name="dts">公历日期</param>
        /// <returns>万年历日期</returns>
        List<CalendarDate> GetCalendarDates(IEnumerable<DateTime> dts);

        /// <summary>
        /// 公历日期转万年历日期
        /// </summary>
        /// <param name="dt">公历日期</param>
        /// <returns>万年历日期</returns>
        CalendarDate GetCalendarDate(DateTime dt);
    }

    /// <summary>
    /// 日期帮助类
    /// </summary>
    public class DateHelper : IDateHelper
    {
        private readonly Resource _resource;
        private readonly SolarTerm _solarTerm;
        private readonly Constellation _constellation;
        private readonly ChineseCalendarInfo _chineseCalendar;

        public DateHelper(ChineseCalendarInfo chineseCalendar,
            SolarTerm solarTerm,
            Constellation constellation,
            Resource resource)
        {
            _chineseCalendar = chineseCalendar;
            _solarTerm = solarTerm;
            _constellation = constellation;
            _resource = resource;
        }

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DateTime[] GetDates(string str)
        {
            DateTime[] dates = null;
            try
            {
                string[] strMonths = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                dates = Array.ConvertAll(strMonths, Convert.ToDateTime);
            }
            catch (FormatException ex)
            {
                throw new UserFriendlyException("非法的日期类型");
            }

            return dates;
        }

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DateTime GetDate(string str)
        {
            DateTime date;
            try

            {
                date = DateTime.Parse(str);
            }
            catch (FormatException ex)
            {
                throw new UserFriendlyException("非法的日期类型");
            }

            return date;
        }

        /// <summary>
        /// 获取年日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>某年的所有日期</returns>
        public List<DateTime> GetDaysByYear(int year)
        {
            List<DateTime> days = new List<DateTime>();
            for (int i = 1; i <= 12; i++)
            {
                days.AddRange(GetDatesByMonth(year, i));
            }

            return days;
        }

        /// <summary>
        /// 获取月的日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>某年某月的所有日期</returns>
        public List<DateTime> GetDatesByMonth(int year, int month)
        {
            List<DateTime> days = new List<DateTime>();
            for (int i = 1, max = DateTime.DaysInMonth(year, month); i <= max; i++)
            {
                days.Add(new DateTime(year, month, i));
            }

            return days;
        }

        /// <summary>
        /// 公历日期转万年历日期
        /// </summary>
        /// <param name="dts">公历日期</param>
        /// <returns>万年历日期</returns>
        public List<CalendarDate> GetCalendarDates(IEnumerable<DateTime> dts)
        {
            List<CalendarDate> days = new List<CalendarDate>();
            foreach (DateTime dt in dts)
            {
                days.Add(GetCalendarDate(dt));
            }
            return days;
        }

        /// <summary>
        /// 公历日期转万年历日期
        /// </summary>
        /// <param name="dt">公历日期</param>
        /// <returns>万年历日期</returns>
        public CalendarDate GetCalendarDate(DateTime dt)
        {
            _chineseCalendar.SolarDate = dt;
            CalendarDate calendarDate = new CalendarDate()
            {
                CurrentDate = dt.ToString("D"),
                LunarText = _chineseCalendar.LunarText,
                CalendarYear = new CalendarYear()
                {
                    CurrentYear = dt.Year,
                    LunarYear = _chineseCalendar.LunarYear,
                    LunarYearText = _chineseCalendar.LunarYearText,
                    LunarYearLeapMonth = _chineseCalendar.LunarYearLeapMonth,
                    IsLeapLunarYear = _chineseCalendar.IsLeapLunarYear,
                    LunarYearAnimal = _chineseCalendar.LunarYearAnimal,
                    SolarBirthStone = _chineseCalendar.SolarBirthStone,
                    LunarYearSexagenary = _chineseCalendar.LunarYearSexagenary,
                    LunarYearNaYinFiveElements = _chineseCalendar.LunarYearNaYinFiveElements,
                },
                CalendarMonth = new CalendarMonth()
                {
                    CurrentMonth = dt.Month,
                    LunarMonth = _chineseCalendar.LunarMonth,
                    LunarMonthText = _chineseCalendar.LunarMonthText,
                    LunarMonthSexagenary = _chineseCalendar.LunarMonthSexagenary,
                    IsLeapLunarMonth = _chineseCalendar.IsLeapLunarMonth,
                    IsBiglunarMonth = _chineseCalendar.IsBiglunarMonth,
                    IsBigMonth = new int[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(dt.Month) ? true : false,
                    LunarMonthNaYinFiveElements = _chineseCalendar.LunarMonthNaYinFiveElements,
                },
                CalendarDay = new CalendarDay()
                {
                    CurrentDay = dt.Day,
                    LunarDay = _chineseCalendar.LunarDay,
                    LunarDayText = _chineseCalendar.LunarDayText,
                    DayType = GetDayType(dt),
                    //LunarHoliday = _chineseCalendar.LunarHoliday,
                    //SolarHoliday = _chineseCalendar.SolarHoliday,
                    //WeekDayHoliday = _chineseCalendar.WeekDayHoliday,
                    //Holiday = String.Join('/', _chineseCalendar.LunarHoliday, _chineseCalendar.SolarHoliday,
                    //    _chineseCalendar.WeekDayHoliday).Trim('/'),
                    LunarHoliday = _chineseCalendar.LunarHoliday,
                    SolarHoliday = _chineseCalendar.SolarHoliday,
                    SolarTerm = _solarTerm.GetSolarTerm(dt),// _chineseCalendar.SolarTerm,
                    SolarTermDays = _solarTerm.GetSolarTermDays(dt),// _chineseCalendar.SolarTermDays,
                    LunarConstellation = _constellation.GetConstellation(_chineseCalendar.LunarMonth - 1, _chineseCalendar.LunarDay - 1).ToString(),//_chineseCalendar.LunarConstellation,
                    SolarConstellation = _chineseCalendar.SolarConstellation,
                    SolarPalace = _chineseCalendar.SolarPalace,
                    SolarPlanet = _chineseCalendar.SolarPlanet,
                    DayOfWeek = _chineseCalendar.DayOfWeek,
                    DayOfWeekText = dt.ToString("ddd"),
                    DayOfYear = _chineseCalendar.DayOfYear,
                    WeekOfYear = _chineseCalendar.WeekOfYear,
                    LunarWeekOfYear = _chineseCalendar.LunarWeekOfYear,
                    LunarDaySexagenary = _chineseCalendar.LunarDaySexagenary,
                    LunarDayNaYinFiveElements = _chineseCalendar.LunarDayNaYinFiveElements,
                }
            };

            calendarDate.LunarDateSexagenary =
                                $"{calendarDate.CalendarYear.LunarYearSexagenary}({calendarDate.CalendarYear.LunarYearAnimal})年{calendarDate.CalendarMonth.LunarMonthSexagenary}月{calendarDate.CalendarDay.LunarDaySexagenary}日";
            return calendarDate;
        }

        /// <summary>
        /// 获取节日
        /// </summary>
        /// <param name="calendarDate">万年历日期</param>
        /// <returns></returns>
        public List<string> GetHoliday(CalendarDate calendarDate)
        {
            List<string> holidays = new List<string>();
            if (calendarDate.CalendarDay.LunarHoliday.Value != null)
            {
                holidays.AddRange(calendarDate.CalendarDay.LunarHoliday.Value?.Split(' '));
            }

            if (calendarDate.CalendarDay.SolarHoliday.Value != null)
            {
                holidays.AddRange(calendarDate.CalendarDay.SolarHoliday.Value?.Split(' '));
            }

            if (holidays.Any())
            {
                holidays = holidays.Where(s => !String.IsNullOrEmpty(s)).ToList();
            }

            return holidays;
        }

        /// <summary>
        /// 获取日期类型
        /// </summary>
        /// <param name="day">某个日期</param>
        /// <returns>日期类型</returns>
        private DayType GetDayType(DateTime day)
        {
            DayOfWeek dayOfWeek = day.DayOfWeek;
            if (IsHoliday(day))
            {
                return DayType.Holiday;
            }
            if (IsCompensationWork(day))
            {
                return DayType.CompensationWork;
            }
            if (_resource.RestDay.Contains(dayOfWeek))
            {
                return DayType.Weekend;
            }
            return DayType.Workday;
        }

        /// <summary>
        /// 是否是法定休息日
        /// </summary>
        /// <param name="day">某个日期</param>
        /// <returns></returns>
        private bool IsHoliday(DateTime day)
        {
            int year = day.Year;
            if (_resource.Holidays.ContainsKey(year))
            {
                Dictionary<int, int[]> months = _resource.Holidays[year];
                int month = day.Month;
                if (months.ContainsKey(month))
                {
                    if (months[month].Contains(day.Day))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 是否是补班
        /// </summary>
        /// <param name="day">某个日期</param>
        /// <returns></returns>
        private bool IsCompensationWork(DateTime day)
        {
            int year = day.Year;
            if (_resource.Workdays.ContainsKey(year))
            {
                Dictionary<int, int[]> months = _resource.Workdays[year];
                int month = day.Month;
                if (months.ContainsKey(month))
                {
                    if (months[month].Contains(day.Day))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}