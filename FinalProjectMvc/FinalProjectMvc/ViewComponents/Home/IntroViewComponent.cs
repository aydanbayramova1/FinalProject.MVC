using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProjectMvc.ViewModels.Admin.IntroVideo;
using FinalProjectMvc.ViewModels.Admin.IntroCounter;
using AutoMapper;
using FinalProjectMvc.Models;

namespace FinalProjectMvc.ViewComponents.Home
{
    public class IntroViewComponent : ViewComponent
    {
        private readonly IIntroVideoService _introService;
        private readonly IMapper _mapper;

        public IntroViewComponent(IIntroVideoService introService, IMapper mapper)
        {
            _introService = introService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var introVideoVM = await _introService.GetAsync(); // ViewModel
            var introVideoModel = _mapper.Map<IntroVideo>(introVideoVM); // Mapping to Model
            return View(introVideoModel); // now model type matches Default.cshtml
        }
    }

}
