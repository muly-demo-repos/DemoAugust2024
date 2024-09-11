using GolfService.Infrastructure;

namespace GolfService.APIs;

public class GamesService : GamesServiceBase
{
    public GamesService(GolfServiceDbContext context)
        : base(context) { }
}
