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
        private readonly ILogger _logger;
        private readonly IBuildHtml _buildHtml;
        private readonly IDateHelper _dateHelper;

        //private readonly DateHelper _dateHelper;
        private readonly IMemoryCache _memoryCache;

        public HomeController(IDateHelper dateHelper,
            ILogger<HomeController> logger,
            IBuildHtml buildHtml,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _buildHtml = buildHtml;
            _dateHelper = dateHelper;
            _memoryCache = memoryCache;
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
                Header = _buildHtml.CreateHeder(),
                Body = _buildHtml.CreateBody(year, month)
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
            CalendarDate calendarDate = _dateHelper.GetCalendarDate(thisDate);
            string big = calendarDate.CalendarMonth.IsBigMonth ? "大" : "小";
            return Json(new
            {
                LunarDateText = $"{calendarDate.CalendarYear.LunarYearSexagenary}({calendarDate.CalendarYear.LunarYearAnimal})年 {calendarDate.CalendarMonth.LunarMonthText}月{calendarDate.CalendarDay.LunarDayText}",
                WorlDay = $"{calendarDate.CalendarYear.CurrentYear}年{calendarDate.CalendarMonth.CurrentMonth}({big}) {calendarDate.CalendarDay.DayOfWeekText}",
                NumberDay = calendarDate.CalendarDay.CurrentDay,
                ChinaDay = $"{calendarDate.CalendarMonth.LunarMonthText}月{calendarDate.CalendarDay.LunarDayText}",
                LunarDateSexagenary = calendarDate.LunarDateSexagenary,
                Animal = calendarDate.CalendarYear.LunarYearAnimal,
                YearNaYinFiveElements = calendarDate.CalendarYear.LunarYearNaYinFiveElements,
                MonthNaYinFiveElements = calendarDate.CalendarMonth.LunarMonthNaYinFiveElements,
                calendarDate.CalendarDay.SolarConstellation,
                calendarDate.CalendarDay.LunarConstellation,
                calendarDate.CalendarDay.SolarPalace,
                calendarDate.CalendarDay.SolarPlanet,
                SolarTermDays = calendarDate.CalendarDay.SolarTermDays,
                DayNaYinFiveElements = calendarDate.CalendarDay.LunarDayNaYinFiveElements,
                Holiday = _dateHelper.GetHoliday(calendarDate),
                DayText = thisDate.ToString("yyyy年MM月dd日"),
                SubtractDays = Math.Abs(Convert.ToDateTime(DateTime.Now.ToShortDateString()).Subtract(thisDate).Days)
            });
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