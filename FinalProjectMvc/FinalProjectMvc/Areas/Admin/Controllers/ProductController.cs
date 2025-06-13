using AutoMapper;
using FinalProjectMvc.Helpers;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISizeService _sizeService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            ISizeService sizeService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sizeService = sizeService;
        }


        public async Task<IActionResult> Index(int page = 1, string search = "", int pageSize = 10)
        {
            var query = await _productService.GetAllQueryAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Ingredients.Contains(search));
            }

            query = query.OrderBy(p => p.Name);
            var pagedResult = query.ToPagedResult(page, pageSize);

            ViewBag.CurrentSearch = search;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductTablePartial", pagedResult);
            }

            return View(pagedResult);
        }


        public async Task<IActionResult> Create()
        {
            var vm = await _productService.GetCreateVMAsync();
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            vm.Categories = await _categoryService.GetSelectListAsync();
            vm.Sizes = await _sizeService.GetSelectListAsync();
            vm.CategoryTypesById = await _categoryService.GetCategoryTypesWithIdAsync();
            
            await _productService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _productService.GetEditVMAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditVM vm)
        {
            if (!await _productService.ValidateProductAsync(vm))
            {
                var categoryType = await _productService.GetCategoryTypeAsync(vm.CategoryId);
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
            await _productService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Detail(int id)
        {
            var vm = await _productService.GetDetailAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetCategoryType(int categoryId)
        {
            var categoryType = await _productService.GetCategoryTypeAsync(categoryId);
            return Json(new { categoryType = categoryType });
        }
    }
}