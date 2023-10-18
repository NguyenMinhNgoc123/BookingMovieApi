using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieDateSetting : EntitieDate
    {
        public DateTime Time { get; set; }
        public virtual ICollection<MovieCinema> MovieCinemas { get; set; }
    }
}