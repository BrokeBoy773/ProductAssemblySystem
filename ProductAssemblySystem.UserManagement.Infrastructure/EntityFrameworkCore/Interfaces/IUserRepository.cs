using CSharpFunctionalExtensions;
using ProductAssemblySystem.UserManagement.Domain.Entities;
using ProductAssemblySystem.UserManagement.Domain.Enums;

namespace ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces
{
    public interface IUserRepository
    {
        Task<Result> CreateUserAsync(User user, CancellationToken ct);

        Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken ct);

        Task<Result<(List<string> ExistingEmails, List<string> ExistingPhoneNumbers), string>> GetEmailsAndPhoneNumbersAsync(CancellationToken ct);

        Task<Result<List<Role>, string>> GetRolesAsync(CancellationToken ct);

        Task<Result<HashSet<PermissionEnum>>> GetUserPermissions(Guid id);
    }
}
