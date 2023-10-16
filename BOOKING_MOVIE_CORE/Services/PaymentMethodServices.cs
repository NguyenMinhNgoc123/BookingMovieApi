using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class PaymentMethodServices : ApplicationService<PaymentMethod>
    {
        public PaymentMethodServices(GenericDomainService<PaymentMethod> domainService) : base(domainService)
        {
        }
    }
}