using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class BadRequestController : Controller
    {
        public IActionResult Index()
        {
            Response.StatusCode = 400;
            return View();
        }
    }
}
