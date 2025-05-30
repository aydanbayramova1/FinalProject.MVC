using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
