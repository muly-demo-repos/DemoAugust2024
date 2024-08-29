namespace OrganizationalManagementSystem.APIs.Dtos;

public class OrganizationCreateInput
{
    public string? Address { get; set; }

    public string? BillingAddress { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<Person>? MainMembers { get; set; }

    public string? Name { get; set; }

    public Person? Persons { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Website { get; set; }
}
