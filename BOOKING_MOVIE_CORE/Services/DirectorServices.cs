using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class DirectorServices : ApplicationService<Director>
    {
        public DirectorServices(GenericDomainService<Director> genericDomainService) : base(genericDomainService)
        {
        }
    }
}