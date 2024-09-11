using GolfService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfService.Infrastructure;

public class GolfServiceDbContext : DbContext
{
    public GolfServiceDbContext(DbContextOptions<GolfServiceDbContext> options)
        : base(options) { }

    public DbSet<GameDbModel> Games { get; set; }

    public DbSet<PlayerDbModel> Players { get; set; }

    public DbSet<CourseDbModel> Courses { get; set; }
}
