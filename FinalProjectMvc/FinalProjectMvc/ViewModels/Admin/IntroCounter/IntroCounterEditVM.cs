using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.IntroCounter
{
    public class IntroCounterEditVM
    {
        public int Id { get; set; }
        public string? ExistingIconPath { get; set; }
        public IFormFile? NewIconFile { get; set; } 
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Count must be a positive number.")]
        public int Count { get; set; }
        public string Suffix { get; set; } = "+";
        [Required]
        public string Description { get; set; }
        [Required]
        public int IntroVideoId { get; set; }
    }
}
