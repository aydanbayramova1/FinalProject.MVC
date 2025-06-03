using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutRestaurant;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Home
{
    public class RestaurantViewComponent : ViewComponent
    {
        private readonly IAboutRestaurantService _aboutRestaurantService;
        private readonly IMapper _mapper;

        public RestaurantViewComponent(IAboutRestaurantService aboutRestaurantService, IMapper mapper)
        {
            _aboutRestaurantService = aboutRestaurantService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var aboutList = await _aboutRestaurantService.GetAllAsync();
            if (aboutList == null || !aboutList.Any()) return Content("");

            var aboutVM = _mapper.Map<AboutRestaurantVM>(aboutList.First());
            return View(aboutVM);

        }
    }
}
