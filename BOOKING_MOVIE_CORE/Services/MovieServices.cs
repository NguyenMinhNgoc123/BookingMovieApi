using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieServices : GenericDomainService<Movie>
    {
        public MovieServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}