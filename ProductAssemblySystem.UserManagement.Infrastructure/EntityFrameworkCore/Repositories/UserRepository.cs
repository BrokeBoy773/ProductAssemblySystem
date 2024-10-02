using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using ProductAssemblySystem.UserManagement.Domain.Entities;
using ProductAssemblySystem.UserManagement.Domain.Enums;
using ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces;

namespace ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Repositories
{
    public class UserRepository(UserManagementDbContext dbContext) : IUserRepository
    {
        private readonly UserManagementDbContext _dbContext = dbContext;

        public async Task<Result> CreateUserAsync(User user, CancellationToken ct)
        {
            if (user is null)
                return Result.Failure("user is null");

            await _dbContext.AddAsync(user, ct);
            await _dbContext.SaveChangesAsync(ct);

            return Result.Success();
        }

        public async Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<User>("email is null or white space");

            User? user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.EmailAddress == email, ct);

            if (user is null)
                return Result.Failure<User>("user not found");
            return Result.Success(user);
        }

        public async Task<Result<(List<string> ExistingEmails, List<string> ExistingPhoneNumbers), string>> GetEmailsAndPhoneNumbersAsync(CancellationToken ct)
        {
            List<User> users = await _dbContext.Users.ToListAsync(ct);

            if (users is null)
                return Result.Failure<(List<string>, List<string>), string>("users is null");

            List<string> existingEmails = users.Select(user => user.Email.EmailAddress).ToList();
            List<string> existingPhoneNumbers = users.Select(user => user.PhoneNumber.Number).ToList();

            return Result.Success<(List<string>, List<string>), string>((existingEmails, existingPhoneNumbers));
        }

        public async Task<Result<List<Role>, string>> GetRolesAsync(CancellationToken ct)
        {
            List<Role> roles = await _dbContext.Roles.ToListAsync(ct);

            if (roles is null || roles.Count == 0)
                return Result.Failure<List<Role>, string>("roles are null or contain no elements");

            return Result.Success<List<Role>, string>(roles);
        }

        public async Task<Result<HashSet<PermissionEnum>>> GetUserPermissions(Guid id)
        {
            IReadOnlyList<Role>? roles = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == id)
                .Select(u => u.Roles)
                .FirstOrDefaultAsync();

            if (roles is null)
                return Result.Failure<HashSet<PermissionEnum>>("roles is null");

            HashSet<PermissionEnum> permissions = roles
                .SelectMany(r => r.Permissions)
                .Select(p => (PermissionEnum)p.Id)
                .ToHashSet();

            if (permissions is null)
                return Result.Failure<HashSet<PermissionEnum>>("permissions is null");

            return Result.Success(permissions);
        }
    }
}
