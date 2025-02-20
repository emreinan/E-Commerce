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
        builder.Property(d => d.Amount).HasPrecision(18, 2);
        builder.Property(d => d.Percentage).HasPrecision(5, 2);
        builder.Property(d => d.MinOrderAmount).IsRequired().HasColumnType("tinyint").HasDefaultValue(0);
        builder.Property(d => d.UsageLimit).IsRequired().HasDefaultValue(1);
        builder.Property(d => d.StartDate).IsRequired().HasColumnType("datetime2");
        builder.Property(d => d.EndDate).IsRequired().HasColumnType("datetime2");
        builder.Property(d => d.IsActive).IsRequired().HasDefaultValue(true);

        builder.ToTable(tb => tb.HasCheckConstraint("CK_Discount_Percentage", "[Percentage] BETWEEN 0 AND 100"));

        builder.HasIndex(d => d.Code).IsUnique();
    }
}