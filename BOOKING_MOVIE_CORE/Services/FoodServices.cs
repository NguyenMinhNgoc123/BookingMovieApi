using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class FoodServices : ApplicationService<Food>
    {
        public FoodServices(GenericDomainService<Food> genericDomainService) : base(genericDomainService)
        {
        }
    }
}