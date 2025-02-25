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

        builder.HasOne(x => x.User)
            .WithOne(x => x.Basket)
            .HasForeignKey<Basket>(x => x.UserId);

        builder.HasOne(x => x.Discount)
            .WithMany()
            .HasForeignKey(x => x.DiscountId);

        //// Sipariþ tamamlanmadan önce sepet aktif olmalýdýr
        //// Sipariþ tamamlandýðýnda sepet pasif hale gelir
        //builder.HasIndex(x => new { x.UserId, x.IsActive })
        //       .HasFilter("[UserId] IS NOT NULL AND IsActive = 1")
        //       .IsUnique();

        //builder.HasIndex(x => new { x.GuestId, x.IsActive })
        //       .HasFilter("[GuestId] IS NOT NULL AND IsActive = 1")
        //       .IsUnique();
    }
}