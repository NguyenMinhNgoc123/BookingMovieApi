using System.Collections.Generic;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class ComboFood : EntitieDate
    {
        public string Name { get; set; }
        
        public decimal? Price { get; set; }
        
        public virtual ICollection<Combo> Combos { get; set; }
    }
}