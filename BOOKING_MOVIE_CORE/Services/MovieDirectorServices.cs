using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieDirectorServices : ApplicationService<MovieDirector>
    {
        public MovieDirectorServices(GenericDomainService<MovieDirector> genericDomainService) : base(genericDomainService)
        {
        }
    }
}