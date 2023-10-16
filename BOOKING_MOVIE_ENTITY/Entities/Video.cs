using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Video : EntitieDate
    {
        public string url { get; set; }
        
        public string Type { get; set; }

        public long? ObjectId { get; set; }
    }
}