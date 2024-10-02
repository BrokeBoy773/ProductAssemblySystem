using CSharpFunctionalExtensions;
using ProductAssemblySystem.UserManagement.Domain.Enums;
using ProductAssemblySystem.UserManagement.Infrastructure.Authorization.Interfaces;
using ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces;

namespace ProductAssemblySystem.UserManagement.Infrastructure.Authorization
{
    public class PermissionService(IUserRepository userRepository) : IPermissionService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<HashSet<PermissionEnum>>> GetPermissionsAsync(Guid id)
        {
            Result<HashSet<PermissionEnum>> resultPermissions = await _userRepository.GetUserPermissions(id);

            if (resultPermissions.IsFailure)
                return Result.Failure<HashSet<PermissionEnum>>(resultPermissions.Error);

            return Result.Success(resultPermissions.Value);
        }
    }
}
