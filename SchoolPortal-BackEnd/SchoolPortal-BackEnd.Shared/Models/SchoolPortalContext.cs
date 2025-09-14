using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public class SchoolPortalContext : DbContext
{
    public SchoolPortalContext(DbContextOptions<SchoolPortalContext> options)
        : base(options)
    {
    }

    // Master Data
    public DbSet<CountryMaster> CountryMasters { get; set; }
    public DbSet<StateMaster> StateMasters { get; set; }
    public DbSet<CityMaster> CityMasters { get; set; }
    
    // Core Entities
    public DbSet<CompanyMaster> CompanyMasters { get; set; }
    public DbSet<SchoolMaster> SchoolMasters { get; set; }
    
    // User Management
    public DbSet<UserDetail> UserDetails { get; set; }
    
    // Other entities can be added as needed
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolPortalContext).Assembly);

        // Any additional configurations that can't be handled by the configuration classes
        modelBuilder.Entity<StateMaster>(entity =>
        {
            entity.HasOne<CountryMaster>()
                .WithMany()
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
