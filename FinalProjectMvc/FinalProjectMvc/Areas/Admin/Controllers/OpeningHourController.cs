using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OpeningHourController : Controller
    {
        private readonly IOpeningHourService _service;

        public OpeningHourController(IOpeningHourService service)
        {
            _service = service;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAsync();

            var list = new List<OpeningHourVM>();

            if (model != null && !string.IsNullOrWhiteSpace(model.DayRange) && !string.IsNullOrWhiteSpace(model.TimeRange))
            {
                list.Add(model);
            }

            return View(list);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _service.GetDetailAsync(id);
            return View(model);
        }


        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var aboutUsExists = await _service.AnyAsync();
            if (!aboutUsExists)
            {
                TempData["AboutUsMissing"] = "Please create About Us first before adding Opening Hours.";
                return RedirectToAction(nameof(Index));
            }

            if (await _service.ExistsAsync())
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(OpeningHourCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetEditAsync(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(OpeningHourEditVM vm)
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