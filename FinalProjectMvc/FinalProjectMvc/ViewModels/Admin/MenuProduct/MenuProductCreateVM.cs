using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.MenuProduct
{
    public class MenuProductCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        [Range(0.01, 9999)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<SelectListItem>? Categories { get; set; }

        public List<int>? SelectedSizeIds { get; set; }

        public List<SelectListItem>? Sizes { get; set; }
        public Dictionary<int, string> CategoryTypesById { get; set; }
    }
}
