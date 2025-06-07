using FinalProjectMvc.Data;
using FinalProjectMvc.ViewModels.Admin.Approach;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using FinalProjectMvc.ViewModels.Admin.OurStory;
using FinalProjectMvc.ViewModels.Admin.StoryItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.ViewComponents.AboutPage
{
    public class ApproachViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ApproachViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var approach = await _context.Approaches
                .Include(a => a.Items)
                .FirstOrDefaultAsync();


            if (approach == null || !approach.Items.Any())
            {
                return Content("");
            }

            var vm = new ApproachVM
            {
                Title = approach.Title,
                SubTitle = approach.SubTitle,
                Image = approach.Image,
                Items = approach.Items.Select(i => new ApproachItemVM
                {
                    Title = i.Title,
                    Description = i.Description,
                    IconImg = i.IconPath,
                }).ToList()
            };

            return View(vm);
        }
    }
}
