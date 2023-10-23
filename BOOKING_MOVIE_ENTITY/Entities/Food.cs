using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Food : EntitieDate
    {
        public string Name { get; set; }
        
        public decimal? Price { get; set; }
    }
}