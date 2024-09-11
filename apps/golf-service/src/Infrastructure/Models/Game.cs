using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfService.Infrastructure.Models;

[Table("Games")]
public class GameDbModel
{
    public string? CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    public CourseDbModel? Course { get; set; } = null;

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? Player_1Id { get; set; }

    [ForeignKey(nameof(Player_1Id))]
    public PlayerDbModel? Player_1 { get; set; } = null;

    public string? Player_2Id { get; set; }

    [ForeignKey(nameof(Player_2Id))]
    public PlayerDbModel? Player_2 { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
