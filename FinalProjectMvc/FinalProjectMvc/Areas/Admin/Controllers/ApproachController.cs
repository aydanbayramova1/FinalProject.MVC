using AutoMapper;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Approach;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApproachController : Controller
    {
        private readonly IApproachService _approachService;

        public ApproachController(IApproachService approachService)
        {
            _approachService = approachService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _approachService.GetAllAsync();
            ViewBag.IsApproachExists = data.Any(); // Yalnız bir dənə varsa view-ə xəbər ver
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var exists = await _approachService.IsExistsAsync();
            if (exists)
            {
                TempData["Error"] = "Only one approach is allowed.";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApproachCreateVM model)
        {
            var exists = await _approachService.IsExistsAsync();
            if (exists)
            {
                TempData["Error"] = "Only one approach is allowed.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
                return View(model);

            await _approachService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var detail = await _approachService.GetByIdAsync(id);
            if (detail == null) return NotFound();

            var model = new ApproachEditVM
            {
                Id = detail.Id,
                Title = detail.Title,
                SubTitle = detail.SubTitle,
                Image = detail.Image
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApproachEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _approachService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _approachService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _approachService.GetByIdAsync(id);
            if (detail == null) return NotFound();

            return View(detail);
        }
    }
}
