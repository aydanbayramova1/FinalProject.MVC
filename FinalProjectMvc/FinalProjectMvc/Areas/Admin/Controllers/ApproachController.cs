using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Approach;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApproachController : Controller
    {
        private readonly IApproachService _approachService;

        public ApproachController(IApproachService approachService)
        {
            _approachService = approachService;
        }

        public async Task<IActionResult> Index()
        {
            // Tüm ApproachVM öğelerini almak için servisinizden veri çekin
            var approachList = await _approachService.GetAsync(); // Bu metod IEnumerable<ApproachVM> döndürmelidir.

            return View(approachList); // Listeyi view'a geçirin
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ApproachCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _approachService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _approachService.GetByIdAsync(id);
            if (model == null) return NotFound();

            var editVM = new ApproachEditVM
            {
                Id = model.Id,
                Title = model.Title,
                SubTitle = model.SubTitle,
                Image = model.Image
            };

            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ApproachEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _approachService.UpdateAsync(id, vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _approachService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _approachService.GetByIdAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

    }
}
