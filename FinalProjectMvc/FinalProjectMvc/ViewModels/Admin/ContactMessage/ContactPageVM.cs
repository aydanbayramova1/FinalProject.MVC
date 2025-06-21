using FinalProjectMvc.ViewModels.Admin.ContactUs;
using FinalProjectMvc.ViewModels.Admin.Subscribe;

namespace FinalProjectMvc.ViewModels.Admin.ContactMessage
{
    public class ContactPageVM
    {
        public ContactUsVM ContactInfo { get; set; }
        public ContactMessageCreateVM MessageForm { get; set; }
        public SubscribeVM Subscribe { get; set; }
    }
}
