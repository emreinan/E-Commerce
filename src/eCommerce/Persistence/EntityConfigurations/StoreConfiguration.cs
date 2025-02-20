using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StoreConfiguration : BaseEntityConfiguration<Store, Guid>
{
    public override void Configure(EntityTypeBuilder<Store> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
        builder.Property(x => x.LogoUrl).HasMaxLength(500);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.PhoneNumber).IsUnique();
    }
}
