using System;
using System.Collections.ObjectModel;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieDateSetting : EntitieDate
    {
        public DateTime Time { get; set; }
        
        public long MovieId { get; set; }
        
        public virtual Collection<Movie> Movies { get; set; }
    }
}