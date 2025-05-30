using Microsoft.AspNetCore.Identity;

namespace FinalProjectMvc.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
