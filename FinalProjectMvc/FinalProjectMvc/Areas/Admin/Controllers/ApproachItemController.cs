using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApproachItemController : Controller
    {
        private readonly IApproachItemService _itemService;
        private readonly IMapper _mapper;

        public ApproachItemController(IApproachItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _itemService.GetAllAsync();

            // Əgər service'dən ApproachItemDetailVM gəlirsə, bura map lazımdır
            var result = _mapper.Map<IEnumerable<ApproachItemVM>>(items);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApproachItemCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _itemService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var detail = await _itemService.GetByIdAsync(id);
            if (detail == null) return NotFound();

            var model = _mapper.Map<ApproachItemEditVM>(detail);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApproachItemEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _itemService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _itemService.GetByIdAsync(id);
            if (detail == null) return NotFound();

            var result = _mapper.Map<ApproachItemDetailVM>(detail);
            return View(result);
        }
    }
}
