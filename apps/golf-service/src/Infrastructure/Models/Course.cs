using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfService.Infrastructure.Models;

[Table("Courses")]
public class CourseDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public List<GameDbModel>? Games { get; set; } = new List<GameDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(0, 100)]
    public double? Level { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
