using System.ComponentModel.DataAnnotations.Schema;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Actor : EntitieDate
    {
        public string Name { get; set; }
        
        [NotMapped]
        public virtual Photo ProfilePhoto { get; set; }
    }
}