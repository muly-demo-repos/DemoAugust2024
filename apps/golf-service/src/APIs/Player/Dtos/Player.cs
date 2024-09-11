namespace GolfService.APIs.Dtos;

public class Player
{
    public DateTime CreatedAt { get; set; }

    public string? FirstName { get; set; }

    public List<string>? GamesAsPlayerOne { get; set; }

    public List<string>? GamesAsPlayerTwo { get; set; }

    public string Id { get; set; }

    public string? LastName { get; set; }

    public DateTime UpdatedAt { get; set; }
}
