using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Topbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopbarController : Controller
    {
        private readonly ITopbarService _topbarService;
        private readonly IMapper _mapper;

        public TopbarController(ITopbarService topbarService,IMapper mapper)
        {
            _topbarService = topbarService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var topbars = await _topbarService.GetAllAsync();
            var topbarVMs = _mapper.Map<List<TopbarVM>>(topbars);
            return View(topbarVMs);
        }


        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TopbarCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _topbarService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _topbarService.GetEditAsync(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TopbarEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _topbarService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var vm = await _topbarService.GetDetailAsync(id);
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _topbarService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
