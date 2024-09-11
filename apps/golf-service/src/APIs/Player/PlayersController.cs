using Microsoft.AspNetCore.Mvc;

namespace GolfService.APIs;

[ApiController()]
public class PlayersController : PlayersControllerBase
{
    public PlayersController(IPlayersService service)
        : base(service) { }
}
