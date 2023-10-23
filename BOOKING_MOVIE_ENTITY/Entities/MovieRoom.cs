using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using BOOKING_MOVIE_ENTITY.EntitieBases;
using Newtonsoft.Json;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieRoom : EntitieDate
    {
        public long RoomId { get; set; }
        
        public long MovieCinemaId { get; set; }
        
        public virtual MovieCinema MovieCinema { get; set; }
        
        public virtual Room Room { get; set; }
        public virtual ICollection<MovieTimeSetting> MovieTimeSettings { get; set; }
    }
}