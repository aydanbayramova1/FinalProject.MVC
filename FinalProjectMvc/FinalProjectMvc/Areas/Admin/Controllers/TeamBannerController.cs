using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.TeamBanner;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamBannerController : Controller
    {
        private readonly ITeamBannerService _teamBannerService;

        public TeamBannerController(ITeamBannerService teamBannerService)
        {
            _teamBannerService = teamBannerService;
        }

        public async Task<IActionResult> Index()
        {
            var vmList = await _teamBannerService.GetAllAsync();
            ViewBag.CanCreate = vmList.Count == 0;
            return View(vmList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamBannerCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _teamBannerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _teamBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new TeamBannerEditVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeamBannerEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _teamBannerService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _teamBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new TeamBannerDetailVM
            {
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamBannerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
