using CSharpFunctionalExtensions;

namespace ProductAssemblySystem.UserManagement.Domain.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        public string Hash { get; }

        private PasswordHash(string hash)
        {
            Hash = hash;
        }

        public static Result<PasswordHash, List<string>> Create(string hash)
        {
            List<string> errorsList = [];

            Result<string> resultHash = ValidateHash(hash);

            if (resultHash.IsFailure)
                errorsList.Add(resultHash.Error);

            if (errorsList.Count > 0)
                return Result.Failure<PasswordHash, List<string>>(errorsList);

            PasswordHash validPasswordHash = new(resultHash.Value);

            return Result.Success<PasswordHash, List<string>>(validPasswordHash);
        }

        private static Result<string> ValidateHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return Result.Failure<string>("hash is null or white space");

            return Result.Success(hash);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
        }
    }
}
