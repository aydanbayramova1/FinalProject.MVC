using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutUs;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutUsController : Controller
    {
        private readonly IAboutUsService _aboutUsService;
        private readonly IMapper _mapper;

        public AboutUsController(IAboutUsService aboutUsService, IMapper mapper)
        {
            _aboutUsService = aboutUsService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var aboutUs = await _aboutUsService.GetAsync();
            if (aboutUs == null)
                return View(null); 
            var vm = _mapper.Map<AboutUsVM>(aboutUs);
            return View(vm);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            if (await _aboutUsService.HasAnyAsync())
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(AboutUsCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _aboutUsService.CreateAsync(vm);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _aboutUsService.GetEditAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(AboutUsEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _aboutUsService.UpdateAsync(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _aboutUsService.DeleteAsync(id);
                return Ok(); 
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var aboutUs = await _aboutUsService.GetByIdAsync(id);
            if (aboutUs == null) return NotFound();

            var vm = _mapper.Map<AboutUsDetailVM>(aboutUs);
            return View(vm);
        }



    }
}
