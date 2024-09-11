namespace GolfService.APIs.Dtos;

public class GameCreateInput
{
    public Course? Course { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Player? Player_1 { get; set; }

    public Player? Player_2 { get; set; }

    public DateTime UpdatedAt { get; set; }
}
