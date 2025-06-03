using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Team
{
    public class TeamSectionViewComponent : ViewComponent
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamSectionViewComponent(ITeamMemberService teamMemberService)
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

            return View(teamMembers);
        }
    }
}
