﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Snow.Calendar.Web.Common;
using Snow.Calendar.Web.Common.Extension;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Controllers
{
    /// <summary>
    /// 万年历
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DayController : ControllerBase
    {
        private readonly IDateHelper _dateHelper;
        private readonly ChineseCalendarInfo _chineseCalendar;
        private readonly ICalendarDateHelper _calendarDateHelper;

        public DayController(
            ChineseCalendarInfo chineseCalendar,
            IDateHelper dateHelper,
            ICalendarDateHelper calendarDateHelper)
        {
            _dateHelper = dateHelper;
            _chineseCalendar = chineseCalendar;
            _calendarDateHelper = calendarDateHelper;
        }

        /// <summary>
        /// 根据天获取
        /// </summary>
        /// <remarks>
        /// 请求示例：
        /// Get /api/day/days?day=2018-09-23,2018-09-24
        /// </remarks>
        /// <param name="day">,分割的时间串</param>
        /// <response code="200">获取成功</response>
        /// <returns></returns>
        [HttpGet, Route("days"),
         ProducesResponseType(typeof(Response<IEnumerable<CalendarDate>>), 200)]
        public IActionResult Days(string day)
        {
            DateTime[] days = day.ConvertTo(Convert.ToDateTime);
            return Ok(new Response<IEnumerable<CalendarDate>>()
            {
                Code = 1,
                Message = "获取成功",
                Data = _calendarDateHelper.GetCalendarDates(days)
            });
        }

        /// <summary>
        /// 根据月获取
        /// </summary>
        /// <remarks>
        /// 请求示例：
        /// Get /api/day/months?day=2018-08-23,2018-09-24
        /// </remarks>
        /// <param name="month">,分割的时间串</param>
        /// <response code="200">获取成功</response>
        /// <returns></returns>
        [HttpGet, Route("months"),
         ProducesResponseType(typeof(Response<IEnumerable<CalendarDate>>), 200)]
        public IActionResult Months(string month)
        {
            DateTime[] months = month.ConvertTo(Convert.ToDateTime);

            List<DateTime> days = new List<DateTime>();
            foreach (DateTime m in months)
            {
                days.AddRange(_dateHelper.GetDatesByMonth(m.Year, m.Month));
            }
            return Ok(new Response<IEnumerable<CalendarDate>>()
            {
                Code = 1,
                Message = "获取成功",
                Data = _calendarDateHelper.GetCalendarDates(days)
            });
        }

        /// <summary>
        /// 转农历
        /// </summary>
        /// <remarks>
        /// 请求示例：
        /// Get /api/day/to_lunar?day=2018-08-23
        /// </remarks>
        /// <param name="day">阳历日期</param>
        /// <returns></returns>
        [HttpGet, Route("to_lunar")]
        public IActionResult Lunar(string day)
        {
            DateTime date = _dateHelper.GetDate(day);
            _chineseCalendar.SolarDate = date;
            return Ok(new Response<string>()
            {
                Code = 1,
                Message = "获取成功",
                Data = _chineseCalendar.LunarText
            });
        }
    }
}