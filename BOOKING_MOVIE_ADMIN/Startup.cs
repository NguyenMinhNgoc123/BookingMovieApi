using System;
using BOOKING_MOVIE_ADMIN.Basis;
using BOOKING_MOVIE_CORE;
using BOOKING_MOVIE_CORE.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DbContext = BOOKING_MOVIE_CORE.Configurations.DbContext;

namespace BOOKING_MOVIE_ADMIN
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Mvc.ConfigureServices(services, Configuration);
            Cors.ConfigureServices(services);
            DbContext.ConfigureServices(services, Configuration);

            Authentication.ConfigureServices(
                services, Configuration,
                typeof(UserValidation)
            );
            Authorization.ConfigureServices(services);
            CoreDependenciesInjection.Inject(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            Authentication.ConfigureApp(app);
            Authorization.ConfigureApp(app);
            Cors.Configure(app);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}