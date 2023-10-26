using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class PromotionServices : ApplicationService<Promotion>
    {
        public PromotionServices(GenericDomainService<Promotion> genericDomainService) : base(genericDomainService)
        {
        }
    }
}