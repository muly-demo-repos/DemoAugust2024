using OrganizationalManagementSystem.APIs.Dtos;
using OrganizationalManagementSystem.Infrastructure.Models;

namespace OrganizationalManagementSystem.APIs.Extensions;

public static class PeopleExtensions
{
    public static Person ToDto(this PersonDbModel model)
    {
        return new Person
        {
            AdditionalOrganizations = model.AdditionalOrganizations?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            FirstName = model.FirstName,
            Id = model.Id,
            LastName = model.LastName,
            MainOrganization = model.MainOrganizationId,
            Role = model.Role,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PersonDbModel ToModel(
        this PersonUpdateInput updateDto,
        PersonWhereUniqueInput uniqueId
    )
    {
        var person = new PersonDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            Role = updateDto.Role
        };

        if (updateDto.CreatedAt != null)
        {
            person.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.MainOrganization != null)
        {
            person.MainOrganizationId = updateDto.MainOrganization;
        }
        if (updateDto.UpdatedAt != null)
        {
            person.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return person;
    }
}
