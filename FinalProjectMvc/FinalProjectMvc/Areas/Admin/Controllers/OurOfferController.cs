﻿using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OurOffer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurOfferController : Controller
    {
        private readonly IOurOfferService _service;

        public OurOfferController(IOurOfferService service)
        {
            _service = service;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync(); 
            return View(model);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _service.GetDetailAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(OurOfferCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _service.GetEditAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(OurOfferEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

    }
}