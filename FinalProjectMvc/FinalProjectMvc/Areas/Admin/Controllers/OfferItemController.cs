using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OfferItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OfferItemController : Controller
    {
        private readonly IOfferItemService _service;

        public OfferItemController(IOfferItemService service)
        {
            _service = service;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var model = await _service.GetDetailAsync(id);
                return View(model);
            }
            catch
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")] 
        public async Task<IActionResult> Create(OfferItemCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            var editVm = new OfferItemEditVM
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description
            };
            return View(editVm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(OfferItemEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.UpdateAsync(vm);
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
