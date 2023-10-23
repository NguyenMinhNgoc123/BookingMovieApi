using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class ComboFood : EntitieDate
    {
        public string Name { get; set; }
        
        public long FoodId { get; set; }
        
        public virtual Food Foods { get; set; }
    }
}