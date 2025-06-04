namespace FinalProjectMvc.Models
{
    public class OfferItem :BaseEntity
    {
        public string Title { get; set; }             
        public string Content { get; set; }    
        public int OfferId { get; set; }   
        public Offer Offer { get; set; }
    }
}
