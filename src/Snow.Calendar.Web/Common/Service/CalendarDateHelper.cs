using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Common
{
    public interface ICalendarDateHelper
    {
        /// <summary>
        /// 公历日期转万年历日期
        /// </summary>
        /// <param name="dts">公历日期</param>
        /// <returns>万年历日期</returns>
        IEnumerable<CalendarDate> GetCalendarDates(IEnumerable<DateTime> dts);

        /// <summary>
        /// 公历日期转万年历日期
        /// </summary>
        /// <param name="dt">公历日期</param>
        /// <returns>万年历日期</returns>
        CalendarDate GetCalendarDate(DateTime dt);
    }

    /// <summary>
    /// 万年历日期类
    /// </summary>
    public class CalendarDateHelper : ICalendarDateHelper
    {
        private readonly SolarTerm _solarTerm;
        private readonly Constellation _constellation;
        private readonly IHolidayHelper _holidayHelper;
        private readonly ChineseCalendarInfo _chineseCalendar;

        public CalendarDateHelper(
            SolarTerm solarTerm
            , Constellation constellation
            , IHolidayHelper holidayHelper
            , ChineseCalendarInfo chineseCalendar)
        {
            _solarTerm = solarTerm;
            _holidayHelper = holidayHelper;
            _constellation = constellation;
            _chineseCalendar = chineseCalendar;
        }

        /// <summary>
        /// 公历日期转万年历日期
        /// </summary>
        /// <param name="dts">公历日期</param>
        /// <returns>万年历日期</returns>
        public IEnumerable<CalendarDate> GetCalendarDates(IEnumerable<DateTime> dts)
        {
            //List<CalendarDate> days = new List<CalendarDate>();
            foreach (DateTime dt in dts)
            {
                yield return GetCalendarDate(dt);
                //days.Add(GetCalendarDate(dt));
            }
            //return days;
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
                    DayType = _holidayHelper.GetDayType(dt),
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
    }
}