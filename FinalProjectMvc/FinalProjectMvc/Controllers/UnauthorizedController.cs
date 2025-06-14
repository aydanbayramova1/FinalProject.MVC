using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class UnauthorizedController : Controller
    {
        public IActionResult Index()
        {
            Response.StatusCode = 401;
            return View();
        }
    }
}
