using FinalProjectMvc.Helpers;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using System.ComponentModel.DataAnnotations;

public class ApproachEditVM
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title can only contain letters and spaces.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Subtitle is required.")]
    [MaxLength(90, ErrorMessage = "Subtitle cannot exceed 90 characters.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Subtitle can only contain letters and spaces.")]
    public string SubTitle { get; set; }

    public string? Image { get; set; }

    [MaxFileSize(3 * 1024 * 1024)] 
    public IFormFile? ImageFile { get; set; }
}