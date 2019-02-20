using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 节假日帮助接口
    /// </summary>
    public interface IHolidayHelper
    {
        /// <summary>
        /// 获取日期类型
        /// </summary>
        /// <param name="day">某个日期</param>
        /// <returns>日期类型</returns>
        DayType GetDayType(DateTime day);

        /// <summary>
        /// 是否是法定休息日
        /// </summary>
        /// <param name="day">某个日期</param>
        /// <returns></returns>
        bool IsHoliday(DateTime day);

        /// <summary>
        /// 是否是补班
        /// </summary>
        /// <param name="day">某个日期</param>
        /// <returns></returns>
        bool IsCompensationWork(DateTime day);
    }

    /// <summary>
    /// 节假日帮助类
    /// </summary>
    public class HolidayHelper : IHolidayHelper
    {
        private readonly Resource _resource;

        public HolidayHelper(
            Resource resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// 获取日期类型
        /// </summary>
        /// <param name="day">某个日期</param>
        /// <returns>日期类型</returns>
        public DayType GetDayType(DateTime day)
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
        public bool IsHoliday(DateTime day)
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
        public bool IsCompensationWork(DateTime day)
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