using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.Shared.Models;

namespace SchoolPortal.Shared.Data.Configurations;

public class StateMasterConfiguration : EntityConfiguration<StateMaster>
{
    public override void Configure(EntityTypeBuilder<StateMaster> builder)
    {
        base.Configure(builder);

        // Configure the relationship with CountryMaster
        builder.HasOne(d => d.Country)
            .WithMany(p => p.StateMasters)
            .HasForeignKey(d => d.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with CityMaster
        builder.HasMany(d => d.CityMasters)
            .WithOne(p => p.CityState)
            .HasForeignKey(p => p.CityStateId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with SchoolMaster for State
        builder.HasMany(d => d.SchoolMasterStates)
            .WithOne(p => p.StateNavigation)
            .HasForeignKey(p => p.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with SchoolMaster for JudistrictionState
        builder.HasMany(d => d.SchoolMasterJudistrictionStates)
            .WithOne(p => p.JudistrictionStateNavigation)
            .HasForeignKey(p => p.JudistrictionStateId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship with SchoolMaster for BankState
        builder.HasMany(d => d.SchoolMasterBankStates)
            .WithOne(p => p.BankStateNavigation)
            .HasForeignKey(p => p.BankStateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
