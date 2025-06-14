using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutUsItemController : Controller
    {
        private readonly IAboutUsItemService _service;
        private readonly IAboutUsService _aboutUsService;

        public AboutUsItemController(IAboutUsItemService service, IAboutUsService aboutUsService)
        {
            _service = service;
            _aboutUsService = aboutUsService;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return View(items);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var vm = await _service.GetDetailAsync(id);
            return View(vm);
        }


        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var aboutUs = await _aboutUsService.GetAsync();
            if (aboutUs == null)
            {
                TempData["Error"] = "Please create AboutUs first.";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(AboutUsItemCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _service.GetEditAsync(id);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(AboutUsItemEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(); 
        }

    }
}
