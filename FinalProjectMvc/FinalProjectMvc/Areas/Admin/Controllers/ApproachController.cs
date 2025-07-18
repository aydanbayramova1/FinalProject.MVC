﻿using AutoMapper;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Approach;
using FinalProjectMvc.ViewModels.Admin.OurStory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApproachController : Controller
    {
        private readonly IApproachService _approachService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ApproachController(IApproachService approachService, IMapper mapper, IWebHostEnvironment env)
        {
            _approachService = approachService;
            _mapper = mapper;
            _env = env;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var entity = await _approachService.GetAsync();
            ViewBag.Exists = entity != null;
            if (entity == null) return View(null);

            var vm = _mapper.Map<ApproachVM>(entity);
            return View(vm);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            if (await _approachService.ExistsAsync())
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(ApproachCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string fileName = await SaveFileAsync(vm.ImageFile);

            var entity = _mapper.Map<Approach>(vm);
            entity.Image = fileName;

            await _approachService.CreateAsync(entity);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _approachService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<ApproachEditVM>(entity);
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(ApproachEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var entity = await _approachService.GetByIdAsync(vm.Id);
            if (entity == null) return NotFound();

            _mapper.Map(vm, entity);

            if (vm.ImageFile != null)
            {
                string newFile = await SaveFileAsync(vm.ImageFile);
                entity.Image = newFile; 
            }

            await _approachService.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _approachService.DeleteAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _approachService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<ApproachDetailVM>(entity);
            return View(vm);
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var savePath = Path.Combine(_env.WebRootPath, "uploads","approach", fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "uploads/approach/" + fileName;
        }
    }
}
