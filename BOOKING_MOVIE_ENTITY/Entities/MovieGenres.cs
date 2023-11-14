using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieGenres : EntitieDate
    {
        public long GenreId { get; set; }
        
        public long MovieId { get; set; }
        
        public virtual Genre Genre { get; set; }
        
        public virtual Movie Movie { get; set; }

    }
}