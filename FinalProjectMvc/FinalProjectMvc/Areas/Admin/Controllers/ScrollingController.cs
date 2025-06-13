using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Scrolling;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ScrollingController : Controller
    {
        private readonly IScrollingService _scrollingService;
        private readonly IMapper _mapper;

        public ScrollingController(IScrollingService scrollingService, IMapper mapper)
        {
            _scrollingService = scrollingService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var scrollings = await _scrollingService.GetAllAsync();
            var vms = _mapper.Map<IEnumerable<ScrollingVM>>(scrollings);
            return View(vms);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var scrolling = await _scrollingService.GetByIdAsync(id);
            var vm = _mapper.Map<ScrollingDetailVM>(scrolling);
            return View(vm);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScrollingCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _scrollingService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var scrolling = await _scrollingService.GetByIdAsync(id);
            var vm = new ScrollingEditVM
            {
                Name = scrolling.Name,
                Img = scrolling.Img
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ScrollingEditVM model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            await _scrollingService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var scrolling = await _scrollingService.GetByIdAsync(id);
            if (scrolling == null)
            {
                return NotFound();
            }

            await _scrollingService.DeleteAsync(scrolling.Id);
            return Ok();
        }


    }
}
