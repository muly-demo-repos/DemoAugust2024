using GolfService.APIs;
using GolfService.APIs.Common;
using GolfService.APIs.Dtos;
using GolfService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GolfService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class GamesControllerBase : ControllerBase
{
    protected readonly IGamesService _service;

    public GamesControllerBase(IGamesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Game
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Game>> CreateGame(GameCreateInput input)
    {
        var game = await _service.CreateGame(input);

        return CreatedAtAction(nameof(Game), new { id = game.Id }, game);
    }

    /// <summary>
    /// Delete one Game
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteGame([FromRoute()] GameWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteGame(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Games
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Game>>> Games([FromQuery()] GameFindManyArgs filter)
    {
        return Ok(await _service.Games(filter));
    }

    /// <summary>
    /// Meta data about Game records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> GamesMeta([FromQuery()] GameFindManyArgs filter)
    {
        return Ok(await _service.GamesMeta(filter));
    }

    /// <summary>
    /// Get one Game
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Game>> Game([FromRoute()] GameWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Game(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Game
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateGame(
        [FromRoute()] GameWhereUniqueInput uniqueId,
        [FromQuery()] GameUpdateInput gameUpdateDto
    )
    {
        try
        {
            await _service.UpdateGame(uniqueId, gameUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Course record for Game
    /// </summary>
    [HttpGet("{Id}/course")]
    public async Task<ActionResult<List<Course>>> GetCourse(
        [FromRoute()] GameWhereUniqueInput uniqueId
    )
    {
        var course = await _service.GetCourse(uniqueId);
        return Ok(course);
    }
}
