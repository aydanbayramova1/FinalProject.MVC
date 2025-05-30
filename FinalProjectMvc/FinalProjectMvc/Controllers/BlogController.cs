using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
