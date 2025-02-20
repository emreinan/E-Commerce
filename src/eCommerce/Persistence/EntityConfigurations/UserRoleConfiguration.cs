using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserRoleConfiguration : BaseEntityConfiguration<UserRole, Guid>
{
    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.RoleId).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId);

        builder.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
    }
}
