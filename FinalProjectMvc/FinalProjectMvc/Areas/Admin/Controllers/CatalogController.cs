using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;

        public CatalogController(ICatalogService catalogService, IMapper mapper)
        {
            _catalogService = catalogService;
            _mapper = mapper;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var entities = await _catalogService.GetAllAsync();
            var vms = _mapper.Map<List<CatalogVM>>(entities);
            return View(vms);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var catalog = await _catalogService.GetByIdAsync(id);
            if (catalog == null) return NotFound();

            var vm = _mapper.Map<CatalogDetailVM>(catalog);
            return View(vm);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(CatalogCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _catalogService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var catalog = await _catalogService.GetByIdAsync(id);
            if (catalog == null) return NotFound();

            var model = _mapper.Map<CatalogEditVM>(catalog);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(CatalogEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _catalogService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _catalogService.DeleteAsync(id);
            return Ok(); 
        }

    }
}
