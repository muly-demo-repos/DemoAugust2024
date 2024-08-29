using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationalManagementSystem.APIs;
using OrganizationalManagementSystem.APIs.Common;
using OrganizationalManagementSystem.APIs.Dtos;
using OrganizationalManagementSystem.APIs.Errors;

namespace OrganizationalManagementSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PeopleControllerBase : ControllerBase
{
    protected readonly IPeopleService _service;

    public PeopleControllerBase(IPeopleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Person
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Person>> CreatePerson(PersonCreateInput input)
    {
        var person = await _service.CreatePerson(input);

        return CreatedAtAction(nameof(Person), new { id = person.Id }, person);
    }

    /// <summary>
    /// Delete one Person
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeletePerson([FromRoute()] PersonWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePerson(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many People
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Person>>> People([FromQuery()] PersonFindManyArgs filter)
    {
        return Ok(await _service.People(filter));
    }

    /// <summary>
    /// Meta data about Person records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PeopleMeta([FromQuery()] PersonFindManyArgs filter)
    {
        return Ok(await _service.PeopleMeta(filter));
    }

    /// <summary>
    /// Get one Person
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Person>> Person([FromRoute()] PersonWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Person(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Person
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdatePerson(
        [FromRoute()] PersonWhereUniqueInput uniqueId,
        [FromQuery()] PersonUpdateInput personUpdateDto
    )
    {
        try
        {
            await _service.UpdatePerson(uniqueId, personUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a MainOrganization record for Person
    /// </summary>
    [HttpGet("{Id}/mainOrganization")]
    public async Task<ActionResult<List<Organization>>> GetMainOrganization(
        [FromRoute()] PersonWhereUniqueInput uniqueId
    )
    {
        var organization = await _service.GetMainOrganization(uniqueId);
        return Ok(organization);
    }
}
