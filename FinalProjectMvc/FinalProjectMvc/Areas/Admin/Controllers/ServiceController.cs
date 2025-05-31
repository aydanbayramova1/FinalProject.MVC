using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Service;
using FinalProjectMvc.ViewModels.Admin.ServiceItem;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var service = await _serviceService.GetAsync();
            if (service == null) return View(null); 

            var vm = _mapper.Map<ServiceVM>(service);
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var service = await _serviceService.GetAsync(); 
            if (service == null) return NotFound();

            var vm = new ServiceItemCreateVM
            {
                ServiceId = service.Id
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _serviceService.CreateAsync(vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null) return NotFound();

            var vm = _mapper.Map<ServiceEditVM>(service);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _serviceService.EditAsync(vm);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null) return NotFound();

            var vm = _mapper.Map<ServiceDetailVM>(service);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
