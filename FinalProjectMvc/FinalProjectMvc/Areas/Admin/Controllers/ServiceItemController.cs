﻿using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ServiceItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceItemController : Controller
    {
        private readonly IServiceItemService _serviceItemService;
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServiceItemController(IServiceItemService serviceItemService, IServiceService serviceService, IMapper mapper)
        {
            _serviceItemService = serviceItemService;
            _serviceService = serviceService;
            _mapper = mapper;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var items = await _serviceItemService.GetAllAsync();
            var vms = _mapper.Map<List<ServiceItemVM>>(items);
            return View(vms);
        }


        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var service = await _serviceService.GetAsync();

            if (service == null)
            {
                TempData["Error"] = "The main Service must be created first.";
                return RedirectToAction("Index", "Service");
            }

            var vm = new ServiceItemCreateVM
            {
                ServiceId = service.Id
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(ServiceItemCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
               
                var service = await _serviceService.GetAsync();
                if (service != null) vm.ServiceId = service.Id;

                return View(vm);
            }

            await _serviceItemService.CreateAsync(vm);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _serviceItemService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<ServiceItemEditVM>(entity);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(ServiceItemEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _serviceItemService.EditAsync(vm);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _serviceItemService.GetByIdWithServiceAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<ServiceItemDetailVM>(entity);
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceItemService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
