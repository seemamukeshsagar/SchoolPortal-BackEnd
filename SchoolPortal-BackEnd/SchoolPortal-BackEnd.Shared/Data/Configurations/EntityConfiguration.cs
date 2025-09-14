using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolPortal.Shared.Data.Configurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public virtual void Configure(EntityTypeBuilder<T> builder) { }
}
