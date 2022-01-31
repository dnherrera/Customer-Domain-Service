using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace CustomerAPI.Extensions
{
    /// <summary>
    /// The IApplication builder extension.
    /// </summary>
    public static class IApplicationBuilderExtension
    {
        /// <summary>
        /// Uses custom swagger.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration"></param>
        public static void UseCustomSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/api/swagger/v1.0/swagger.json", configuration.GetValue<string>("SwaggerSettings:AppName"));
                config.RoutePrefix = "api/swagger";
            });
        }
    }
}
