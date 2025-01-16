using System.Globalization;
using Snow.Calendar.Common.Model;
using Snow.Calendar.Web.Common;

namespace Snow.Calendar.Common.Service
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
        /// 按公历日计算的节日
        /// </summary>
        KeyValuePair<string, string> GetSolarHoliday(DateTime solarDate);

        /// <summary>
        /// 计算中国农历节日
        /// </summary>
        KeyValuePair<string, string> GetLunarHoliday(ChineseCalendarInfo chineseCalendarInfo);

        /// <summary>
        /// 按某月第几周第几日计算的节日
        /// </summary>
        string GetWeekDayHoliday(DateTime solarDate);
    }

    /// <summary>
    /// 节假日帮助类
    /// </summary>
    public class HolidayHelper : IHolidayHelper
    {
        private readonly Resource _resource;
        private readonly IDayHelper _dayHelper;
        private readonly ChineseLunisolarCalendar _chineseLunisolarCalendar;

        public HolidayHelper(
            Resource resource
            , IDayHelper dayHelper
            , ChineseLunisolarCalendar chineseLunisolarCalendar)
        {
            _resource = resource;
            _dayHelper = dayHelper;
            _chineseLunisolarCalendar = chineseLunisolarCalendar; //new ChineseLunisolarCalendar();
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
        /// 按公历日计算的节日
        /// </summary>
        public KeyValuePair<string, string> GetSolarHoliday(DateTime solarDate)
        {
            KeyValuePair<string, string> tempStr = _resource.SolarHoliday
                .Where(s => s.Month == solarDate.Month && s.Day == solarDate.Day)
                .Select(s => new KeyValuePair<string, string>(s.HolidayName, s.HolidayAll))
                .FirstOrDefault();
            return tempStr;
        }

        /// <summary>
        /// 计算中国农历节日
        /// </summary>
        public KeyValuePair<string, string> GetLunarHoliday(ChineseCalendarInfo chineseCalendarInfo)
        {
            KeyValuePair<string, string> tempStr = new KeyValuePair<string, string>();// = String.Empty;
            if (chineseCalendarInfo.IsLeapLunarMonth == false) //闰月不计算节日
            {
                if (_resource.LunarHoliday.Any(l => l.Month == chineseCalendarInfo.LunarMonth
                                            && l.Day == chineseCalendarInfo.LunarDay))
                {
                    tempStr = _resource.LunarHoliday.Where(l => l.Month == chineseCalendarInfo.LunarMonth
                                                        && l.Day == chineseCalendarInfo.LunarDay)
                        .Select(l => new KeyValuePair<string, string>(l.HolidayName, l.HolidayAll))
                        .FirstOrDefault();
                }
                else
                {
                    //对除夕进行特别处理
                    if (chineseCalendarInfo.LunarMonth == 12)
                    {
                        int i = _chineseLunisolarCalendar
                            .GetDaysInMonth(chineseCalendarInfo.LunarYear, chineseCalendarInfo.LunarMonth);//计算当年农历12月的总天数
                        if (chineseCalendarInfo.LunarDay == i) //如果为最后一天
                        {
                            tempStr = new KeyValuePair<string, string>("除夕", "除夕");
                        }
                    }
                }
            }
            return tempStr;
        }

        /// <summary>
        /// 按某月第几周第几日计算的节日
        /// </summary>
        public string GetWeekDayHoliday(DateTime solarDate)
        {
            string tempStr = String.Empty;
            foreach (WeekHoliday wh in _resource.WeekHoliday)
            {
                if (_dayHelper.CompareWeekDayHoliday(solarDate, wh.Month, wh.WeekAtMonth, wh.WeekDay))
                {
                    tempStr = wh.HolidayName;
                    break;
                }
            }
            return tempStr;
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