using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        private static readonly string RemoveWhiteSpacesPattern = @"\s+";

        public string ItemName { get; }

        private Name(string itemName)
        {
            ItemName = itemName;
        }

        public static Result<Name, List<string>> Create(string itemName)
        {
            List<string> errorsList = [];

            Result<string> resultItemName = ValidateItemName(itemName);

            if (resultItemName.IsFailure)
                errorsList.Add(resultItemName.Error);

            if (errorsList.Count > 0)
                return Result.Failure<Name, List<string>>(errorsList);

            Name validName = new(resultItemName.Value);

            return Result.Success<Name, List<string>>(validName);
        }

        private static Result<string> ValidateItemName(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                return Result.Failure<string>("itemName is null or white space");

            string updatedItemName = Regex.Replace(itemName, RemoveWhiteSpacesPattern, match => " ").Trim();

            if (updatedItemName.Length > 256)
                return Result.Failure<string>("itemName exceeds maximum string length");

            return Result.Success(updatedItemName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ItemName;
        }
    }
}
