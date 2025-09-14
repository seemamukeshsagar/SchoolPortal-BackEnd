using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.Shared.Models;

namespace SchoolPortal.Shared.Data.Configurations;

public class SchoolMasterConfiguration : EntityConfiguration<SchoolMaster>
{
    public override void Configure(EntityTypeBuilder<SchoolMaster> builder)
    {
        base.Configure(builder);

        // Company relationship
        builder.HasOne(d => d.Company)
            .WithMany(c => c.SchoolMasters)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Main Address relationships
        builder.HasOne(d => d.Country)
            .WithMany()
            .HasForeignKey(d => d.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.State)
            .WithMany()
            .HasForeignKey(d => d.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.City)
            .WithMany()
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Jurisdiction Address relationships
        builder.HasOne(d => d.JudistrictionCountry)
            .WithMany()
            .HasForeignKey(d => d.JudistrictionCountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.JudistrictionState)
            .WithMany()
            .HasForeignKey(d => d.JudistrictionStateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.JudistrictionCity)
            .WithMany()
            .HasForeignKey(d => d.JudistrictionCityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Bank Address relationships
        builder.HasOne(d => d.BankCountry)
            .WithMany()
            .HasForeignKey(d => d.BankCountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.BankState)
            .WithMany()
            .HasForeignKey(d => d.BankStateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.BankCity)
            .WithMany()
            .HasForeignKey(d => d.BankCityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
