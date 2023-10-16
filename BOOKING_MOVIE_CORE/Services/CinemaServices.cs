using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class CinemaServices : ApplicationService<Cinema>
    {
        public CinemaServices(GenericDomainService<Cinema> domainService) : base(domainService)
        {
        }
    }
}