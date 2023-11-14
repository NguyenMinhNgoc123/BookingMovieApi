using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieGenresServices : ApplicationService<MovieGenres>
    {
        public MovieGenresServices(
            GenericDomainService<MovieGenres> genericDomainService) : base(genericDomainService)
        {
        }
    }
}