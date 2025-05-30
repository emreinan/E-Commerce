using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductCommentConfiguration : BaseEntityConfiguration<ProductComment, Guid>
{
    public override void Configure(EntityTypeBuilder<ProductComment> builder)
    {
        base.Configure(builder);
        builder.ToTable(tb => tb.HasCheckConstraint("CHK_ProductComment_StarCount", "[StarCount] BETWEEN 1 AND 5"));

        builder.Property(pc => pc.ProductId).IsRequired();
        builder.Property(pc => pc.UserId).IsRequired();
        builder.Property(pc => pc.Text).IsRequired().HasMaxLength(1000);
        builder.Property(pc => pc.StarCount).IsRequired().HasDefaultValue(1);

        builder.HasOne(pc => pc.Product)
            .WithMany(p => p.ProductComments)
            .HasForeignKey(pc => pc.ProductId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User)
            .WithMany(x => x.ProductComments)
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => new { x.ProductId, x.UserId })
            .IsUnique()
            .HasFilter("[DeletedDate] IS NULL AND [ProductId] IS NOT NULL");
    }
}