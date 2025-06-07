using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ContactUs
{
    public class ContactUsCreateVM
    {
        [Required, MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(200)]
        public string Address { get; set; }

        [Required, MaxLength(100)]
        public string MessageTitle { get; set; }

        [Required, MaxLength(500)]
        public string MessageDescription { get; set; }
    }
}
