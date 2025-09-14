using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.Shared.Models;

namespace SchoolPortal.Shared.Data.Configurations;

public class CompanyMasterConfiguration : EntityConfiguration<CompanyMaster>
{
    public override void Configure(EntityTypeBuilder<CompanyMaster> builder)
    {
        base.Configure(builder);

        builder.HasOne(d => d.Country)
            .WithMany()
            .HasForeignKey(d => d.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.State)
            .WithMany()
            .HasForeignKey(d => d.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.City)
            .WithMany(c => c.CompanyMasterCities)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.JudistrictionAreaNavigation)
            .WithMany(c => c.CompanyMasterJudistrictionAreaNavigations)
            .HasForeignKey(d => d.JudistrictionArea)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
