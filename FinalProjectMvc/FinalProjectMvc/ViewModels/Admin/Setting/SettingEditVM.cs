using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Setting
{
    public class SettingEditVM
    {
            public int Id { get; set; }
            public string HeaderLogo { get; set; }
            public string FooterLogo { get; set; }
            public string BackgroundImage { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string EverydayWorkingHours { get; set; }
            public string KitchenCloseTime { get; set; }
            public IFormFile HeaderLogoFile { get; set; }
            public IFormFile FooterLogoFile { get; set; }
            public IFormFile BackgroundImageFile { get; set; }       
    }
}
