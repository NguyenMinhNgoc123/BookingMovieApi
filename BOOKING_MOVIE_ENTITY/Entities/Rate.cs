using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Rate : EntitieDate
    {
        public decimal Rating { get; set; }
        
        public string Note { get; set; }
        
        public long MovieId { get; set; }
        
        public long CustomerId { get; set; }
    }
}