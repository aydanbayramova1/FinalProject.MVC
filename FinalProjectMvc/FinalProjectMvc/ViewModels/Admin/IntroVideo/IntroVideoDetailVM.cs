using FinalProjectMvc.ViewModels.Admin.IntroCounter;

namespace FinalProjectMvc.ViewModels.Admin.IntroVideo
{
    public class IntroVideoDetailVM
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Img { get; set; } 

        public string VideoUrl { get; set; }

        public List<IntroCounterDetailVM> Counters { get; set; }
    }
}
