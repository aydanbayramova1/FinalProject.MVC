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
            var banners = await _teamBannerService.GetAllAsync();
            var vmList = banners.Select(b => new TeamBannerVM
            {
                Id = b.Id,
                Title = b.Title,
                Img = b.Img
            }).ToList();

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
            if (banner == null)
                throw new KeyNotFoundException($"TeamBanner with ID {id} not found.");

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
            if (banner == null)
                throw new KeyNotFoundException($"TeamBanner with ID {id} not found.");

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
