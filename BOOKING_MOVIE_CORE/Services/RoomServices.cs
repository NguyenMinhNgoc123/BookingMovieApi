using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class RoomServices : ApplicationService<Room>
    {
        public RoomServices(GenericDomainService<Room> domainService) : base(domainService)
        {
        }
    }
}