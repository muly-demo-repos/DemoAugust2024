using Microsoft.EntityFrameworkCore;
using OrganizationalManagementSystem.APIs;
using OrganizationalManagementSystem.APIs.Common;
using OrganizationalManagementSystem.APIs.Dtos;
using OrganizationalManagementSystem.APIs.Errors;
using OrganizationalManagementSystem.APIs.Extensions;
using OrganizationalManagementSystem.Infrastructure;
using OrganizationalManagementSystem.Infrastructure.Models;

namespace OrganizationalManagementSystem.APIs;

public abstract class PeopleServiceBase : IPeopleService
{
    protected readonly OrganizationalManagementSystemDbContext _context;

    public PeopleServiceBase(OrganizationalManagementSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Person
    /// </summary>
    public async Task<Person> CreatePerson(PersonCreateInput createDto)
    {
        var person = new PersonDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Role = createDto.Role,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            person.Id = createDto.Id;
        }
        if (createDto.AdditionalOrganizations != null)
        {
            person.AdditionalOrganizations = await _context
                .Organizations.Where(organization =>
                    createDto.AdditionalOrganizations.Select(t => t.Id).Contains(organization.Id)
                )
                .ToListAsync();
        }

        if (createDto.MainOrganization != null)
        {
            person.MainOrganization = await _context
                .Organizations.Where(organization =>
                    createDto.MainOrganization.Id == organization.Id
                )
                .FirstOrDefaultAsync();
        }

        _context.People.Add(person);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PersonDbModel>(person.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Person
    /// </summary>
    public async Task DeletePerson(PersonWhereUniqueInput uniqueId)
    {
        var person = await _context.People.FindAsync(uniqueId.Id);
        if (person == null)
        {
            throw new NotFoundException();
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many People
    /// </summary>
    public async Task<List<Person>> People(PersonFindManyArgs findManyArgs)
    {
        var people = await _context
            .People.Include(x => x.AdditionalOrganizations)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return people.ConvertAll(person => person.ToDto());
    }

    /// <summary>
    /// Meta data about Person records
    /// </summary>
    public async Task<MetadataDto> PeopleMeta(PersonFindManyArgs findManyArgs)
    {
        var count = await _context.People.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Person
    /// </summary>
    public async Task<Person> Person(PersonWhereUniqueInput uniqueId)
    {
        var people = await this.People(
            new PersonFindManyArgs { Where = new PersonWhereInput { Id = uniqueId.Id } }
        );
        var person = people.FirstOrDefault();
        if (person == null)
        {
            throw new NotFoundException();
        }

        return person;
    }

    /// <summary>
    /// Update one Person
    /// </summary>
    public async Task UpdatePerson(PersonWhereUniqueInput uniqueId, PersonUpdateInput updateDto)
    {
        var person = updateDto.ToModel(uniqueId);

        if (updateDto.AdditionalOrganizations != null)
        {
            person.AdditionalOrganizations = await _context
                .Organizations.Where(organization =>
                    updateDto.AdditionalOrganizations.Select(t => t).Contains(organization.Id)
                )
                .ToListAsync();
        }

        if (updateDto.MainOrganization != null)
        {
            person.MainOrganization = await _context
                .Organizations.Where(organization => updateDto.MainOrganization == organization.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.People.Any(e => e.Id == person.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a MainOrganization record for Person
    /// </summary>
    public async Task<Organization> GetMainOrganization(PersonWhereUniqueInput uniqueId)
    {
        var person = await _context
            .People.Where(person => person.Id == uniqueId.Id)
            .Include(person => person.MainOrganization)
            .FirstOrDefaultAsync();
        if (person == null)
        {
            throw new NotFoundException();
        }
        return person.MainOrganization.ToDto();
    }
}
