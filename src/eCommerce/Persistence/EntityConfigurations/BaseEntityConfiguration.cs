using Core.Persistence.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public abstract class BaseEntityConfiguration<T, TId> : IEntityTypeConfiguration<T> where T : Entity<TId>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.CreatedDate).IsRequired().ValueGeneratedOnAdd();
        builder.Property(a => a.UpdatedDate).IsRequired(false).ValueGeneratedOnUpdate();
        builder.Property(a => a.DeletedDate).IsRequired(false);
        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}