using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using FinalProjectMvc.ViewModels.Admin.StoryItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApproachItemController : Controller
    {
        private readonly IApproachItemService _approachItemService;
        private readonly IApproachService _approachService;
        public ApproachItemController(IApproachItemService approachItemService, IApproachService approachService)
        {
            _approachItemService = approachItemService;
            _approachService = approachService;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var items = await _approachItemService.GetAllAsync();
            return View(items);
        }



        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            var approach = _approachService.GetFirstAsync();

            if (approach == null)
            {
                return RedirectToAction("Index", "Approach");
            }

            var vm = new ApproachItemCreateVM
            {
                ApproachId = approach.Id
            };

            return View(vm);
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(ApproachItemCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _approachItemService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _approachItemService.GetEditAsync(id);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(ApproachItemEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _approachItemService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var item = await _approachItemService.GetDetailAsync(id);

            if (item == null)
            {

                return NotFound();

            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _approachItemService.DeleteAsync(id);
            return Ok(); 
        }

    }
}
