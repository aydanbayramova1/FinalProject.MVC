using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string query)
        {
            List<MenuProduct> result;

            if (string.IsNullOrWhiteSpace(query))
            {
                result = _context.MenuProducts.Include(p => p.Category).ToList();
                return View(result);
            }

            result = _context.MenuProducts
                .Include(p => p.Category)
                .Where(p =>
                    p.Name.Contains(query) ||
                    p.Price.ToString().Contains(query) ||
                    p.Category.Name.Contains(query))
                .ToList();

            if (result == null || !result.Any())
            {
                return RedirectToAction("Index", "NotFound");
            }

            return View(result);
        }
    }
}
