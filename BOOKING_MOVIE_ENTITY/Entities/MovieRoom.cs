using System.Collections.ObjectModel;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieRoom : EntitieDate
    {
        public string Name { get; set; }
        
        public long MovieId { get; set; }
        
        public long RoomId { get; set; }
        
        public virtual Collection<Movie> Movies { get; set; }
    }
}