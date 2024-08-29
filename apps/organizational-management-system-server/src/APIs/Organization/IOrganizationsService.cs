using OrganizationalManagementSystem.APIs.Common;
using OrganizationalManagementSystem.APIs.Dtos;

namespace OrganizationalManagementSystem.APIs;

public interface IOrganizationsService
{
    /// <summary>
    /// Create one Organization
    /// </summary>
    public Task<Organization> CreateOrganization(OrganizationCreateInput organization);

    /// <summary>
    /// Delete one Organization
    /// </summary>
    public Task DeleteOrganization(OrganizationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Organizations
    /// </summary>
    public Task<List<Organization>> Organizations(OrganizationFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Organization records
    /// </summary>
    public Task<MetadataDto> OrganizationsMeta(OrganizationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Organization
    /// </summary>
    public Task<Organization> Organization(OrganizationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Organization
    /// </summary>
    public Task UpdateOrganization(
        OrganizationWhereUniqueInput uniqueId,
        OrganizationUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Main Mambers records to Organization
    /// </summary>
    public Task ConnectMainMembers(
        OrganizationWhereUniqueInput uniqueId,
        PersonWhereUniqueInput[] peopleId
    );

    /// <summary>
    /// Disconnect multiple Main Mambers records from Organization
    /// </summary>
    public Task DisconnectMainMembers(
        OrganizationWhereUniqueInput uniqueId,
        PersonWhereUniqueInput[] peopleId
    );

    /// <summary>
    /// Find multiple Main Mambers records for Organization
    /// </summary>
    public Task<List<Person>> FindMainMembers(
        OrganizationWhereUniqueInput uniqueId,
        PersonFindManyArgs PersonFindManyArgs
    );

    /// <summary>
    /// Update multiple Main Mambers records for Organization
    /// </summary>
    public Task UpdateMainMembers(
        OrganizationWhereUniqueInput uniqueId,
        PersonWhereUniqueInput[] peopleId
    );

    /// <summary>
    /// Get a Persons record for Organization
    /// </summary>
    public Task<Person> GetPersons(OrganizationWhereUniqueInput uniqueId);
}
