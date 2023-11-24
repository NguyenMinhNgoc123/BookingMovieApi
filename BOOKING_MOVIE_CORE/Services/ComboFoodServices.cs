using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class ComboFoodServices : ApplicationService<ComboFood>
    {
        public ComboFoodServices(GenericDomainService<ComboFood> genericDomainService) : base(genericDomainService)
        {
        }
    }
}