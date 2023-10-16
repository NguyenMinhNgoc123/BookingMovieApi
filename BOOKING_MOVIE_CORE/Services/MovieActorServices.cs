using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieActorServices : GenericDomainService<MovieActor>
    {
        public MovieActorServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}