using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
