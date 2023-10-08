using System;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace BOOKING_MOVIE_CORE.Configurations
{
    public static class DbContext
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
            var mySqlVersion = new Version(8, 0, 0);

            services.AddDbContextPool<movie_context>(options =>
                options
                    .UseLazyLoadingProxies(false)
                    .UseMySql(
                        connectionString,
                        mysqlOptions =>
                            mysqlOptions
                                .MigrationsAssembly("BOOKING_MOVIE_MIGRATION")
                                .ServerVersion(mySqlVersion, ServerType.MySql)
                    )
                    .EnableSensitiveDataLogging()
            );
        }
    }
}