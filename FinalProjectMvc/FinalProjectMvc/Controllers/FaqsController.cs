using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class FaqsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
