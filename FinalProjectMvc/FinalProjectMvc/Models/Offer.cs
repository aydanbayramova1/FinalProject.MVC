namespace FinalProjectMvc.Models
{
    public class Offer : BaseEntity
    {
        public string MainTitle { get; set; }       
        public string Subtitle { get; set; }          
        public string Description { get; set; }       
        public string MainImage { get; set; }
        public string DecorativeImageLeft { get; set; }
        public string DecorativeImageRight { get; set; }

        public ICollection<OfferItem> OfferItems { get; set; }
    }
}
