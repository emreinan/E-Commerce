using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RoleConfiguration : BaseEntityConfiguration<Role, Guid>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(15);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
