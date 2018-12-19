using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public class DateHelper
    {
        private readonly ChineseCalendarInfo _chineseCalendar;
        private readonly SolarTerm _solarTerm;
        private readonly Constellation _constellation;
        private readonly Resource _resource;
        private readonly IMemoryCache _memoryCache;

        public DateHelper(ChineseCalendarInfo chineseCalendar,
            SolarTerm solarTerm,
            IMemoryCache memoryCache,
            Constellation constellation,
            Resource resource)
        {
            _chineseCalendar = chineseCalendar;
            _memoryCache = memoryCache;
            _solarTerm = solarTerm;
            _constellation = constellation;
            _resource = resource;
        }

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public DateTime[] GetDates(string strs)
        {
            DateTime[] dates = null;
            try

            {
                string[] strMonths = strs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
        /// 获取年日期缓存
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>某年的所有日期</returns>
        public List<DateTime> GetYearDaysCache(int year)
        {
            return _memoryCache
                .GetOrCreate($"GetYearDays({year})",
                    entry => GetYearDays(year));
        }

        /// <summary>
        /// 获取年日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>某年的所有日期</returns>
        private List<DateTime> GetYearDays(int year)
        {
            List<DateTime> days = new List<DateTime>();
            for (int i = 1; i <= 12; i++)
            {
                days.AddRange(GetMonthDaysCache(year, i));
            }

            return days;
        }

        /// <summary>
        /// 获取月的日期缓存
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>某年某月的所有日期</returns>
        public List<DateTime> GetMonthDaysCache(int year, int month)
        {
            return _memoryCache
                .GetOrCreate($"GetMonthDays({year},{month})",
                    entry => GetMonthDays(year, month));
        }

        /// <summary>
        /// 获取月的日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>某年某月的所有日期</returns>
        private List<DateTime> GetMonthDays(int year, int month)
        {
            List<DateTime> days = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                days.Add(new DateTime(year, month, i));
            }

            return days;
        }

        public List<Date> GetDaysCache(IEnumerable<DateTime> dts)
        {
            return _memoryCache
                .GetOrCreate($"GetDays({String.Join(',', dts.ToList().Select(c => c.ToString("yyyyMMdd")))})",
                    entry => GetDays(dts));
        }

        private List<Date> GetDays(IEnumerable<DateTime> dts)
        {
            List<Date> days = new List<Date>();
            foreach (DateTime dt in dts)
            {
                days.Add(GetDayCache(dt));
            }
            return days;
        }

        public List<string> GetHoliday(Date date)
        {
            List<string> holidays = new List<string>();
            if (date.Day.LunarHoliday.Value != null)
            {
                holidays.AddRange(date.Day.LunarHoliday.Value?.Split(' '));
            }

            if (date.Day.SolarHoliday.Value != null)
            {
                holidays.AddRange(date.Day.SolarHoliday.Value?.Split(' '));
            }

            if (holidays.Any())
            {
                holidays = holidays.Where(s => !String.IsNullOrEmpty(s)).ToList();
            }

            return holidays;
            //return String.Join(' ', holidays);
        }

        /// <summary>
        /// 获取日期信息缓存
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns>日期信息</returns>
        public Date GetDayCache(DateTime dt)
        {
            return _memoryCache
                 .GetOrCreate(dt.ToShortDateString(),
                     entry => GetDay(dt));
        }

        public Date GetDay(DateTime dt)
        {
            _chineseCalendar.SolarDate = dt;
            Date date = new Date()
            {
                CurrentDate = dt.ToString("D"),
                LunarText = _chineseCalendar.LunarText,
                Year = new Year()
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
                Month = new Month()
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

                Day = new Day()
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

            date.LunarDateSexagenary =
                                $"{date.Year.LunarYearSexagenary}({date.Year.LunarYearAnimal})年{date.Month.LunarMonthSexagenary}月{date.Day.LunarDaySexagenary}日";
            return date;
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