using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.TeamMember
{
    public class TeamMemberVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Desc { get; set; }
        public string Position { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
