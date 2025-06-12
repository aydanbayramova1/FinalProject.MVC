using FinalProjectMvc.Helpers;
using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.MenuProduct;
using FinalProjectMvc.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuProductController : Controller
    {
        private readonly IMenuProductService _menuProductService;
        private readonly ICategoryService _categoryService;
        private readonly ISizeService _sizeService;

        public MenuProductController(
            IMenuProductService menuProductService,
            ICategoryService categoryService,
            ISizeService sizeService)
        {
            _menuProductService = menuProductService;
            _categoryService = categoryService;
            _sizeService = sizeService;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            const int pageSize = 10;

            var allProducts = await _menuProductService.GetAllAsync();

            var query = await _menuProductService.GetAllQueryAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Ingredients.Contains(search));
            }

            query = query.OrderBy(p => p.Name);

            var pagedResult = query.ToPagedResult(page, pageSize);

            ViewBag.CurrentSearch = search;
            ViewBag.AllProducts = allProducts; 

            return View(pagedResult);
        }


        public async Task<IActionResult> Create()
        {
            var vm = await _menuProductService.GetCreateVMAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuProductCreateVM vm)
        {
            if (!await _menuProductService.ValidateMenuProductAsync(vm))
            {
                var categoryType = await _menuProductService.GetCategoryTypeAsync(vm.CategoryId);
                if (categoryType == "Drink")
                {
                    ModelState.AddModelError("SelectedSizeIds", "İçki məhsulları üçün həmişə 3 ölçü seçilməlidir (Small, Medium, Large).");
                }
            }

            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            vm.Categories = await _categoryService.GetSelectListAsync();
            vm.Sizes = await _sizeService.GetSelectListAsync();
            vm.CategoryTypesById = await _categoryService.GetCategoryTypesWithIdAsync();
            await _menuProductService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _menuProductService.GetEditVMAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuProductEditVM vm)
        {
            if (!await _menuProductService.ValidateMenuProductAsync(vm))
            {
                var categoryType = await _menuProductService.GetCategoryTypeAsync(vm.CategoryId);
                if (categoryType == "Drink")
                {
                    ModelState.AddModelError("SelectedSizeIds", "İçki məhsulları üçün həmişə 3 ölçü seçilməlidir (Small, Medium, Large).");
                }
            }

            if (ModelState.IsValid)
            {
                return BadRequest();
                
            }
            vm.Categories = await _categoryService.GetSelectListAsync();
            vm.Sizes = await _sizeService.GetSelectListAsync();
            vm.CategoryTypesById = await _categoryService.GetCategoryTypesWithIdAsync();
            await _menuProductService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var vm = await _menuProductService.GetDetailAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuProductService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryType(int categoryId)
        {
            var categoryType = await _menuProductService.GetCategoryTypeAsync(categoryId);
            return Json(new { categoryType = categoryType });
        }
    }
}