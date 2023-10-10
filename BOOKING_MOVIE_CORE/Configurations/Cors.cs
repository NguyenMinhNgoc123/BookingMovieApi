using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_CORE.Configurations
{
    public static class Cors
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition")
                        .Build());
            });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
        }
    }
}