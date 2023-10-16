using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieCinemaServices : ApplicationService<MovieCinema>
    {
        public MovieCinemaServices(DomainService<MovieCinema> domainService) : base(domainService)
        {
        }
    }
}