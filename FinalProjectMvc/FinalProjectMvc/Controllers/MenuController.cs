using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
