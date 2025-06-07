using FinalProjectMvc.Data;
using FinalProjectMvc.ViewModels.Admin.OurStory;
using FinalProjectMvc.ViewModels.Admin.StoryItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.ViewComponents.AboutPage
{
    public class StoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public StoryViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var story = await _context.OurStories
                .Include(o => o.StoryItems)
                .FirstOrDefaultAsync();

  
            if (story == null || !story.StoryItems.Any())
            {
                return Content(""); 
            }

            var vm = new OurStoryVM
            {
                SectionTitle = story.SectionTitle,
                SectionSubtitle = story.SectionSubtitle,
                SectionDescription = story.SectionDescription,
                Image = story.Image,
                StoryItems = story.StoryItems.Select(i => new StoryItemVM
                {
                    Title = i.Title,
                    Description = i.Description,
                    IconImg = i.IconImg
                }).ToList()
            };

            return View(vm);
        }
    }

}
