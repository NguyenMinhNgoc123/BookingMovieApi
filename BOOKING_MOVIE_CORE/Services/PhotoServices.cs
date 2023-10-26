using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class PhotoServices : ApplicationService<Photo>
    {
        public PhotoServices(GenericDomainService<Photo> genericDomainService) : base(genericDomainService)
        {
        }
    }
}