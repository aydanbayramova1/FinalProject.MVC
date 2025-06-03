using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutRestaurant;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutRestaurantController : Controller
    {
        private readonly IAboutRestaurantService _aboutService;
        private readonly IMapper _mapper;

        public AboutRestaurantController(IAboutRestaurantService aboutService, IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _aboutService.GetAllAsync();
            var model = _mapper.Map<List<AboutRestaurantVM>>(entities);
            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutRestaurantCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _aboutService.CreateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _aboutService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<AboutRestaurantEditVM>(entity);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AboutRestaurantEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _aboutService.UpdateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _aboutService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<AboutRestaurantVM>(entity);
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _aboutService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _aboutService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<AboutRestaurantDetailVM>(entity);
            return View(vm);
        }
    }
}