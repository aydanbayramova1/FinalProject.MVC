using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ServiceItem;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceItemController : Controller
    {
        private readonly IServiceItemService _serviceItemService;
        private readonly IMapper _mapper;

        public ServiceItemController(IServiceItemService serviceItemService, IMapper mapper)
        {
            _serviceItemService = serviceItemService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _serviceItemService.GetAllAsync();
            var vms = _mapper.Map<List<ServiceItemVM>>(items);
            return View(vms);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceItemCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _serviceItemService.CreateAsync(vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _serviceItemService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<ServiceItemEditVM>(entity);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceItemEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _serviceItemService.EditAsync(vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _serviceItemService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var vm = _mapper.Map<ServiceItemDetailVM>(entity);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceItemService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
