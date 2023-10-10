using BOOKING_MOVIE_CORE;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class UserServices : ApplicationService<User>
    {
        public UserServices(GenericDomainService<User> genericDomainService) : base(genericDomainService)
        {
            
        }
    }
}