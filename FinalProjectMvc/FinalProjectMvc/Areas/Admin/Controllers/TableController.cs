using FinalProjectMvc.Data;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        private readonly AppDbContext _context;

        public TableController(ITableService tableService, AppDbContext context)
        {
            _tableService = tableService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tables = await _tableService.GetAllAsync();
            return View(tables);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var table = await _tableService.GetByIdAsync(id);
            if (table == null) return NotFound();

            return View(table);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(TableCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var existingTable = await _context.Tables
                .FirstOrDefaultAsync(t => t.Number == vm.Number);
            if (existingTable != null)
            {
                ModelState.AddModelError("Number", "This table number already exists.");
                return View(vm);
            }

            await _tableService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            if (id == 0)
                return Json(new { success = false, message = "Invalid table ID." });

            var table = await _tableService.GetByIdAsync(id);
            if (table == null)
                return Json(new { success = false, message = "Table not found." });

            await _tableService.DeleteAsync(id);
            return Json(new { success = true, message = "Table deleted successfully." });
        }
    }
}
