using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_CORE
{
    public class ApplicationServiceRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<UserServices>();
            services.AddScoped<AuthServices>();
            services.AddScoped<CustomerServices>();
            services.AddScoped<ActorServices>();
            services.AddScoped<DirectorServices>();
            services.AddScoped<CategoryServices>();
            services.AddScoped<MovieServices>();
            services.AddScoped<CinemaServices>();
            services.AddScoped<InvoiceServices>();
            services.AddScoped<InvoicesDetailServices>();
            services.AddScoped<MovieActor>();
        }

    }
}