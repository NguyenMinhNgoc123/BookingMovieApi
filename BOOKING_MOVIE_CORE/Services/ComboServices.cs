using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class ComboServices : ApplicationService<Combo>
    {
        public ComboServices(GenericDomainService<Combo> genericDomainService) : base(genericDomainService)
        {
        }
    }
}