using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.Shared.Models;

namespace SchoolPortal.Shared.Data.Configurations;

public class CountryMasterConfiguration : EntityConfiguration<CountryMaster>
{
    public override void Configure(EntityTypeBuilder<CountryMaster> builder)
    {
        base.Configure(builder);

        // Configure the relationship with SchoolMaster for BankCountry
        builder.HasMany(d => d.SchoolMasterBankCountries)
            .WithOne(p => p.BankCountryNavigation)
            .HasForeignKey(p => p.BankCountryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with SchoolMaster for Country
        builder.HasMany(d => d.SchoolMasterCountries)
            .WithOne(p => p.CountryNavigation)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with SchoolMaster for JudistrictionCountry
        builder.HasMany(d => d.SchoolMasterJudistrictionCountries)
            .WithOne(p => p.JudistrictionCountryNavigation)
            .HasForeignKey(p => p.JudistrictionCountryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with CompanyMaster
        builder.HasMany(d => d.CompanyMasters)
            .WithOne(p => p.Country)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with StateMaster
        builder.HasMany(d => d.StateMasters)
            .WithOne(p => p.Country)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
