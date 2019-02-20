using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Snow.Calendar.Web.Common;
using Snow.Calendar.Web.Common.Extension;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Controllers
{
    /// <summary>
    /// 节日
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IDateHelper _dateHelper;
        private readonly IHolidayHelper _holidayHelper;

        public HolidayController(
            IDateHelper dateHelper
            , IHolidayHelper holidayHelper)
        {
            _dateHelper = dateHelper;
            _holidayHelper = holidayHelper;
        }

        /// <summary>
        /// 根据天获取
        /// </summary>
        /// <remarks>
        /// 请求示例：
        /// Get /api/holiday/days?days=2018-09-23,2018-09-24
        /// </remarks>
        /// <param name="day">,分割的时间串</param>
        /// <response code="200">获取成功</response>
        /// <returns></returns>
        [HttpGet, Route("days"),
         ProducesResponseType(typeof(Response<IEnumerable<HolidayYears>>), 200)]
        public IActionResult Days(string day)
        {
            DateTime[] days = day.ConvertTo(Convert.ToDateTime);

            return Ok(new Response<IEnumerable<HolidayYears>>()
            {
                Code = 1,
                Message = "获取成功",
                Data = GetHolidayOutputs(days)
            });
        }

        /// <summary>
        /// 根据月获取
        /// </summary>
        /// <remarks>
        /// 请求示例：
        /// Get /api/holiday/months?months=2018-08-23,2018-09-24
        /// </remarks>
        /// <param name="month">,分割的时间串</param>
        /// <response code="200">获取成功</response>
        /// <returns></returns>
        [HttpGet, Route("months"),
         ProducesResponseType(typeof(Response<IEnumerable<HolidayYears>>), 200)]
        public IActionResult Months(string month)
        {
            DateTime[] months = month.ConvertTo(Convert.ToDateTime);
            List<DateTime> days = new List<DateTime>();
            foreach (DateTime m in months)
            {
                days.AddRange(_dateHelper.GetDatesByMonth(m.Year, m.Month));
            }
            return Ok(new Response<IEnumerable<HolidayYears>>()
            {
                Code = 1,
                Message = "获取成功",
                Data = GetHolidayOutputs(days)
            });
        }

        /// <summary>
        /// 根据年获取
        /// </summary>
        /// <remarks>
        /// 请求示例：
        /// Get /api/holiday/years?year=2018,2019
        /// </remarks>
        /// <param name="year">,分割的时间串</param>
        /// <response code="200">获取成功</response>
        /// <returns></returns>
        [HttpGet, Route("years"),
         ProducesResponseType(typeof(Response<IEnumerable<HolidayYears>>), 200)]
        public IActionResult Years(string year)
        {
            int[] years = year.ConvertTo(Convert.ToInt32);
            List<DateTime> days = new List<DateTime>();
            foreach (int y in years)
            {
                days.AddRange(_dateHelper.GetDatesByYear(y));
            }
            return Ok(new Response<IEnumerable<HolidayYears>>()
            {
                Code = 1,
                Message = "获取成功",
                Data = GetHolidayOutputs(days)
            });
        }

        private IEnumerable<HolidayYears> GetHolidayOutputs(IEnumerable<DateTime> days)
        {
            return days.OrderBy(s => s.Year).GroupBy(
                s => s.Year,
                (year, yearGroup) => new HolidayYears
                {
                    Year = year,
                    HolidayMonths =
                        yearGroup.OrderBy(s2 => s2.Month).GroupBy(
                            s2 => s2.Month,
                            (month, monthGroup) => new HolidayMonths
                            {
                                Month = month,
                                HolidayDays = monthGroup.OrderBy(s3 => s3.Day).Select(s3 => new HolidayDays
                                {
                                    Day = s3.Day,
                                    Date = s3.ToString("yyyy/MM/dd"),
                                    DayType = _holidayHelper.GetDayType(s3)
                                })
                            }
                        )
                });
        }
    }
}