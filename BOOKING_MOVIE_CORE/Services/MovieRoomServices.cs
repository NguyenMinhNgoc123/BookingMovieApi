using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieRoomServices : GenericDomainService<MovieRoom>
    {
        public MovieRoomServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}