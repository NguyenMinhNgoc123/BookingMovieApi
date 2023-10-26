using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieCategoriesServices : ApplicationService<MovieCategories>
    {
        public MovieCategoriesServices(GenericDomainService<MovieCategories> genericDomainService) : base(genericDomainService)
        {
        }
    }
}