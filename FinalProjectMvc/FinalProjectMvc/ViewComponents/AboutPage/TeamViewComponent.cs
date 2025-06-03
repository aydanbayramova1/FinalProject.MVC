using AutoMapper.Execution;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.AboutPage
{
    public class TeamViewComponent : ViewComponent
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamViewComponent(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teamMembers = await _teamMemberService.GetAllAsync();

            if (teamMembers == null || !teamMembers.Any())
            {
                return Content("");
            }

            var lastThree = teamMembers
            .OrderByDescending(m => m.Id)
             .Take(3)
            .ToList();

            return View(lastThree);
        }
    }
}
