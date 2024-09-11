using GolfService.APIs;
using GolfService.APIs.Common;
using GolfService.APIs.Dtos;
using GolfService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GolfService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CoursesControllerBase : ControllerBase
{
    protected readonly ICoursesService _service;

    public CoursesControllerBase(ICoursesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Course
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Course>> CreateCourse(CourseCreateInput input)
    {
        var course = await _service.CreateCourse(input);

        return CreatedAtAction(nameof(Course), new { id = course.Id }, course);
    }

    /// <summary>
    /// Delete one Course
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCourse([FromRoute()] CourseWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCourse(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Courses
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Course>>> Courses([FromQuery()] CourseFindManyArgs filter)
    {
        return Ok(await _service.Courses(filter));
    }

    /// <summary>
    /// Meta data about Course records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CoursesMeta(
        [FromQuery()] CourseFindManyArgs filter
    )
    {
        return Ok(await _service.CoursesMeta(filter));
    }

    /// <summary>
    /// Get one Course
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Course>> Course([FromRoute()] CourseWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Course(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Course
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCourse(
        [FromRoute()] CourseWhereUniqueInput uniqueId,
        [FromQuery()] CourseUpdateInput courseUpdateDto
    )
    {
        try
        {
            await _service.UpdateCourse(uniqueId, courseUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Games records to Course
    /// </summary>
    [HttpPost("{Id}/games")]
    public async Task<ActionResult> ConnectGames(
        [FromRoute()] CourseWhereUniqueInput uniqueId,
        [FromQuery()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.ConnectGames(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Games records from Course
    /// </summary>
    [HttpDelete("{Id}/games")]
    public async Task<ActionResult> DisconnectGames(
        [FromRoute()] CourseWhereUniqueInput uniqueId,
        [FromBody()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.DisconnectGames(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Games records for Course
    /// </summary>
    [HttpGet("{Id}/games")]
    public async Task<ActionResult<List<Game>>> FindGames(
        [FromRoute()] CourseWhereUniqueInput uniqueId,
        [FromQuery()] GameFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindGames(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Games records for Course
    /// </summary>
    [HttpPatch("{Id}/games")]
    public async Task<ActionResult> UpdateGames(
        [FromRoute()] CourseWhereUniqueInput uniqueId,
        [FromBody()] GameWhereUniqueInput[] gamesId
    )
    {
        try
        {
            await _service.UpdateGames(uniqueId, gamesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
