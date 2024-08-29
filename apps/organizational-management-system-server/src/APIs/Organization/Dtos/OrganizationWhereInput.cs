namespace OrganizationalManagementSystem.APIs.Dtos;

public class OrganizationWhereInput
{
    public string? Address { get; set; }

    public string? BillingAddress { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<string>? MainMembers { get; set; }

    public string? Name { get; set; }

    public string? Persons { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Website { get; set; }
}
