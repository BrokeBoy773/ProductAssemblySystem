using ProductAssemblySystem.UserManagement.Domain.Entities;

namespace ProductAssemblySystem.UserManagement.Infrastructure.Authentication.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateJwtSecurityToken(User user);
    }
}
