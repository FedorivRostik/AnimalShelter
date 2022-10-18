using Host.Middlewares;
using WebApi.Middlewares;

namespace Host.Middlewares.Configuration
{
    public static class ApplicationMiddlewareConfiguration
    {
        public static void AddApplicationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionsMiddleware>();
        }
    }
}
