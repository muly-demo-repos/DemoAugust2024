using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationalManagementSystem.APIs;
using OrganizationalManagementSystem.APIs.Common;
using OrganizationalManagementSystem.APIs.Dtos;
using OrganizationalManagementSystem.APIs.Errors;

namespace OrganizationalManagementSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OrganizationsControllerBase : ControllerBase
{
    protected readonly IOrganizationsService _service;

    public OrganizationsControllerBase(IOrganizationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Organization
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Organization>> CreateOrganization(OrganizationCreateInput input)
    {
        var organization = await _service.CreateOrganization(input);

        return CreatedAtAction(nameof(Organization), new { id = organization.Id }, organization);
    }

    /// <summary>
    /// Delete one Organization
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteOrganization(
        [FromRoute()] OrganizationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteOrganization(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Organizations
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Organization>>> Organizations(
        [FromQuery()] OrganizationFindManyArgs filter
    )
    {
        return Ok(await _service.Organizations(filter));
    }

    /// <summary>
    /// Meta data about Organization records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OrganizationsMeta(
        [FromQuery()] OrganizationFindManyArgs filter
    )
    {
        return Ok(await _service.OrganizationsMeta(filter));
    }

    /// <summary>
    /// Get one Organization
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Organization>> Organization(
        [FromRoute()] OrganizationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Organization(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Organization
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrganization(
        [FromRoute()] OrganizationWhereUniqueInput uniqueId,
        [FromQuery()] OrganizationUpdateInput organizationUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrganization(uniqueId, organizationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Main Mambers records to Organization
    /// </summary>
    [HttpPost("{Id}/mainMembers")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectMainMembers(
        [FromRoute()] OrganizationWhereUniqueInput uniqueId,
        [FromQuery()] PersonWhereUniqueInput[] peopleId
    )
    {
        try
        {
            await _service.ConnectMainMembers(uniqueId, peopleId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Main Mambers records from Organization
    /// </summary>
    [HttpDelete("{Id}/mainMembers")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectMainMembers(
        [FromRoute()] OrganizationWhereUniqueInput uniqueId,
        [FromBody()] PersonWhereUniqueInput[] peopleId
    )
    {
        try
        {
            await _service.DisconnectMainMembers(uniqueId, peopleId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Main Mambers records for Organization
    /// </summary>
    [HttpGet("{Id}/mainMembers")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Person>>> FindMainMembers(
        [FromRoute()] OrganizationWhereUniqueInput uniqueId,
        [FromQuery()] PersonFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindMainMembers(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Main Mambers records for Organization
    /// </summary>
    [HttpPatch("{Id}/mainMembers")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateMainMembers(
        [FromRoute()] OrganizationWhereUniqueInput uniqueId,
        [FromBody()] PersonWhereUniqueInput[] peopleId
    )
    {
        try
        {
            await _service.UpdateMainMembers(uniqueId, peopleId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Persons record for Organization
    /// </summary>
    [HttpGet("{Id}/persons")]
    public async Task<ActionResult<List<Person>>> GetPersons(
        [FromRoute()] OrganizationWhereUniqueInput uniqueId
    )
    {
        var person = await _service.GetPersons(uniqueId);
        return Ok(person);
    }
}
