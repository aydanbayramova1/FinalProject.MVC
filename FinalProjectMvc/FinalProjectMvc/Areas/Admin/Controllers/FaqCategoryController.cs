using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.FaqCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqCategoryController : Controller
    {
        private readonly IFaqCategoryService _faqCategoryService;

        public FaqCategoryController(IFaqCategoryService faqCategoryService)
        {
            _faqCategoryService = faqCategoryService;
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var categories = await _faqCategoryService.GetAllAsync();
            return View(categories);
        }



        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            var vm = new FaqCategoryCreateVM();
            return View(vm); 
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(FaqCategoryCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _faqCategoryService.CreateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }




        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _faqCategoryService.GetEditVMAsync(id);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(FaqCategoryEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _faqCategoryService.UpdateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var vm = await _faqCategoryService.GetByIdAsync(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _faqCategoryService.DeleteAsync(id);
            return Ok();
        }

    }
}
