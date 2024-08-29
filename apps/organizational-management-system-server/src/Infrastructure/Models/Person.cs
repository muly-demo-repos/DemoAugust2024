using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganizationalManagementSystem.Core.Enums;

namespace OrganizationalManagementSystem.Infrastructure.Models;

[Table("People")]
public class PersonDbModel
{
    public List<OrganizationDbModel>? AdditionalOrganizations { get; set; } =
        new List<OrganizationDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    public string? MainOrganizationId { get; set; }

    [ForeignKey(nameof(MainOrganizationId))]
    public OrganizationDbModel? MainOrganization { get; set; } = null;

    public RoleEnum? Role { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
