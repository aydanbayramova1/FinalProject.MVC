using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.IntroCounter;
using FinalProjectMvc.ViewModels.Admin.IntroVideo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IntroVideoController : Controller
    {
        private readonly IIntroVideoService _introVideoService;

        public IntroVideoController(IIntroVideoService introVideoService)
        {
            _introVideoService = introVideoService;
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var video = await _introVideoService.GetAsync();

            if (video == null) return View(null);

            var vm = new IntroVideoVM
            {
                Id = video.Id,
                Title = video.Title,
                Subtitle = video.Subtitle,
                Img = video.Img,
                VideoUrl = video.VideoUrl,
                Counters = video.Counters?.Select(c => new IntroCounterVM
                {
                    IconPath = c.IconPath,
                    Count = c.Count,
                    Suffix = c.Suffix,
                    Description = c.Description
                }).ToList()
            };

            return View(vm);
        }



        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var existing = await _introVideoService.GetAsync();
            if (existing != null) return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(IntroVideoCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _introVideoService.CreateAsync(vm);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _introVideoService.GetEditVMAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(IntroVideoEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _introVideoService.EditAsync(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _introVideoService.DeleteAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var video = await _introVideoService.GetAsync();
            if (video == null || video.Id != id) return NotFound();

            var vm = new IntroVideoDetailVM
            {
                Title = video.Title,
                Subtitle = video.Subtitle,
                Img = video.Img,
                VideoUrl = video.VideoUrl,
                Counters = video.Counters?.Select(c => new IntroCounterDetailVM
                {
                    IconPath = c.IconPath,
                    Count = c.Count,
                    Suffix = c.Suffix,
                    Description = c.Description
                }).ToList()
            };

            return View(vm);
        }

    }
}
