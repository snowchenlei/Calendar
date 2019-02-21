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
        DateTime GetDate(string str);

        /// <summary>
        /// 获取月的日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>某年某月的所有日期</returns>
        IEnumerable<DateTime> GetDatesByMonth(int year, int month);

        /// <summary>
        /// 获取年日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>某年的所有日期</returns>
        IEnumerable<DateTime> GetDatesByYear(int year);

        /// <summary>
        /// 获取节日
        /// </summary>
        /// <param name="calendarDate">万年历日期</param>
        /// <returns></returns>
        IEnumerable<string> GetHoliday(CalendarDate calendarDate);
    }

    /// <summary>
    /// 日期帮助类
    /// </summary>
    public class DateHelper : IDateHelper
    {
        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DateTime GetDate(string str)
        {
            if (!DateTime.TryParse(str, out var date))
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
        public IEnumerable<DateTime> GetDatesByYear(int year)
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
        public IEnumerable<DateTime> GetDatesByMonth(int year, int month)
        {
            //List<DateTime> days = new List<DateTime>();
            for (int i = 1, max = DateTime.DaysInMonth(year, month); i <= max; i++)
            {
                yield return new DateTime(year, month, i);
                //days.Add(new DateTime(year, month, i));
            }

            //return days;
        }

        /// <summary>
        /// 获取节日
        /// </summary>
        /// <param name="calendarDate">万年历日期</param>
        /// <returns></returns>
        public IEnumerable<string> GetHoliday(CalendarDate calendarDate)
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

            if (!String.IsNullOrEmpty(calendarDate.CalendarDay.WeekDayHoliday))
            {
                holidays.Add(calendarDate.CalendarDay.WeekDayHoliday);
            }
            if (holidays.Any())
            {
                holidays = holidays.Where(s => !String.IsNullOrEmpty(s)).ToList();
            }

            return holidays;
        }
    }
}