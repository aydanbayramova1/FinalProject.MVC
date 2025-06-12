namespace FinalProjectMvc.Models
{
    public class MenuProductSize : BaseEntity
    {
        public int MenuProductId { get; set; }
        public MenuProduct MenuProduct { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
    }
}
