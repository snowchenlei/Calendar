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
            "Ԫ��", "����", "Ԫ����", "������", "�����", "�����", "�����"
        };


        public void OnGet()
        {
        }
    }
}
