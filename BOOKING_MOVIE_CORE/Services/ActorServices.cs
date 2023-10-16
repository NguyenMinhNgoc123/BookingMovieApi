using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class ActorServices : ApplicationService<Actor>
    {
        public ActorServices(GenericDomainService<Actor> domainService) : base(domainService)
        {
        }
    }
}