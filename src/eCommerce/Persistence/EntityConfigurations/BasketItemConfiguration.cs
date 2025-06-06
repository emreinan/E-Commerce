using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BasketItemConfiguration : BaseEntityConfiguration<BasketItem, Guid>
{
    public override void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        base.Configure(builder);
        builder.Property(bi => bi.BasketId).IsRequired();
        builder.Property(bi => bi.ProductId).IsRequired();
        builder.Property(bi => bi.Quantity).IsRequired().HasDefaultValue(1);
        builder.Property(bi => bi.UnitPrice).IsRequired().HasPrecision(18, 2);

        builder.ToTable(tb => tb.HasCheckConstraint("CHK_BasketItem_Quantity", "[Quantity] >= 1"));

        builder.HasOne(bi => bi.Basket)
               .WithMany(b => b.BasketItems)
               .HasForeignKey(bi => bi.BasketId).OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => new { x.BasketId, x.ProductId })
            .IsUnique()
            .HasFilter("[DeletedDate] IS NULL");
    }
}
