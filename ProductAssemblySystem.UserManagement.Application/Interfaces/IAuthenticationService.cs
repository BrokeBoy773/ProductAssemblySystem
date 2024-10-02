using CSharpFunctionalExtensions;

namespace ProductAssemblySystem.UserManagement.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<bool, List<string>>> RegisterAsync(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string apartmentNumber,
            string password,
            string repeatPassword,
            CancellationToken ct);

        Task<Result<string>> LoginAsync(
            string email,
            string password,
            CancellationToken ct);
    }
}
