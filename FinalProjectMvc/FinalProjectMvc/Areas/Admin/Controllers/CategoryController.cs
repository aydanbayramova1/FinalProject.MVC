using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryTypeService _categoryTypeService;

        public CategoryController(ICategoryService categoryService, ICategoryTypeService categoryTypeService)
        {
            _categoryService = categoryService;
            _categoryTypeService = categoryTypeService;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var data = await _categoryService.GetAllAsync();
            return View(data);
        }



        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var types = await _categoryTypeService.GetAllAsync();
            var vm = new CategoryCreateVM
            {
                CategoryTypes = types
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CategoryTypes = await _categoryTypeService.GetAllAsync();
                return View(vm);
            }

            try
            {
                await _categoryService.CreateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                vm.CategoryTypes = await _categoryTypeService.GetAllAsync();
                return View(vm);
            }
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dbCategory = await _categoryService.GetEntityByIdAsync(id);
            if (dbCategory == null) return NotFound();

            var vm = new CategoryEditVM
            {
                Id = dbCategory.Id,
                Name = dbCategory.Name,
                CategoryTypeId = dbCategory.CategoryTypeId,
                ExistingImage = dbCategory.Image,
                CategoryTypes = await _categoryTypeService.GetAllAsync()
            };

            return View(vm);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(CategoryEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CategoryTypes = await _categoryTypeService.GetAllAsync();
                return View(vm);
            }

            try
            {
                await _categoryService.UpdateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                vm.CategoryTypes = await _categoryTypeService.GetAllAsync();
                return View(vm);
            }
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok();
        }

    }
}
