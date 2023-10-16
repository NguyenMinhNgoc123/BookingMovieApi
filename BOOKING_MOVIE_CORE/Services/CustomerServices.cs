using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class CustomerServices : ApplicationService<Customer>
    {
        public CustomerServices(GenericDomainService<Customer> domainService) : base(domainService)
        {
        }
    }
}