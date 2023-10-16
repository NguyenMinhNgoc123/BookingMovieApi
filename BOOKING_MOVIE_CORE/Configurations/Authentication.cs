using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BOOKING_MOVIE_CORE.Configurations
{
    public static class Authentication
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration, Type EventTypeUsser = null)
        {
            if(EventTypeUsser != null)
            {
                services.AddScoped(EventTypeUsser);
            }

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(WebEncoders.Base64UrlDecode(Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero,
                        NameClaimType = JwtRegisteredClaimNames.Sub
                    };

                    if (EventTypeUsser != null)
                    {
                        options.EventsType = EventTypeUsser;
                    }
                });
        }
        
        public static void ConfigureApp(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}