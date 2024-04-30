using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snow.Calendar.Web.Common;
using Snow.Calendar.Web.Model;
using System.Data;

namespace Snow.Calendar.Web.Pages
{
    public class IndexModel : PageModel
    {
        public readonly Resource Resource;
        private readonly ILogger<IndexModel> _logger;
        private readonly IBuildHtml _buildHtml;
        private readonly IDateHelper _dateHelper;
        private readonly ICalendarDateHelper _calendarDateHelper;

        [BindProperty(SupportsGet = true)]
        public int? Year { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Month { get; set; }

        public string DateRaw { get; set; }

        public IndexModel(ILogger<IndexModel> logger, Resource resource, IDateHelper dateHelper, ICalendarDateHelper calendarDateHelper, IBuildHtml buildHtml)
        {
            _logger = logger;
            Resource = resource;
            _dateHelper = dateHelper;
            _calendarDateHelper = calendarDateHelper;
            _buildHtml = buildHtml;
        }
        public List<IndexModelModel> DateModels { get; set; }
        public string[] HolidayDays = {
            "元旦", "春节", "元宵节", "清明节", "端午节", "中秋节", "国庆节"
        };


        public void OnGet()
        {
            if (!Year.HasValue)
            {
                Year = DateTime.Now.Year;
            }
            if(!Month.HasValue)
            {
                Month = DateTime.Now.Month;
            }
            DateRaw = _buildHtml.CreateBody(Year.Value, Month.Value);
            IEnumerable<DateTime> days = _dateHelper.GetDatesByMonth(Year.Value, Month.Value);
            IEnumerable<CalendarDate> calendarDates = _calendarDateHelper.GetCalendarDates(days);
            DateModels = new List<IndexModelModel>();
            foreach (CalendarDate calendarDate in calendarDates)
            {
                DateModels.Add(new IndexModelModel()
                {
                    CalendarDate = calendarDate,
                    CurrentDay = calendarDate.CalendarDay.CurrentDay,
                    DayType = calendarDate.CalendarDay.DayType,
                });
            }
        }
    }
    public class IndexModelModel
    {
        public CalendarDate CalendarDate { get; set;}
        public int CurrentDay { get; set; }
        public DayType DayType { get; set; }
    }
}
