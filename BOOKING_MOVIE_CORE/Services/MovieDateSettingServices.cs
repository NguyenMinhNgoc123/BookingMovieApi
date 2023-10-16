using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieDateSettingServices : ApplicationService<MovieDateSetting>
    {
        public MovieDateSettingServices(GenericDomainService<MovieDateSetting> domainService) : base(domainService)
        {
        }
    }
}