namespace GolfService.APIs.Dtos;

public class Course
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public List<string>? Games { get; set; }

    public string Id { get; set; }

    public double? Level { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
