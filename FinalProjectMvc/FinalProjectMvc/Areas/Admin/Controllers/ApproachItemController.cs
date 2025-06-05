using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApproachItemController : Controller
    {
        private readonly IApproachItemService _service;

        public ApproachItemController(IApproachItemService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int approachId)
        {
            var items = await _service.GetByIdAsync(approachId);
            ViewBag.ApproachId = approachId;
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        public IActionResult Create(int approachId)
        {
            return View(new ApproachItemCreateVM { ApproachId = approachId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApproachItemCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.CreateAsync(vm);
            return RedirectToAction("Index", "Approach");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null) return NotFound();

            var editVM = new ApproachItemEditVM
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                IconPath = model.IconPath,
            };

            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ApproachItemEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.UpdateAsync(id, vm);
            return RedirectToAction("Index", "Approach");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index", "Approach");
        }
    }
}

