using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductConfiguration : BaseEntityConfiguration<Product, Guid>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.StoreId).IsRequired();
        builder.Property(p => p.CategoryId).IsRequired();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Price).IsRequired().HasPrecision(18, 2);
        builder.Property(p => p.Details).HasMaxLength(500);
        builder.Property(p => p.StockAmount).IsRequired().HasDefaultValue(0);
        builder.Property(p => p.Enabled).IsRequired().HasDefaultValue(true);

        builder.ToTable(tb => tb.HasCheckConstraint("CHK_Product_StockAmount", "[StockAmount] >= 0"));
        builder.ToTable(tb => tb.HasCheckConstraint("CHK_Product_Price", "[Price] > 0"));

        builder.HasIndex(x => new { x.StoreId, x.Name })
            .IsUnique()
            .HasFilter("[DeletedDate] IS NULL AND [StoreId] IS NOT NULL");
        // A store can have only one product with the same name

        builder.HasOne(x => x.Store)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.StoreId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.ProductImages)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.ProductComments)
            .WithOne(pc => pc.Product)
            .HasForeignKey(pc => pc.ProductId).OnDelete(DeleteBehavior.NoAction);
    }
}