using IAM.IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IAM.IdentityService.Data;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    public DbSet<Identity> Identities => Set<Identity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Identity>().HasKey(x => x.Id);
        modelBuilder.Entity<Identity>().HasIndex(x => x.Username).IsUnique();
    }
}
