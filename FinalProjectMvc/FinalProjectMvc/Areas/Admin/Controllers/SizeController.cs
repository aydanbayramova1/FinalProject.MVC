using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Size;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly ISizeService _service;

        public SizeController(ISizeService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var sizes = await _service.GetAllAsync();
            return View(sizes);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(SizeCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
