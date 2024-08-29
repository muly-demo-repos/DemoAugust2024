using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationalManagementSystem.Infrastructure.Models;

[Table("Organizations")]
public class OrganizationDbModel
{
    [StringLength(1000)]
    public string? Address { get; set; }

    [StringLength(1000)]
    public string? BillingAddress { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<PersonDbModel>? MainMembers { get; set; } = new List<PersonDbModel>();

    [StringLength(1000)]
    public string? Name { get; set; }

    public string? PersonsId { get; set; }

    [ForeignKey(nameof(PersonsId))]
    public PersonDbModel? Persons { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Website { get; set; }
}
