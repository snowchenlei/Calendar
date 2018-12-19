using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Snow.Calendar.Web.Common;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>
    public class HomeController : Controller
    {
        private readonly DateHelper _dateHelper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _memoryCache;

        public HomeController(DateHelper dateHelper,
            ILogger<HomeController> logger,
            IMemoryCache memoryCache)
        {
            _dateHelper = dateHelper;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        private string[] HolidayDays = {
            "元旦", "春节", "元宵节", "清明节", "端午节", "中秋节", "国庆节"
        };

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.HolidayDays = HolidayDays;
            return View();
        }

        /// <summary>
        /// 获取核心数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetContent(int year, int month)
        {
            return Json(new
            {
                Header = _memoryCache.GetOrCreate("GetHeder", entry => GetHeder()),
                Body = _memoryCache.GetOrCreate($"GetBody({year},{month})", entry => GetBody(year, month))
            });
        }

        /// <summary>
        /// 获取日信息
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>日信息</returns>
        public JsonResult GetDay(int year, int month, int day)
        {
            DateTime thisDate = new DateTime(year, month, day);
            Date date = _dateHelper.GetDayCache(thisDate);
            string big = date.Month.IsBigMonth ? "大" : "小";
            return Json(new
            {
                LunarDateText = $"{date.Year.LunarYearSexagenary}({date.Year.LunarYearAnimal})年 {date.Month.LunarMonthText}月{date.Day.LunarDayText}",
                WorlDay = $"{date.Year.CurrentYear}年{date.Month.CurrentMonth}({big}) {date.Day.DayOfWeekText}",
                NumberDay = date.Day.CurrentDay,
                ChinaDay = $"{date.Month.LunarMonthText}月{date.Day.LunarDayText}",
                LunarDateSexagenary = date.LunarDateSexagenary,
                Animal = date.Year.LunarYearAnimal,
                YearNaYinFiveElements = date.Year.LunarYearNaYinFiveElements,
                MonthNaYinFiveElements = date.Month.LunarMonthNaYinFiveElements,
                date.Day.SolarConstellation,
                date.Day.LunarConstellation,
                date.Day.SolarPalace,
                date.Day.SolarPlanet,
                SolarTermDays = date.Day.SolarTermDays,
                DayNaYinFiveElements = date.Day.LunarDayNaYinFiveElements,
                Holiday = _dateHelper.GetHoliday(date),
                DayText = thisDate.ToString("yyyy年MM月dd日"),
                SubtractDays = Math.Abs(Convert.ToDateTime(DateTime.Now.ToShortDateString()).Subtract(thisDate).Days)
            });
        }

        private Dictionary<DayOfWeek, string> dayOfWeeks = new Dictionary<DayOfWeek, string>()
        {
            [DayOfWeek.Sunday] = "星期日",
            [DayOfWeek.Monday] = "星期一",
            [DayOfWeek.Tuesday] = "星期二",
            [DayOfWeek.Wednesday] = "星期三",
            [DayOfWeek.Thursday] = "星期四",
            [DayOfWeek.Friday] = "星期五",
            [DayOfWeek.Saturday] = "星期六",
        };

        private string GetHeder()
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<tr>");
            foreach (KeyValuePair<DayOfWeek, string> dayOfWeek in dayOfWeeks)
            {
                sbHtml.Append("<td class='thead'>" + dayOfWeek.Value + "</td>");
            }

            sbHtml.Append("</tr>");
            return sbHtml.ToString();
        }

        private string GetBody(int year, int month)
        {
            List<DateTime> days = _dateHelper.GetMonthDaysCache(year, month);
            //_memoryCache.GetOrCreate($"GetMonthDays({year},{month})", entry => _dateHelper.GetMonthDays(year, month));

            List<Date> dates = _dateHelper.GetDaysCache(days);
            //_memoryCache.GetOrCreate($"GetDays({year},{month})", entry => _dateHelper.GetDays(days));
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<tr>");
            foreach (Date date in dates)
            {
                CanlendarDayInfo canlendarDay = GetCanlendarDay(date);
                DayOfWeek current = date.Day.DayOfWeek;
                if (date == dates.First())
                {
                    for (int i = 0, max = dayOfWeeks.Keys.IndexOf(current); i < max; i++)
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
                if (current == dayOfWeeks.Last().Key)
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
        /// <param name="date"></param>
        /// <returns></returns>
        public CanlendarDayInfo GetCanlendarDay(Date date)
        {
            return new CanlendarDayInfo
            {
                CurrentDay = date.Day.CurrentDay,
                LunarDayText = GetSubhead(date),
                DayType = date.Day.DayType,
            };
        }

        /// <summary>
        /// 获取副标题
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetSubhead(Date date)
        {
            string subhead = String.Empty;
            if (!String.IsNullOrEmpty(date.Day.SolarTerm))
            {
                subhead = "<div class='lnumber' style='color:#BC5016;'>" + date.Day.SolarTerm + "</div>";
            }
            //else if (!String.IsNullOrEmpty(date.Day.Holiday))
            else if (!String.IsNullOrEmpty(date.Day.LunarHoliday.Key))
            {
                subhead = $"<div class='lnumber txtOver' style='color:#BC5016;' title='{date.Day.LunarHoliday.Value}'>{date.Day.LunarHoliday.Key}</div>";
            }
            else if (!String.IsNullOrEmpty(date.Day.SolarHoliday.Key))
            {
                subhead = $"<div class='lnumber txtOver' style='color:#BC5016;' title='{date.Day.SolarHoliday.Value}'>{date.Day.SolarHoliday.Key}</div>";
            }
            else if (date.Day.LunarDay == 1)
            {
                subhead = "<div class='lnumber'>" + date.Month.LunarMonthText + "</div>";
            }
            else
            {
                subhead = "<div class='lnumber'>" + date.Day.LunarDayText + "</div>";
            }
            return subhead;
        }

        /// <summary>
        /// 错误页
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            _logger.LogError("Oops!Error Info-----:", error);
            return View();
        }
    }
}