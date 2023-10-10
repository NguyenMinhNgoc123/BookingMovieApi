using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_CORE
{
    public class DomainServiceRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(GenericDomainService<>));

        }

    }
}