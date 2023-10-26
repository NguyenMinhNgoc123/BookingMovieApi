using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieActorServices : ApplicationService<MovieActor>
    {
        public MovieActorServices(GenericDomainService<MovieActor> genericDomainService) : base(genericDomainService)
        {
        }
    }
}