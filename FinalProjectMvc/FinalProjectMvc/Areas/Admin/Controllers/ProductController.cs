using AutoMapper;
using FinalProjectMvc.Helpers;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;
using FinalProjectMvc.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISizeService _sizeService;
        private readonly IEmailService _emailService;
        private readonly ISubscribeService _subscribeService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            ISizeService sizeService,
            IEmailService emailService,
            ISubscribeService subscribeService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sizeService = sizeService;
            _emailService = emailService;
            _subscribeService = subscribeService;
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index(int page = 1, string search = "", int pageSize = 10)
        {
            var query = await _productService.GetAllQueryAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string normalizedSearch = search.Trim().ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(normalizedSearch) ||
                    p.Ingredients.ToLower().Contains(normalizedSearch));
            }

            query = query.OrderByDescending(p => p.CreateDate); 

            var pagedResult = query.ToPagedResult(page, pageSize);

            ViewBag.CurrentSearch = search;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductTablePartial", pagedResult);
            }

            return View(pagedResult);
        }



      [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var vm = await _productService.GetCreateVMAsync();
            return View(vm);
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {

            if (!await _productService.ValidateSizeCountForCategoryType1Async(vm.CategoryId, vm.SelectedSizeIds))
            {
                ModelState.AddModelError("SelectedSizeIds", "Bu kateqoriya üçün 3 ölçü (Small, Medium, Large) seçilməlidir.");
            }

            if (ModelState.IsValid)
            {
                var existingProduct = await _productService.GetByNameAsync(vm.Name);
                if (existingProduct != null)
                {
                    vm.Categories = await _categoryService.GetSelectListAsync();
                    vm.Sizes = await _sizeService.GetSelectListAsync();
                    vm.CategoryTypesById = await _categoryService.GetCategoryTypesWithIdAsync();
                    return View(vm);
                }

                var createdProduct = await _productService.CreateAsync(vm);

                var subscribers = await _subscribeService.GetAllAsync();



                string subject = "A New Flavor Has Arrived at Caffe Luna!";

                string menuLink = "https://localhost:7117/Menu"; 

                string html = $@"
  <div style='font-family: Arial, sans-serif;background-color: rgba(228, 204, 180, 0.2);color: #3e2723;padding: 30px;border-radius: 10px;max-width: 600px;margin: auto;box-shadow: 0 4px 12px rgba(0,0,0,0.1);'>
    <h2 style='color: #6d4c41;'>☕ A Delicious New Arrival!</h2>
    <p style='font-size: 18px;'>We’ve just added a brand new delight to the Caffe Luna menu: <strong>{createdProduct.Name}</strong></p>
    <p style='font-size: 16px; line-height: 1.6;'>
      <em>{createdProduct.Ingredients}</em>
    </p>

    <div style='text-align: center; margin-top: 25px;'>
      <a href='{menuLink}' style='
        background-color: #8d6e63;
        color: white;
        padding: 12px 25px;
        text-decoration: none;
        border-radius: 25px;
        font-size: 16px;
        font-weight: bold;
      '>
        View Our Menu
      </a>
    </div>

    <p style='margin-top: 30px; font-size: 14px; color: #8d6e63;'>
      Come and discover your new favorite flavor at Caffe Luna 💫
    </p>

    <p style='margin-top: 40px; font-size: 16px;'>
      Best regards,<br />
      <strong>Aydan Bayramova</strong><br />
      <em>Owner, Caffe Luna</em>
    </p>
  </div>";

                foreach (var subscriber in subscribers)
                {
                    await _emailService.SendAsync(subscriber.Email, subject, html);
                }

                return RedirectToAction(nameof(Index));
            }

            vm.Categories = await _categoryService.GetSelectListAsync();
            vm.Sizes = await _sizeService.GetSelectListAsync();
            vm.CategoryTypesById = await _categoryService.GetCategoryTypesWithIdAsync();
            return View(vm);
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _productService.GetEditVMAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
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
                var existingProduct = await _productService.GetByNameAsync(vm.Name);
                if (existingProduct != null && existingProduct.Id != vm.Id)
                {
                    ViewData["Error"] = "A product with the same name already exists.";

                    vm.Categories = await _categoryService.GetSelectListAsync();
                    vm.Sizes = await _sizeService.GetSelectListAsync();
                    vm.CategoryTypesById = await _categoryService.GetCategoryTypesWithIdAsync();
                    return View(vm);
                }

                await _productService.EditAsync(vm);
                return RedirectToAction(nameof(Index));
            }

            vm.Categories = await _categoryService.GetSelectListAsync();
            vm.Sizes = await _sizeService.GetSelectListAsync();
            vm.CategoryTypesById = await _categoryService.GetCategoryTypesWithIdAsync();
            return View(vm);
        }





        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var vm = await _productService.GetDetailAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
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

        [HttpGet]
        public async Task<IActionResult> CheckProductName(string name)
        {
            var product = await _productService.GetByNameAsync(name);
            return Json(product != null);
        }
    }
}