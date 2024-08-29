using OrganizationalManagementSystem.Core.Enums;

namespace OrganizationalManagementSystem.APIs.Dtos;

public class PersonUpdateInput
{
    public List<string>? AdditionalOrganizations { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public string? MainOrganization { get; set; }

    public RoleEnum? Role { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
