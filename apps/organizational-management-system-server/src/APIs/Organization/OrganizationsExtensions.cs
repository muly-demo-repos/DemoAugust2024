using OrganizationalManagementSystem.APIs.Dtos;
using OrganizationalManagementSystem.Infrastructure.Models;

namespace OrganizationalManagementSystem.APIs.Extensions;

public static class OrganizationsExtensions
{
    public static Organization ToDto(this OrganizationDbModel model)
    {
        return new Organization
        {
            Address = model.Address,
            BillingAddress = model.BillingAddress,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            MainMembers = model.MainMembers?.Select(x => x.Id).ToList(),
            Name = model.Name,
            Persons = model.PersonsId,
            UpdatedAt = model.UpdatedAt,
            Website = model.Website,
        };
    }

    public static OrganizationDbModel ToModel(
        this OrganizationUpdateInput updateDto,
        OrganizationWhereUniqueInput uniqueId
    )
    {
        var organization = new OrganizationDbModel
        {
            Id = uniqueId.Id,
            Address = updateDto.Address,
            BillingAddress = updateDto.BillingAddress,
            Name = updateDto.Name,
            Website = updateDto.Website
        };

        if (updateDto.CreatedAt != null)
        {
            organization.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Persons != null)
        {
            organization.PersonsId = updateDto.Persons;
        }
        if (updateDto.UpdatedAt != null)
        {
            organization.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return organization;
    }
}
