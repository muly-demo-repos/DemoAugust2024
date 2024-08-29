using OrganizationalManagementSystem.APIs.Common;
using OrganizationalManagementSystem.APIs.Dtos;

namespace OrganizationalManagementSystem.APIs;

public interface IPeopleService
{
    /// <summary>
    /// Create one Person
    /// </summary>
    public Task<Person> CreatePerson(PersonCreateInput person);

    /// <summary>
    /// Delete one Person
    /// </summary>
    public Task DeletePerson(PersonWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many People
    /// </summary>
    public Task<List<Person>> People(PersonFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Person records
    /// </summary>
    public Task<MetadataDto> PeopleMeta(PersonFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Person
    /// </summary>
    public Task<Person> Person(PersonWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Person
    /// </summary>
    public Task UpdatePerson(PersonWhereUniqueInput uniqueId, PersonUpdateInput updateDto);

    /// <summary>
    /// Get a MainOrganization record for Person
    /// </summary>
    public Task<Organization> GetMainOrganization(PersonWhereUniqueInput uniqueId);
}
