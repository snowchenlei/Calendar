using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Snow.Calendar.Web.Common;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBuildHtml _buildHtml;
        private readonly IDateHelper _dateHelper;
        private readonly ICalendarDateHelper _calendarDateHelper;

        //private readonly DateHelper _dateHelper;
        private readonly IMemoryCache _memoryCache;

        public HomeController(IDateHelper dateHelper,
            ILogger<HomeController> logger,
            IBuildHtml buildHtml,
            IMemoryCache memoryCache
            , ICalendarDateHelper calendarDateHelper)
        {
            _logger = logger;
            _buildHtml = buildHtml;
            _dateHelper = dateHelper;
            _memoryCache = memoryCache;
            _calendarDateHelper = calendarDateHelper;
        }

        /// <summary>
        /// 获取核心数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("content")]
        public object GetContent(int year, int month)
        {
            return new
            {
                Header = _buildHtml.CreateHeder(),
                Body = _buildHtml.CreateBody(year, month)
            };
        }

        /// <summary>
        /// 获取日信息
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>日信息</returns>
        [HttpGet("day")]
        public object GetDay(int year, int month, int day)
        {
            DateTime thisDate = new DateTime(year, month, day);
            CalendarDate calendarDate = _calendarDateHelper.GetCalendarDate(thisDate);
            string big = calendarDate.CalendarMonth.IsBigMonth ? "大" : "小";
            return new
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
            };
        }
    }
}