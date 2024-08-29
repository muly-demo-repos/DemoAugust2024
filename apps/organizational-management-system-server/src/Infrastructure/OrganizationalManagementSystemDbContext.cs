using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrganizationalManagementSystem.Infrastructure.Models;

namespace OrganizationalManagementSystem.Infrastructure;

public class OrganizationalManagementSystemDbContext : IdentityDbContext<IdentityUser>
{
    public OrganizationalManagementSystemDbContext(
        DbContextOptions<OrganizationalManagementSystemDbContext> options
    )
        : base(options) { }

    public DbSet<OrganizationDbModel> Organizations { get; set; }

    public DbSet<PersonDbModel> People { get; set; }
}
