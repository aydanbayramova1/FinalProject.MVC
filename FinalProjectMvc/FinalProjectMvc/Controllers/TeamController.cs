using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
