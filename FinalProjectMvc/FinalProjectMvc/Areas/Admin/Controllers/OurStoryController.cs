using AutoMapper;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OurStory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurStoryController : Controller
    {
        private readonly IOurStoryService _ourStoryService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public OurStoryController(IOurStoryService ourStoryService, IMapper mapper, IWebHostEnvironment env)
        {
            _ourStoryService = ourStoryService;
            _mapper = mapper;
            _env = env;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var entity = await _ourStoryService.GetAsync();
            ViewBag.Exists = entity != null;
            if (entity == null) return View(null);

            var vm = _mapper.Map<OurStoryVM>(entity);
            return View(vm);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            if (await _ourStoryService.ExistsAsync())
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(OurStoryCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string fileName = await SaveFileAsync(vm.ImageFile);

            var entity = _mapper.Map<OurStory>(vm);
            entity.Image = fileName;

            await _ourStoryService.CreateAsync(entity);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _ourStoryService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<OurStoryEditVM>(entity);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(OurStoryEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var entity = await _ourStoryService.GetByIdAsync(vm.Id);
            if (entity == null) return NotFound();

            if (vm.ImageFile != null)
            {
                string newFile = await SaveFileAsync(vm.ImageFile);
                entity.Image = newFile;
            }

            _mapper.Map(vm, entity);
            await _ourStoryService.UpdateAsync(entity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ourStoryService.DeleteAsync(id);
            return Ok();
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _ourStoryService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<OurStoryDetailVM>(entity);
            return View(vm);
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var savePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "uploads/" + fileName;
        }
    }
}
