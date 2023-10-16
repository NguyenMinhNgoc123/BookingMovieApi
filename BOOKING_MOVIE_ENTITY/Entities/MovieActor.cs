using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieActor : EntitieDate
    {
        public long ActorId { get; set; }
        
        public long MovieId { get; set; }
        
        public virtual Actor Actor { get; set; }
    }
}