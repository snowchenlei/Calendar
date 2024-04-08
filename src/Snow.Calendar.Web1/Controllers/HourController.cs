using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snow.Calendar.Web.Common;
using Snow.Calendar.Web.Common.Extension;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Controllers
{
    /// <summary>
    /// 小时
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HourController : ControllerBase
    {
        private readonly ChineseCalendarInfo _chineseCalendar;
        private readonly IDateHelper _dateHelper;

        public HourController(
            ChineseCalendarInfo chineseCalendar,
            IDateHelper dateHelper)
        {
            _chineseCalendar = chineseCalendar;
            _dateHelper = dateHelper;
        }

        /// <summary>
        /// 获取小时的干支
        /// </summary>
        /// <param name="hour">,分割的时间串</param>
        /// <returns></returns>
        [HttpGet, Route("hours")]
        public IActionResult Hours(string hour)
        {
            DateTime[] days = hour.ConvertTo(Convert.ToDateTime);

            List<string> result = new List<string>();
            foreach (DateTime dt in days)
            {
                _chineseCalendar.SolarDate = dt;
                result.Add(_chineseCalendar.LunarHourText + "——" + _chineseCalendar.LunarHourSexagenary);
            }
            return Ok(new Response<List<string>>
            {
                Code = 1,
                Message = "获取成功",
                Data = result
            });
        }
    }
}