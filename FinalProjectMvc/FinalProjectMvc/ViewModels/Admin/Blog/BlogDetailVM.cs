namespace FinalProjectMvc.ViewModels.Admin.Blog
{
    public class BlogDetailVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
