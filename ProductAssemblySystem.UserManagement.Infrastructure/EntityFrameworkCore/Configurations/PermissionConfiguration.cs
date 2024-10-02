using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAssemblySystem.UserManagement.Domain.Entities;
using ProductAssemblySystem.UserManagement.Domain.Enums;

namespace ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(64);

            List<Permission> permissions = Enum
                .GetValues<PermissionEnum>()
                .Select(p => Permission.Create((int)p, p.ToString()).Value)
                .ToList();

            builder.HasData(permissions);
        }
    }
}
