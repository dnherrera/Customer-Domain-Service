using Customer.Components.Middlewares;
using CustomerAPI.Data;
using CustomerAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerAPI
{
    /// <summary>
    /// The start up
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration _configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configure the service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSettings(_configuration);

            services.AddControllers();

            services.AddRepositories();

            services.AddExternalService();

            services.AddAutoMapperProfiles();

            services.AddMediaRHandlers();

            services.AddCustomSwagger(_configuration);

            services.AddBasicAuthentication();
        }

        /// <summary>
        /// Config the application.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="customerSeed"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CustomerSeed customerSeed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseHttpsRedirection();
            }

            //customerSeed.SeedCustomers();

            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseRouting();

            app.UseAuthentication(); 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomSwagger(_configuration);
        }
    }
}
