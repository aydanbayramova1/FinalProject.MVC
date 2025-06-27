namespace FinalProjectMvc.ViewModels.Admin.User
{
    public class ManageUserRolesVM
    {
        public List<string> Roles { get; set; } = new List<string>();
        public List<string> Usernames { get; set; } = new();
    }
}
