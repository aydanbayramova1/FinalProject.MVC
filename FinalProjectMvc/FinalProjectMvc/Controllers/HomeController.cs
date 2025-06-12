using System.Diagnostics;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Controllers
{
    public class HomeController : Controller
    {      

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(string query)
        {
            List<Product> result;

            if (string.IsNullOrWhiteSpace(query))
            {
                result = _context.Products.Include(p => p.Category).ToList();
                return View(result);
            }

            result = _context.Products
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
