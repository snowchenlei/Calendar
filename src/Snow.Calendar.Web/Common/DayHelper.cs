using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 日相关
    /// </summary>
    public interface IDayHelper
    {
        /// <summary>
        /// 比较当天是不是指定的第周几
        /// </summary>
        bool CompareWeekDayHoliday(DateTime date, int month, int week, int day);
    }

    /// <summary>
    /// 日相关
    /// </summary>
    public class DayHelper : IDayHelper
    {
        /// <summary>
        /// 比较当天是不是指定的第周几
        /// </summary>
        public bool CompareWeekDayHoliday(DateTime date, int month, int week, int day)
        {
            bool ret = false;

            if (date.Month == month) //月份相同
            {
                if (ConvertDayOfWeek(date.DayOfWeek) == day) //星期几相同
                {
                    //本月第一天
                    DateTime firstDay = date.AddDays(1 - date.Day);
                    //本月第一天是周几
                    int weekday = ConvertDayOfWeek(firstDay.DayOfWeek);
                    //本月第一周有几天
                    int firstWeekEndDay = 7 - ConvertDayOfWeek(firstDay.DayOfWeek) + 1;

                    if (weekday > day)
                    {
                        if ((week - 1) * 7 + day + firstWeekEndDay == date.Day)
                        {
                            ret = true;
                        }
                    }
                    else
                    {
                        if (day + firstWeekEndDay + (week - 2) * 7 == date.Day)
                        {
                            ret = true;
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 将星期几转成数字表示
        /// </summary>
        private int ConvertDayOfWeek(DayOfWeek dayOfWeek)
        {
            return (int)dayOfWeek + 1;
        }
    }
}