using System;
using System.IO;
using System.Reflection;
using CustomerAPI.AutoMapperProfiles;
using CustomerAPI.Data;
using CustomerAPI.Filters;
using CustomerAPI.Handlers;
using CustomerAPI.Services.Concretes;
using CustomerAPI.Services.Contracts;
using CustomerAPI.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CustomerAPI.Extensions
{
    /// <summary>
    /// The IService collection extension.
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Adds the settings.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // connect to Customer API Connection
            services.AddDbContext<RepositoryDbContext>(opt => opt.UseSqlite(configuration.GetConnectionString("CustomerAPIConnection")));

            // read Auth Key Setting
            services.Configure<AuthKeySetting>(configuration.GetSection(AuthKeySetting.SectionName));

            // read App Setting
            services.Configure<AppSetting>(configuration.GetSection(AppSetting.SectionName));
        }

        /// <summary>
        /// Add Controller with Views to support Auto Profile while Displaying after Creating User.
        /// </summary>
        /// <param name="services"></param>
        public static void AddControllersWithView(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IAuthenticationKeyService, AuthenticationKeyService>();
            services.AddSingleton<ITimeService, TimeService>();
            services.AddTransient<CustomerSeed>();
        }

        /// <summary>
        /// Adds media r handlers.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddMediaRHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetCustomerCollectionHandler));
            services.AddMediatR(typeof(GetCustomerByIdHandler));
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddExternalService(this IServiceCollection services)
        {
        }

        /// <summary>
        /// Adds the automatic mapper profiles.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CustomerProfile).Assembly);
        }

        /// <summary>
        /// Adds media r handlers.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddBasicAuthentication(this IServiceCollection services)
        {
            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("BasicAuthentication", null);
        }

        /// <summary>
        /// Adds custom swagger.
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo 
                { 
                    Title = "Customer API", 
                    Version = "v1.0" 
                
                });

                c.EnableAnnotations();
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });

                c.OperationFilter<SwaggerAuthorizationOperationFilter>();

                // make sure generate swagger with operation id
                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");

                // xml document for decorate the swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
