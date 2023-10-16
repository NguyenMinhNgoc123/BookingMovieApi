using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_CORE.Configurations
{
    public class Authorization : JwtBearerEvents
    {
        public static void ConfigureServices(IServiceCollection services, Type EventType = null)
        {
            if(EventType != null)
            {
                services.AddScoped(EventType);
            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                {
                    policy.RequireClaim("userId"); 
                });

                options.AddPolicy("Customer", policy =>
                {
                    policy.RequireClaim("customerId");
                });
            });
        }
        
        public static void ConfigureApp(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}