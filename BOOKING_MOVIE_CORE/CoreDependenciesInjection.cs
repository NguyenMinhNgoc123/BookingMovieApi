using BOOKING_MOVIE_ENTITY;
using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_CORE
{
    public class CoreDependenciesInjection
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddScoped<UnitOfWork>();

            ApplicationServiceRegister.Register(services);
            DomainServiceRegister.Register(services);
        }
    }
}