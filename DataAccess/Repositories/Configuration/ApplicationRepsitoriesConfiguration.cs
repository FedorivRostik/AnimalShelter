using Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Repositories.Configuration;
public static class ApplicationRepsitoriesConfiguration
{
    public static void AddApplicationRepsitories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizeRepository, AuthorizeRepository>();
    }
}

