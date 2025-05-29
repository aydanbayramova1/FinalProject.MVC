using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
