using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.IntroCounter;

namespace FinalProjectMvc.ViewModels.Admin.IntroVideo
{
    public class IntroVideoVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Img { get; set; }

        public string VideoUrl { get; set; }

        public List<IntroCounterVM> Counters { get; set; }
    }
}
