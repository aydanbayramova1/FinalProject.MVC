using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ContactUs
{
    public class ContactUsEditVM
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(50, ErrorMessage = "Phone number can't be longer than 50 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address containing '@'.")]
        [MaxLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200, ErrorMessage = "Address can't be longer than 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Message title is required.")]
        [MaxLength(60, ErrorMessage = "Message title can't be longer than 60 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Message title must contain only letters and spaces, no digits or symbols.")]
        public string MessageTitle { get; set; }

        [Required(ErrorMessage = "Message description is required.")]
        [MaxLength(500, ErrorMessage = "Message description can't be longer than 500 characters.")]
        public string MessageDescription { get; set; }
    }
}
