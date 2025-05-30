using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductImageConfiguration : BaseEntityConfiguration<ProductImage, Guid>
{
    public override void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        base.Configure(builder);

        builder.Property(pi => pi.ProductId).IsRequired();
        builder.Property(pi => pi.ImageUrl).IsRequired().HasMaxLength(500);

        builder.HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId).OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(pi => new { pi.ProductId, pi.IsMain })
                   .IsUnique()
                   .HasFilter("[IsMain] = 1"); // Her ürün için yalnýzca bir main fotoðraf
    }
}