using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.FaqItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqItemController : Controller
    {
        private readonly IFaqItemService _faqItemService;
        private readonly IFaqCategoryService _faqCategoryService;

        public FaqItemController(IFaqItemService faqItemService, IFaqCategoryService faqCategoryService)
        {
            _faqItemService = faqItemService;
            _faqCategoryService = faqCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _faqItemService.GetAllAsync();
            return View(items);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var item = await _faqItemService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _faqCategoryService.GetAllAsync();

            var vm = new FaqItemCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList()
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Create(FaqItemCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _faqCategoryService.GetAllAsync();
                vm.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return View(vm);
            }

            await _faqItemService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _faqItemService.GetByIdAsync(id);
            if (item == null) return NotFound();

            var categories = await _faqCategoryService.GetAllAsync();

            var vm = new FaqItemEditVM
            {
                Id = item.Id,
                Question = item.Question,
                Answer = item.Answer,
                FaqCategoryId = item.FaqCategory.Id,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FaqItemEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _faqCategoryService.GetAllAsync();
                vm.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return View(vm);
            }

            await _faqItemService.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _faqItemService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
