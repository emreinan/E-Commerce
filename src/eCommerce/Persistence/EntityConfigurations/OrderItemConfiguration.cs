using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderItemConfiguration : BaseEntityConfiguration<OrderItem, Guid>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);
        builder.Property(oi => oi.OrderId).IsRequired();
        builder.Property(oi => oi.ProductId).IsRequired();
        builder.Property(oi => oi.ProductNameAtOrderTime).IsRequired().HasMaxLength(100);
        builder.Property(oi => oi.ProductPriceAtOrderTime).IsRequired().HasPrecision(18, 2);
        builder.Property(oi => oi.Quantity).IsRequired().HasDefaultValue(1);
        builder.Ignore(oi => oi.TotalPrice); // Derived Property, veritabanýnda tutulmuyor.

        // builder.Property(oi => oi.TotalPrice)
        //.HasComputedColumnSql("[ProductPriceAtOrderTime] * [Quantity]", stored: true);

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);
    }
}