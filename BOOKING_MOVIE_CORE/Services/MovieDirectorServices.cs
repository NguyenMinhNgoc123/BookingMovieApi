using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieDirectorServices : GenericDomainService<MovieDirector>
    {
        public MovieDirectorServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}