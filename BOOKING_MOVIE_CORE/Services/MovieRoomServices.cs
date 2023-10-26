using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieRoomServices : ApplicationService<MovieRoom>
    {
        public MovieRoomServices(GenericDomainService<MovieRoom> genericDomainService) : base(genericDomainService)
        {
        }
    }
}