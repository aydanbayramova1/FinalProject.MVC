using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Home
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SliderViewComponent(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _sliderService.GetAllAsync();
            var sliderVMs = _mapper.Map<IEnumerable<SliderVM>>(sliders);
            return View(sliderVMs);
        }
    }
}