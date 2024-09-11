using Microsoft.AspNetCore.Mvc;

namespace GolfService.APIs;

[ApiController()]
public class GamesController : GamesControllerBase
{
    public GamesController(IGamesService service)
        : base(service) { }
}
