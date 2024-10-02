using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace ProductAssemblySystem.UserManagement.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private static readonly string PhoneNumberPattern = @"^(7|8)(9)([0-9]{9})$";

        public string Number { get; }

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public static Result<PhoneNumber, List<string>> Create(string phoneNumber, List<string> existingPhoneNumbers)
        {
            List<string> errorsList = [];

            Result<string> resultNumber = ValidateNumber(phoneNumber, existingPhoneNumbers);

            if (resultNumber.IsFailure)
                errorsList.Add(resultNumber.Error);

            if (errorsList.Count > 0)
                return Result.Failure<PhoneNumber, List<string>>(errorsList);

            PhoneNumber validPhoneNumber = new(resultNumber.Value);

            return Result.Success<PhoneNumber, List<string>>(validPhoneNumber);
        }

        private static Result<string> ValidateNumber(string phoneNumber, List<string> existingPhoneNumbers)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return Result.Failure<string>("phoneNumber is null or white space");

            string updatedPhoneNumber = phoneNumber.Trim();

            if (!Regex.IsMatch(updatedPhoneNumber, PhoneNumberPattern))
                return Result.Failure<string>("phoneNumber does not match the regular expression");

            if (updatedPhoneNumber.StartsWith('7'))
                updatedPhoneNumber = string.Concat("8", updatedPhoneNumber.AsSpan(1));

            if (existingPhoneNumbers.Any(existing => existing.Equals(updatedPhoneNumber)))
                return Result.Failure<string>("phoneNumber already exists");

            return Result.Success(updatedPhoneNumber);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
