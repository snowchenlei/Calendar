using System.Text;
using Snow.Calendar.Common;
using Snow.Calendar.Common.Model;
using Snow.Calendar.Common.Service;
using Snow.Calendar.Web.Common;

namespace Snow.Calendar.Web.Model
{
    /// <summary>
    /// 编译Html
    /// </summary>
    public interface IBuildHtml
    {
        /// <summary>
        /// 创建头部
        /// </summary>
        /// <returns></returns>
        string CreateHeader();

        /// <summary>
        /// 创建内容
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        string CreateBody(int year, int month);
    }

    /// <summary>
    /// 编译Html
    /// </summary>
    public class BuildHtml : IBuildHtml
    {
        private readonly Resource _resource;
        private readonly IDateHelper _dateHelper;
        private readonly ICalendarDateHelper _calendarDateHelper;

        //private readonly Dictionary<DayOfWeek, string> OneWeek = new Dictionary<DayOfWeek, string>()
        //{
        //    [DayOfWeek.Monday] = "星期一",
        //    [DayOfWeek.Tuesday] = "星期二",
        //    [DayOfWeek.Wednesday] = "星期三",
        //    [DayOfWeek.Thursday] = "星期四",
        //    [DayOfWeek.Friday] = "星期五",
        //    [DayOfWeek.Saturday] = "星期六",
        //    [DayOfWeek.Sunday] = "星期日",
        //};

        public BuildHtml(
            Resource resource,
            IDateHelper dateHelper,
            ICalendarDateHelper calendarDateHelper)
        {
            _resource = resource;
            _dateHelper = dateHelper;
            _calendarDateHelper = calendarDateHelper;
        }

        /// <summary>
        /// 创建头部
        /// </summary>
        /// <returns></returns>
        public string CreateHeader()
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<tr>");
            foreach (string dayOfWeek in _resource.OneWeek.Values)
            {
                sbHtml.Append("<td class='thead'>" + dayOfWeek + "</td>");
            }

            sbHtml.Append("</tr>");
            return sbHtml.ToString();
        }

        /// <summary>
        /// 创建内容
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public string CreateBody(int year, int month)
        {
            IEnumerable<DateTime> days = _dateHelper.GetDatesByMonth(year, month);
            IEnumerable<CalendarDate> dates = _calendarDateHelper.GetCalendarDates(days);
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<tr>");
            var calendarDates = dates as CalendarDate[] ?? dates.ToArray();
            foreach (CalendarDate date in calendarDates)
            {
                CanlendarDayInfo canlendarDay = GetCanlendarDay(date);
                DayOfWeek current = date.CalendarDay.DayOfWeek;
                if (Array.IndexOf(calendarDates, date) == 0)
                {
                    for (int i = 0, max = Array.IndexOf(_resource.OneWeek.Keys.ToArray(), current); i < max; i++)
                    {
                        sbHtml.Append("<td style='height: 16%;' class='block'></td>");
                    }
                }

                sbHtml.Append(DateTime.Now.GetDateTimeFormats('D')[0] == date.CurrentDate
                    ? "<td style='height: 16%;' class='block today'>"
                    : "<td style='height: 16%;' class='block'>");
                sbHtml.Append("<a class='block_content' href='javascript:;'>");
                if (canlendarDay.DayType == DayType.CompensationWork)
                {
                    sbHtml.Append(
                        "<div class='calendarHoliday calendarWork'>班</div>");
                }
                else if (canlendarDay.DayType == DayType.Holiday)
                {
                    sbHtml.Append(
                        "<div class='calendarHoliday'>休</div>");
                }
                sbHtml.Append(
                    "<div style='display:inline-block;position:absolute;top:50%;width:100%;margin-top:-22px;left:0;'>");
                sbHtml.Append("<div class='number'>" + canlendarDay.CurrentDay + "</div>");
                //sbHtml.AppendFormat("<div class='lnumber'>{0}</div>", canlendarDay.LunarDayText);
                sbHtml.AppendFormat(canlendarDay.LunarDayText);
                sbHtml.Append("</div></a></td>");
                if (current == _resource.OneWeek.Last().Key)
                {
                    sbHtml.Append("</tr><tr>");
                }
            }
            sbHtml.Append("</tr>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// 获取万年历信息
        /// </summary>
        /// <param name="calendarDate"></param>
        /// <returns></returns>
        private CanlendarDayInfo GetCanlendarDay(CalendarDate calendarDate)
        {
            return new CanlendarDayInfo
            {
                CurrentDay = calendarDate.CalendarDay.CurrentDay,
                LunarDayText = GetSubhead(calendarDate),
                DayType = calendarDate.CalendarDay.DayType,
            };
        }

        /// <summary>
        /// 获取副标题
        /// </summary>
        /// <param name="calendarDate"></param>
        /// <returns></returns>
        private string GetSubhead(CalendarDate calendarDate)
        {
            string subhead = String.Empty;
            if (!String.IsNullOrEmpty(calendarDate.CalendarDay.SolarTerm))
            {
                subhead = "<div class='lnumber' style='color:#BC5016;'>" + calendarDate.CalendarDay.SolarTerm + "</div>";
            }
            //else if (!String.IsNullOrEmpty(calendarDate.CalendarDay.Holiday))
            else if (!String.IsNullOrEmpty(calendarDate.CalendarDay.LunarHoliday.Key))
            {
                subhead = $"<div class='lnumber txtOver' style='color:#BC5016;' title='{calendarDate.CalendarDay.LunarHoliday.Value}'>{calendarDate.CalendarDay.LunarHoliday.Key}</div>";
            }
            else if (!String.IsNullOrEmpty(calendarDate.CalendarDay.SolarHoliday.Key))
            {
                subhead = $"<div class='lnumber txtOver' style='color:#BC5016;' title='{calendarDate.CalendarDay.SolarHoliday.Value}'>{calendarDate.CalendarDay.SolarHoliday.Key}</div>";
            }
            else if (!String.IsNullOrEmpty(calendarDate.CalendarDay.WeekDayHoliday))
            {
                subhead = $"<div class='lnumber txtOver' style='color:#BC5016;' title='{calendarDate.CalendarDay.WeekDayHoliday}'>{calendarDate.CalendarDay.WeekDayHoliday}</div>";
            }
            else if (calendarDate.CalendarDay.LunarDay == 1)
            {
                subhead = "<div class='lnumber'>" + calendarDate.CalendarMonth.LunarMonthText + "</div>";
            }
            else
            {
                subhead = "<div class='lnumber'>" + calendarDate.CalendarDay.LunarDayText + "</div>";
            }
            return subhead;
        }
    }
}