using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class CustomerPromotionServices : ApplicationService<CustomerPromotion>
    {
        public CustomerPromotionServices(GenericDomainService<CustomerPromotion> genericDomainService) : base(genericDomainService)
        {
        }
    }
}