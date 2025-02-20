using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AddressConfiguration : BaseEntityConfiguration<Address, Guid>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);
        builder.HasIndex(a => a.AddressTitle).IsUnique();

        builder.Property(a => a.GuestId).HasMaxLength(50);
        builder.Property(a => a.AddressTitle).IsRequired().HasMaxLength(20);
        builder.Property(a => a.FullName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(a => a.City).IsRequired().HasMaxLength(50);
        builder.Property(a => a.District).IsRequired().HasMaxLength(50);
        builder.Property(a => a.Street).IsRequired().HasMaxLength(100);
        builder.Property(a => a.ZipCode).HasMaxLength(10);
        builder.Property(a => a.AddressDetail).IsRequired().HasMaxLength(250);

        // Her kullanýcý için sadece bir varsayýlan adres olabilir
        builder.HasIndex(x => new { x.UserId, x.IsDefault })
            .HasFilter("[UserId] IS NOT NULL AND [IsDefault] = 1")
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.UserId)
            .IsRequired(false);
    }
}
