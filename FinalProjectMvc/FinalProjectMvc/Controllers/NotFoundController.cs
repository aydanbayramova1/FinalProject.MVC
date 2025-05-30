using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
