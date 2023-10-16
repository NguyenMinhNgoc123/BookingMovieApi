using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Photo : EntitieDate
    {
        public string url { get; set; }
        
        public string Type { get; set; }

        public long? ObjectId { get; set; }
    }
}