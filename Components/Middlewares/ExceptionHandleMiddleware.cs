using System;
using System.Text.Json;
using System.Threading.Tasks;
using Customer.Components.Dtos.Responses;
using Customer.Components.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Customer.Components.Middlewares
{
    /// <summary>
    /// The exception handle middleware.
    /// </summary>
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandleMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var result = new ProblemDetailsDto
            {
                Title = "Unhandle exception.",
                Detail = ex.Message,
                Type = ex.GetType().ToString(),
                Instance = GetRoute(context.GetRouteData()),
            };

            if (ex is BadInputException exception)
            {
                result.Title = exception.Title;
                result.ErrorCode = exception.ErrorCode;
                result.Status = context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (ex is NotFoundException notfound)
            {
                result.Title = notfound.Title;
                result.ErrorCode = notfound.ErrorCode;
                result.Status = context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (ex is UnauthorizedAccessException unauthorizedAccessException)
            {
                result.Title = "Unauthorized access.";
                result.Status = context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                result.Status = context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            // write the response
            await context.Response.WriteAsync(JsonSerializer.Serialize(result,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        private string GetRoute(RouteData routeData)
        {
            return $"{routeData.Values["controller"]}/{routeData.Values["action"]}";
        }
    }
}
