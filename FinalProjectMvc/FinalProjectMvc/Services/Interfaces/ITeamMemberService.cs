using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.TeamMember;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task<List<TeamMemberVM>> GetAllAsync();
        Task<TeamMember> GetByIdAsync(int id);
        Task CreateAsync(TeamMemberCreateVM vm);
        Task EditAsync(int id, TeamMemberEditVM vm);
        Task DeleteAsync(int id);
    }
}
