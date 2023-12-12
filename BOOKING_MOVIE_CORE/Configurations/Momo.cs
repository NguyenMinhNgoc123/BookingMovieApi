using BOOKING_MOVIE_CORE.Values;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_CORE.Configurations
{
    public static class Momo
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MomoConfig>(configuration.GetSection(MomoConfig.ConfigName));
        }
    }
}