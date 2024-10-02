using ProductAssemblySystem.UserManagement.Infrastructure.Authentication.Interfaces;

namespace ProductAssemblySystem.UserManagement.Infrastructure.Authentication
{
    public class PasswordHasher
    {
        public static string GenerateHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
    }
}
