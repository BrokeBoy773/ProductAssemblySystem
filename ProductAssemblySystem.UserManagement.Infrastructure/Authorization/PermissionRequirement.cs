using Microsoft.AspNetCore.Authorization;
using ProductAssemblySystem.UserManagement.Domain.Enums;

namespace ProductAssemblySystem.UserManagement.Infrastructure.Authorization
{
    public class PermissionRequirement(PermissionEnum[] permissions) : IAuthorizationRequirement
    {
        public PermissionEnum[] Permissions { get; set; } = permissions;
    }
}
