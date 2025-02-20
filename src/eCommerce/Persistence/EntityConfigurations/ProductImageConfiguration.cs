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
        builder.Property(pi => pi.ImageUrl2).HasMaxLength(500);

        builder.HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId).OnDelete(DeleteBehavior.NoAction);

        // A product can have only one main image
        builder.HasIndex(x => new { x.ProductId, x.IsMain })
            .HasFilter("[IsMain] = 1")
            .IsUnique();
    }
}