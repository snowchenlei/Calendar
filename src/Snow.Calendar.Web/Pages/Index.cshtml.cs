using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Snow.Calendar.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public string[] HolidayDays = {
            "元旦", "春节", "元宵节", "清明节", "端午节", "中秋节", "国庆节"
        };


        public void OnGet()
        {
        }
    }
}
