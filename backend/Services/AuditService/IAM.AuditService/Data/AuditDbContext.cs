using Microsoft.EntityFrameworkCore;
using IAM.AuditService.Models;

namespace IAM.AuditService.Data;

public class AuditDbContext : DbContext
{
    public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }

    public DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditEntry>().HasKey(x => x.Id);
        modelBuilder.Entity<AuditEntry>().HasIndex(x => x.Timestamp);
    }
}
