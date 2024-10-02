using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAssemblySystem.UserManagement.Domain.Entities;
using ProductAssemblySystem.UserManagement.Domain.Enums;
using ProductAssemblySystem.UserManagement.Infrastructure.Authorization;

namespace ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Configurations
{
    public class RolePermissionConfiguration(AuthorizationOptions authOptions) : IEntityTypeConfiguration<RolePermission>
    {
        private readonly AuthorizationOptions _authOptions = authOptions;

        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.HasData(ParseRolePermissions());
        }

        private RolePermission[] ParseRolePermissions()
        {
            return _authOptions.RolePermissions
                .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermission
                {
                    RoleId = (int)Enum.Parse<RoleEnum>(rp.Role),
                    PermissionId = (int)Enum.Parse<PermissionEnum>(p)
                }))
                .ToArray();
        }
    }
}
