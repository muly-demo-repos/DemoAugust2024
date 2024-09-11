using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfService.Infrastructure.Models;

[Table("Players")]
public class PlayerDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    public List<GameDbModel>? GamesAsPlayerOne { get; set; } = new List<GameDbModel>();

    public List<GameDbModel>? GamesAsPlayerTwo { get; set; } = new List<GameDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
