using GolfService.APIs;
using GolfService.APIs.Common;
using GolfService.APIs.Dtos;
using GolfService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GolfService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PlayersControllerBase : ControllerBase
{
    protected readonly IPlayersService _service;

    public PlayersControllerBase(IPlayersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Player
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Player>> CreatePlayer(PlayerCreateInput input)
    {
        var player = await _service.CreatePlayer(input);

        return CreatedAtAction(nameof(Player), new { id = player.Id }, player);
    }

    /// <summary>
    /// Delete one Player
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeletePlayer([FromRoute()] PlayerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePlayer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Players
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Player>>> Players([FromQuery()] PlayerFindManyArgs filter)
    {
        return Ok(await _service.Players(filter));
    }

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PlayersMeta(
        [FromQuery()] PlayerFindManyArgs filter
    )
    {
        return Ok(await _service.PlayersMeta(filter));
    }

    /// <summary>
    /// Get one Player
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Player>> Player([FromRoute()] PlayerWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Player(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Player
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdatePlayer(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] PlayerUpdateInput playerUpdateDto
    )
    {
        try
        {
            await _service.UpdatePlayer(uniqueId, playerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple GamesAsPlayerOne records to Player
    /// </summary>
    [HttpPost("{Id}/gamesAsPlayerOne")]
    public async Task<ActionResult> ConnectGamesAsPlayerOne(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.ConnectGamesAsPlayerOne(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple GamesAsPlayerOne records from Player
    /// </summary>
    [HttpDelete("{Id}/gamesAsPlayerOne")]
    public async Task<ActionResult> DisconnectGamesAsPlayerOne(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromBody()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.DisconnectGamesAsPlayerOne(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple GamesAsPlayerOne records for Player
    /// </summary>
    [HttpGet("{Id}/gamesAsPlayerOne")]
    public async Task<ActionResult<List<Game>>> FindGamesAsPlayerOne(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] GameFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindGamesAsPlayerOne(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple GamesAsPlayerOne records for Player
    /// </summary>
    [HttpPatch("{Id}/gamesAsPlayerOne")]
    public async Task<ActionResult> UpdateGamesAsPlayerOne(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromBody()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.UpdateGamesAsPlayerOne(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple GamesAsPlayerTwo records to Player
    /// </summary>
    [HttpPost("{Id}/gamesAsPlayerTwo")]
    public async Task<ActionResult> ConnectGamesAsPlayerTwo(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.ConnectGamesAsPlayerTwo(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple GamesAsPlayerTwo records from Player
    /// </summary>
    [HttpDelete("{Id}/gamesAsPlayerTwo")]
    public async Task<ActionResult> DisconnectGamesAsPlayerTwo(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromBody()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.DisconnectGamesAsPlayerTwo(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple GamesAsPlayerTwo records for Player
    /// </summary>
    [HttpGet("{Id}/gamesAsPlayerTwo")]
    public async Task<ActionResult<List<Game>>> FindGamesAsPlayerTwo(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] GameFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindGamesAsPlayerTwo(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple GamesAsPlayerTwo records for Player
    /// </summary>
    [HttpPatch("{Id}/gamesAsPlayerTwo")]
    public async Task<ActionResult> UpdateGamesAsPlayerTwo(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromBody()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.UpdateGamesAsPlayerTwo(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
