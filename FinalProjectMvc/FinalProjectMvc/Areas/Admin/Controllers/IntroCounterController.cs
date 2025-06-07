using AutoMapper;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.IntroCounter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IntroCounterController : Controller
    {
        private readonly IIntroCounterService _service;
        private readonly IIntroVideoService _videoService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public IntroCounterController(IIntroCounterService service, IIntroVideoService videoService, IWebHostEnvironment env, IMapper mapper)
        {
            _service = service;
            _videoService = videoService;
            _env = env;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var counters = await _service.GetAllAsync();
            var vmList = _mapper.Map<List<IntroCounterVM>>(counters);
            return View(vmList);
        }

        public async Task<IActionResult> Create()
        {
            var videos = await _videoService.GetAllAsync();
            ViewBag.IntroVideos = new SelectList(videos, "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IntroCounterCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var videos = await _videoService.GetAllAsync();
            var firstVideo = videos.FirstOrDefault();
            if (firstVideo == null)
            {
                ModelState.AddModelError("", "Əvvəlcə bir Intro Video əlavə etməlisiniz.");
                return View(vm);
            }

            string iconPath = await SaveFileAsync(vm.IconFile);

            var counter = _mapper.Map<IntroCounter>(vm);
            counter.IconPath = iconPath;
            counter.IntroVideoId = firstVideo.Id;

            await _service.CreateAsync(counter);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var counter = await _service.GetByIdAsync(id);
            if (counter == null) return NotFound();

            var vm = _mapper.Map<IntroCounterEditVM>(counter);
            vm.ExistingIconPath = counter.IconPath;

            var videos = await _videoService.GetAllAsync();
            ViewBag.IntroVideos = new SelectList(videos, "Id", "Title", counter.IntroVideoId);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IntroCounterEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                var videos = await _videoService.GetAllAsync();
                ViewBag.IntroVideos = new SelectList(videos, "Id", "Title", vm.IntroVideoId);
                return View(vm);
            }

            var counter = await _service.GetByIdAsync(vm.Id);
            if (counter == null) return NotFound();

            _mapper.Map(vm, counter);

            if (vm.NewIconFile != null)
            {
                string newPath = await SaveFileAsync(vm.NewIconFile);
                counter.IconPath = newPath;
            }

            await _service.UpdateAsync(counter);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            string folder = Path.Combine(_env.WebRootPath, "uploads", "intro");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("uploads", "intro", fileName);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var counter = await _service.GetByIdAsync(id);
            if (counter == null) return NotFound();

            var vm = _mapper.Map<IntroCounterVM>(counter);
            return View(vm);
        }
    }

}
