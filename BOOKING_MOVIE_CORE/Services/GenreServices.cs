using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class GenreServices : ApplicationService<Genre>
    {
        public GenreServices(GenericDomainService<Genre> domainService) : base(domainService)
        {
        }
    }
}