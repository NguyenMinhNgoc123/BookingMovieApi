using System;
using System.Collections.Generic;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_ADMIN.Values
{
    public class TrendingAllDayDto
    {
        public string Name { get; set; }
        
        public string MovieStatus { get; set; }
        
        public string YearOfRelease { get; set; }
        
        public string Time { get; set; }
        
        public string Country { get; set; }
        
        public string Rate { get; set; }
        
        public string Description { get; set; }
        
        public DateTime ReleaseDate { get; set; }

        public DateTime PremiereDate { get; set; }
        
        public virtual List<long> GenreIds { get; set; }
        
        public virtual ICollection<MovieGenres> MovieGenres { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }
        public virtual ICollection<MovieDateSetting> MovieDateSettings { get; set; }
    }
}