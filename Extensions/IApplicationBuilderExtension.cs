using Microsoft.AspNetCore.Builder;

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
        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(options =>
            {
                //options.SerializeAsV2 = true;
                options.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Customer API");
                //config.RoutePrefix = "/swagger";
            });
        }
    }
}
