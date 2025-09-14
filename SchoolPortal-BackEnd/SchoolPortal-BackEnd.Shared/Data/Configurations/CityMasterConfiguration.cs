using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.Shared.Models;

namespace SchoolPortal.Shared.Data.Configurations;

public class CityMasterConfiguration : EntityConfiguration<CityMaster>
{
    public override void Configure(EntityTypeBuilder<CityMaster> builder)
    {
        base.Configure(builder);

        // Configure the relationship with StateMaster
        builder.HasOne(d => d.CityStateNavigation)
            .WithMany(p => p.CityMasters)
            .HasForeignKey(d => d.CityStateId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // Configure the relationship with CompanyMaster for City
        builder.HasMany(d => d.CompanyMasterCities)
            .WithOne(p => p.City)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with SchoolMaster for City
        builder.HasMany(d => d.SchoolMasterCities)
            .WithOne(p => p.CityNavigation)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with SchoolMaster for JudistrictionCity
        builder.HasMany(d => d.SchoolMasterJudistrictionCities)
            .WithOne(p => p.JudistrictionCityNavigation)
            .HasForeignKey(p => p.JudistrictionCityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with SchoolMaster for BankCity
        builder.HasMany(d => d.SchoolMasterBankCities)
            .WithOne(p => p.BankCityNavigation)
            .HasForeignKey(p => p.BankCityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
