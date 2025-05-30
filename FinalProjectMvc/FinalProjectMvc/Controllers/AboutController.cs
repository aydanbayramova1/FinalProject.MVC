using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
