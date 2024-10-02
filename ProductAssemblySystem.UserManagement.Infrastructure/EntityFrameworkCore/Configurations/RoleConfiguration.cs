using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAssemblySystem.UserManagement.Domain.Entities;
using ProductAssemblySystem.UserManagement.Domain.Enums;

namespace ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>(
                    right => right.HasOne<Permission>().WithMany().HasForeignKey(p => p.PermissionId),
                    left => left.HasOne<Role>().WithMany().HasForeignKey(r => r.RoleId));

            builder.Property(r => r.Name).IsRequired().HasMaxLength(64);

            List<Role> roles = Enum
                .GetValues<RoleEnum>()
                .Select(r => Role.Create((int)r, r.ToString()).Value)
                .ToList();

            builder.HasData(roles);
        }
    }
}
