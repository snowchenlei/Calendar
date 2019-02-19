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
        private readonly Resource _resource;
        private readonly IDateHelper _dateHelper;

        public HolidayController(
            Resource resource,
            IDateHelper dateHelper)
        {
            _resource = resource;
            _dateHelper = dateHelper;
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
            DateTime[] days = _dateHelper.GetDates(day);

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
            DateTime[] months = _dateHelper.GetDates(month);
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
            string[] strMonths = year.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] years = Array.ConvertAll(strMonths, Convert.ToInt32);
            List<DateTime> days = new List<DateTime>();
            foreach (int y in years)
            {
                days.AddRange(_dateHelper.GetDaysByYear(y));
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
                                    DayType = GetDayType(s3)
                                })
                            }
                        )
                });
        }

        /// <summary>
        /// 获取日期类型
        /// </summary>
        /// <param name="day">某个日期</param>
        /// <returns></returns>
        private DayType GetDayType(DateTime day)
        {
            DayOfWeek dayOfWeek = day.DayOfWeek;
            if (IsHoliday(day))
            {
                return DayType.Holiday;
            }

            if (IsCompensationWork(day))
            {
                return DayType.Workday;
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