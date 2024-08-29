using OrganizationalManagementSystem.APIs;

namespace OrganizationalManagementSystem;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationsService, OrganizationsService>();
        services.AddScoped<IPeopleService, PeopleService>();
    }
}
