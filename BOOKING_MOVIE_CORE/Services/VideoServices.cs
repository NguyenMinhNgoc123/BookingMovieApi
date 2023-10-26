using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class VideoServices : ApplicationService<Video>
    {
        public VideoServices(GenericDomainService<Video> genericDomainService) : base(genericDomainService)
        {
        }
    }
}