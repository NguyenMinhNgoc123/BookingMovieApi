using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using BOOKING_MOVIE_ENTITY.EntitieBases;
using Newtonsoft.Json;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Movie : EntitieDate
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
        public virtual ICollection<MovieCategories> MovieCategories { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }
        public virtual ICollection<MovieDateSetting> MovieDateSettings { get; set; }

    }
}