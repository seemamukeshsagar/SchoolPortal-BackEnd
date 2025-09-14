using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.Shared.Models;

namespace SchoolPortal.Shared.Data.Configurations;

public class UserDetailConfiguration : EntityConfiguration<UserDetail>
{
    public override void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        base.Configure(builder);

        // Company relationship
        builder.HasOne(d => d.Company)
            .WithMany()
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        // School relationship
        builder.HasOne(d => d.School)
            .WithMany()
            .HasForeignKey(d => d.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        // UserRole relationship
        builder.HasOne(d => d.UserRole)
            .WithMany()
            .HasForeignKey(d => d.UserRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Designation relationship
        builder.HasOne(d => d.Designation)
            .WithMany()
            .HasForeignKey(d => d.DesignationId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // Self-referential relationship for ModifiedBy
        builder.HasOne(d => d.ModifiedByNavigation)
            .WithMany(p => p.InverseModifiedByNavigation)
            .HasForeignKey(d => d.ModifiedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
