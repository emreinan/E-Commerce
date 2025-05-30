using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BasketConfiguration : BaseEntityConfiguration<Basket, Guid>
{
    public override void Configure(EntityTypeBuilder<Basket> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.GuestId).HasMaxLength(50);
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.HasOne(x => x.Discount)
            .WithMany()
            .HasForeignKey(x => x.DiscountId).OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => new { x.UserId, x.IsActive })
            .IsUnique()
            .HasFilter("[UserId] IS NOT NULL AND IsActive = 1");


        builder.HasIndex(x => new { x.GuestId, x.IsActive })
            .IsUnique()
            .HasFilter("[UserId] IS NOT NULL AND IsActive = 1");

    }
}