using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Table
{
    public class TableCreateVM
    {
        [Required(ErrorMessage = "Table number is required.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Location must only contain letters and spaces.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 20, ErrorMessage = "Capacity must be between 1 and 20.")]
        public int Capacity { get; set; }
    }
}
