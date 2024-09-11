namespace GolfService.APIs.Dtos;

public class GameWhereInput
{
    public string? Course { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Player_1 { get; set; }

    public string? Player_2 { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
