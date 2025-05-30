using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DiscountConfiguration : BaseEntityConfiguration<Discount, Guid>
{
    public override void Configure(EntityTypeBuilder<Discount> builder)
    {
        base.Configure(builder);

        builder.Property(d => d.Code).IsRequired().HasMaxLength(20);
        builder.Property(d => d.Value).IsRequired().HasPrecision(5, 2);
        builder.Property(d => d.Type).HasConversion<string>();
        builder.Property(d => d.MinOrderAmount).IsRequired().HasPrecision(5, 2);
        builder.Property(d => d.UsageLimit).IsRequired().HasDefaultValue(1);
        builder.Property(d => d.StartDate).IsRequired().HasColumnType("datetime2");
        builder.Property(d => d.EndDate).IsRequired().HasColumnType("datetime2");
        builder.Property(d => d.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasIndex(d => d.Code)
            .IsUnique()
            .HasFilter("[DeletedDate] IS NULL");
    }
}