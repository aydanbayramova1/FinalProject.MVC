using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutUs;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using Microsoft.AspNetCore.Mvc;

public class AboutViewComponent : ViewComponent
{
    private readonly IAboutUsService _aboutUsService;

    public AboutViewComponent(IAboutUsService aboutUsService)
    {
        _aboutUsService = aboutUsService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var aboutUsEntity = await _aboutUsService.GetAsync();

        if (aboutUsEntity == null || aboutUsEntity.AboutUsItems == null || !aboutUsEntity.AboutUsItems.Any())
            return Content("");

        var model = new AboutUsVM
        {
            Title = aboutUsEntity.Title,
            Subtitle = aboutUsEntity.Subtitle,
            Description = aboutUsEntity.Description,
            VideoUrl = aboutUsEntity.VideoUrl,
            ImageUrl = aboutUsEntity.ImageUrl,
            OpeningTimeTitle = aboutUsEntity.OpeningTimeTitle,

            AboutUsItems = aboutUsEntity.AboutUsItems
                .TakeLast(2)
                .Select(item => new AboutUsItemVM
                {
                    Title = item.Title,
                    Description = item.Description,
                    IconUrl = item.IconUrl
                }).ToList(),

            OpeningHours = aboutUsEntity.OpeningHours?
                .Select(oh => new OpeningHourVM
                {
                    DayRange = oh.DayRange,
                    TimeRange = oh.TimeRange
                }).ToList()
        };

        return View(model);
    }
}
