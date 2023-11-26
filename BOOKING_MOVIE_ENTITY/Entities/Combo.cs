using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Combo : EntitieDate
    {
        public long ComboFoodId { get; set; }

        public long FoodId { get; set; }
        
        public virtual Food Food { get; set; }
    }
}