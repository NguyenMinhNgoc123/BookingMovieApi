using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieCategories : EntitieDate
    {
        public long CategoryId { get; set; }
        
        public long MovieId { get; set; }
        
        public virtual Category Category { get; set; }
    }
}