using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieCategoriesServices : GenericDomainService<MovieCategories>
    {
        public MovieCategoriesServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}