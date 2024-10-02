using CSharpFunctionalExtensions;
using ProductAssemblySystem.UserManagement.Domain.Enums;

namespace ProductAssemblySystem.UserManagement.Infrastructure.Authorization.Interfaces
{
    public interface IPermissionService
    {
        Task<Result<HashSet<PermissionEnum>>> GetPermissionsAsync(Guid id);
    }
}
