using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderConfiguration : BaseEntityConfiguration<Order, Guid>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        builder.HasIndex(o => o.OrderCode).IsUnique();

        builder.Property(o => o.OrderCode).IsRequired().HasMaxLength(50);
        builder.Property(x => x.GuestId).HasMaxLength(50);
        builder.Property(x => x.GuestEmail).HasMaxLength(100);
        builder.Property(x => x.GuestPhoneNumber).HasMaxLength(20);

        builder.Property(o => o.OrderDate).IsRequired().HasColumnType("datetime2");
        builder.Property(o => o.TaxAmount).IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.ShippingCost).IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.FinalAmount).IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.Status).IsRequired();
        builder.Property(o => o.PaymentMethod).IsRequired();

        builder.HasOne(x => x.User)
               .WithMany(x => x.Orders)
               .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.ShippingAddress)
               .WithMany()
               .HasForeignKey(x => x.ShippingAddressId);

        builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Discount)
               .WithMany()
               .HasForeignKey(x => x.DiscountId);
    }
}
