using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_CORE.Values;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_CORE
{
    public class ApplicationServiceRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<MomoConfig>();

            services.AddScoped<UserServices>();
            services.AddScoped<AuthServices>();
            services.AddScoped<CustomerServices>();
            services.AddScoped<ActorServices>();
            services.AddScoped<DirectorServices>();
            services.AddScoped<GenreServices>();
            services.AddScoped<MovieServices>();
            services.AddScoped<CinemaServices>();
            services.AddScoped<InvoiceServices>();
            services.AddScoped<InvoicesDetailServices>();
            services.AddScoped<MovieActorServices>();
            services.AddScoped<RoomServices>();
            services.AddScoped<MovieGenresServices>();
            services.AddScoped<MovieDirectorServices>();
            services.AddScoped<MovieCinemaServices>();
            services.AddScoped<MovieRoomServices>();
            services.AddScoped<MovieDateSettingServices>();
            services.AddScoped<MovieTimeSettingServices>();
            services.AddScoped<PaymentMethodServices>();
            services.AddScoped<InvoicePaymentServices>();
            services.AddScoped<PromotionServices>();
            services.AddScoped<PhotoServices>();
            services.AddScoped<VideoServices>();
            services.AddScoped<FoodServices>();
            services.AddScoped<ComboFoodServices>();
            services.AddScoped<ComboServices>();
        }

    }
}