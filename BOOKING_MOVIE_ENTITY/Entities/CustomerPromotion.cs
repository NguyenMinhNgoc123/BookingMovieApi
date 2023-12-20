using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class CustomerPromotion : EntitieDate
    {
        public long CustomerId { get; set; }
        public long PromotionId { get; set; }
        
        public virtual Promotion Promotion { get; set; }
        
        public virtual Customer Customer { get; set; }
    }
}