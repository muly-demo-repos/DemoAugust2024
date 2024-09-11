namespace GolfService.APIs.Dtos;

public class PlayerCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? FirstName { get; set; }

    public List<Game>? GamesAsPlayerOne { get; set; }

    public List<Game>? GamesAsPlayerTwo { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public DateTime UpdatedAt { get; set; }
}
