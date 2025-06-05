using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using System.ComponentModel.DataAnnotations;

public class ApproachEditVM
{
    public int Id { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title can only contain letters.")]
    public string Title { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Subtitle can only contain letters.")]
    public string SubTitle { get; set; }

    public string Image { get; set; }
    public IFormFile? ImageFile { get; set; }  

    public List<ApproachItemEditVM> Items { get; set; }
}
