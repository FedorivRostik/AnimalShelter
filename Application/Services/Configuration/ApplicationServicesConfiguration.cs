using Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Configuration;
public static class ApplicationServicesConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizeService, AuthorizeService>();
    }
}
