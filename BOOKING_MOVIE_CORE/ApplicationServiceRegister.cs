using BOOKING_MOVIE_CORE.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_CORE
{
    public class ApplicationServiceRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<UserServices>();
        }

    }
}