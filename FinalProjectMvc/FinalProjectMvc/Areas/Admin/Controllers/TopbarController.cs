using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Topbar;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var topbars = await _topbarService.GetAllAsync();
            var topbarVMs = _mapper.Map<List<TopbarVM>>(topbars);
            return View(topbarVMs);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(TopbarCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _topbarService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _topbarService.GetEditAsync(id);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(TopbarEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _topbarService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var vm = await _topbarService.GetDetailAsync(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _topbarService.DeleteAsync(id);
            return Ok();
        }

    }
}
