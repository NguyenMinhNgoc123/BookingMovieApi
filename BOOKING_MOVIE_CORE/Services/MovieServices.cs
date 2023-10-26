using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieServices : ApplicationService<Movie>
    {
        public MovieServices(GenericDomainService<Movie> genericDomainService) : base(genericDomainService)
        {
        }
    }
}