using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieDirector : EntitieDate
    {
        public long DirectorId { get; set; }
        
        public long MovieId { get; set; }
        
        public virtual Director Director { get; set; }
    }
}