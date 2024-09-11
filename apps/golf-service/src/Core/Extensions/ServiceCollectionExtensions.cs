using GolfService.APIs;

namespace GolfService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICoursesService, CoursesService>();
        services.AddScoped<IGamesService, GamesService>();
        services.AddScoped<IPlayersService, PlayersService>();
    }
}
