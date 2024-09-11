using GolfService.Infrastructure;

namespace GolfService.APIs;

public class PlayersService : PlayersServiceBase
{
    public PlayersService(GolfServiceDbContext context)
        : base(context) { }
}
