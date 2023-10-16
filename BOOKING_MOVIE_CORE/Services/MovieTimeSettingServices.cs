using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieTimeSettingServices : ApplicationService<MovieTimeSetting>
    {
        public MovieTimeSettingServices(GenericDomainService<MovieTimeSetting> domainService) : base(domainService)
        {
        }
    }
}