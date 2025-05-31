using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Topbar
{
    public class TopbarEditVM
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address with '@'.")]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
