using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SliderController(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var sliderVMs = _mapper.Map<IEnumerable<SliderVM>>(sliders);
            return View(sliderVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            var sliderDetail = _mapper.Map<SliderDetailVM>(slider);
            return View(sliderDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM model)
        {
            if (!ModelState.IsValid)return View(model);
            await _sliderService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            var sliderEditVM = new SliderEditVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Subtitle = slider.Subtitle,
                Description = slider.Description,
                Img = slider.Img
            };
            return View(sliderEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderEditVM model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            await _sliderService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            await _sliderService.DeleteAsync(slider);
            return RedirectToAction(nameof(Index));
        }
    }
}
