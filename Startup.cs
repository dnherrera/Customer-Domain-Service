using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CustomerAPI.Data;
using CustomerAPI.Helpers;
using CustomerAPI.Middlewares;
using CustomerAPI.Services;
using CustomerAPI.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CustomerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; private set; }
        //public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Customer API", Version = "v1.0" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                c.OperationFilter<SwaggerAuthorizationOperationFilter>();
            });

            services.AddAutoMapper(c => {c.AddCollectionMappers();});
            services.AddDbContext<RepositoryDbContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("CustomerAPIConnection")));
            services.Configure<AuthKeySetting>(Configuration.GetSection(AuthKeySetting.SectionName));
            services.Configure<AppSetting>(Configuration.GetSection(AppSetting.SectionName));

            services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); ;
            services.AddTransient<CustomerSeed>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IAuthenticationKeyService, AuthenticationKeyService>();
            services.AddSingleton<ITimeService, TimeService>();
            services.AddOptions();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("BasicAuthentication", null);

        }
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    //builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
        //    builder.RegisterType<AuthenticationKeyService>().As<IAuthenticationKeyService>();
        //    builder.RegisterType<TimeService>().As<ITimeService>();
        //}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CustomerSeed customerSeed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            //customerSeed.SeedCustomers();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); 

            app.UseAuthorization();

            app.UseSwagger();

            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Customer API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
