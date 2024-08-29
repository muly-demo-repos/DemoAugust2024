using Microsoft.EntityFrameworkCore;
using OrganizationalManagementSystem.APIs;
using OrganizationalManagementSystem.APIs.Common;
using OrganizationalManagementSystem.APIs.Dtos;
using OrganizationalManagementSystem.APIs.Errors;
using OrganizationalManagementSystem.APIs.Extensions;
using OrganizationalManagementSystem.Infrastructure;
using OrganizationalManagementSystem.Infrastructure.Models;

namespace OrganizationalManagementSystem.APIs;

public abstract class OrganizationsServiceBase : IOrganizationsService
{
    protected readonly OrganizationalManagementSystemDbContext _context;

    public OrganizationsServiceBase(OrganizationalManagementSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Organization
    /// </summary>
    public async Task<Organization> CreateOrganization(OrganizationCreateInput createDto)
    {
        var organization = new OrganizationDbModel
        {
            Address = createDto.Address,
            BillingAddress = createDto.BillingAddress,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt,
            Website = createDto.Website
        };

        if (createDto.Id != null)
        {
            organization.Id = createDto.Id;
        }
        if (createDto.MainMembers != null)
        {
            organization.MainMembers = await _context
                .People.Where(person => createDto.MainMembers.Select(t => t.Id).Contains(person.Id))
                .ToListAsync();
        }

        if (createDto.Persons != null)
        {
            organization.Persons = await _context
                .People.Where(person => createDto.Persons.Id == person.Id)
                .FirstOrDefaultAsync();
        }

        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<OrganizationDbModel>(organization.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Organization
    /// </summary>
    public async Task DeleteOrganization(OrganizationWhereUniqueInput uniqueId)
    {
        var organization = await _context.Organizations.FindAsync(uniqueId.Id);
        if (organization == null)
        {
            throw new NotFoundException();
        }

        _context.Organizations.Remove(organization);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Organizations
    /// </summary>
    public async Task<List<Organization>> Organizations(OrganizationFindManyArgs findManyArgs)
    {
        var organizations = await _context
            .Organizations.Include(x => x.MainMembers)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return organizations.ConvertAll(organization => organization.ToDto());
    }

    /// <summary>
    /// Meta data about Organization records
    /// </summary>
    public async Task<MetadataDto> OrganizationsMeta(OrganizationFindManyArgs findManyArgs)
    {
        var count = await _context.Organizations.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Organization
    /// </summary>
    public async Task<Organization> Organization(OrganizationWhereUniqueInput uniqueId)
    {
        var organizations = await this.Organizations(
            new OrganizationFindManyArgs { Where = new OrganizationWhereInput { Id = uniqueId.Id } }
        );
        var organization = organizations.FirstOrDefault();
        if (organization == null)
        {
            throw new NotFoundException();
        }

        return organization;
    }

    /// <summary>
    /// Update one Organization
    /// </summary>
    public async Task UpdateOrganization(
        OrganizationWhereUniqueInput uniqueId,
        OrganizationUpdateInput updateDto
    )
    {
        var organization = updateDto.ToModel(uniqueId);

        if (updateDto.MainMembers != null)
        {
            organization.MainMembers = await _context
                .People.Where(person => updateDto.MainMembers.Select(t => t).Contains(person.Id))
                .ToListAsync();
        }

        if (updateDto.Persons != null)
        {
            organization.Persons = await _context
                .People.Where(person => updateDto.Persons == person.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(organization).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Organizations.Any(e => e.Id == organization.Id))
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
    /// Connect multiple Main Mambers records to Organization
    /// </summary>
    public async Task ConnectMainMembers(
        OrganizationWhereUniqueInput uniqueId,
        PersonWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Organizations.Include(x => x.MainMembers)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .People.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.MainMembers);

        foreach (var child in childrenToConnect)
        {
            parent.MainMembers.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Main Mambers records from Organization
    /// </summary>
    public async Task DisconnectMainMembers(
        OrganizationWhereUniqueInput uniqueId,
        PersonWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Organizations.Include(x => x.MainMembers)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .People.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.MainMembers?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Main Mambers records for Organization
    /// </summary>
    public async Task<List<Person>> FindMainMembers(
        OrganizationWhereUniqueInput uniqueId,
        PersonFindManyArgs organizationFindManyArgs
    )
    {
        var people = await _context
            .People.Where(m => m.MainOrganizationId == uniqueId.Id)
            .ApplyWhere(organizationFindManyArgs.Where)
            .ApplySkip(organizationFindManyArgs.Skip)
            .ApplyTake(organizationFindManyArgs.Take)
            .ApplyOrderBy(organizationFindManyArgs.SortBy)
            .ToListAsync();

        return people.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Main Mambers records for Organization
    /// </summary>
    public async Task UpdateMainMembers(
        OrganizationWhereUniqueInput uniqueId,
        PersonWhereUniqueInput[] childrenIds
    )
    {
        var organization = await _context
            .Organizations.Include(t => t.MainMembers)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (organization == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .People.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        organization.MainMembers = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Persons record for Organization
    /// </summary>
    public async Task<Person> GetPersons(OrganizationWhereUniqueInput uniqueId)
    {
        var organization = await _context
            .Organizations.Where(organization => organization.Id == uniqueId.Id)
            .Include(organization => organization.Persons)
            .FirstOrDefaultAsync();
        if (organization == null)
        {
            throw new NotFoundException();
        }
        return organization.Persons.ToDto();
    }
}
