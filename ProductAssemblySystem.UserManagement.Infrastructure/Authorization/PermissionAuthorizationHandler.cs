using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ProductAssemblySystem.UserManagement.Domain.Enums;
using ProductAssemblySystem.UserManagement.Infrastructure.Authentication;
using ProductAssemblySystem.UserManagement.Infrastructure.Authorization.Interfaces;
using System.Security.Claims;

namespace ProductAssemblySystem.UserManagement.Infrastructure.Authorization
{
    public class PermissionAuthorizationHandler(
        IServiceScopeFactory serviceScopeFactory)
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        protected override async Task<Result> HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            Claim? userId = context.User.Claims.FirstOrDefault(
                c => c.Type == CustomClaims.UserId);

            if (userId is null || !Guid.TryParse(userId.Value, out Guid parsedId))
                return Result.Failure("userId is null or does not match Guid");

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            Result<HashSet<PermissionEnum>> resultPermissions = await permissionService.GetPermissionsAsync(parsedId);

            if (resultPermissions.IsFailure)
                return Result.Failure(resultPermissions.Error);

            if (!resultPermissions.Value.Intersect(requirement.Permissions).Any())
                return Result.Failure("Additional access rights required");

            context.Succeed(requirement);
            return Result.Success();
        }
    }
}
