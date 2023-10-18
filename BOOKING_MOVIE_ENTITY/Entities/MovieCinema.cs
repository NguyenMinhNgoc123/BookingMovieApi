using System.Collections.Generic;
using System.Runtime.Serialization;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieCinema : EntitieDate
    {
        public string Name { get; set; }
        
        public long CinemaId { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        public virtual ICollection<MovieRoom> MovieRooms { get; set; }
    }
}