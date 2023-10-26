using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class RateServices : ApplicationService<Rate>
    {
        public RateServices(GenericDomainService<Rate> genericDomainService) : base(genericDomainService)
        {
        }
    }
}