using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace ProductAssemblySystem.UserManagement.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        private static readonly string RemoveWhiteSpacesPattern = @"\s+";
        private static readonly string PostalCodePattern = @"^([0-8]{1})([0-9]{5})$";
        private static readonly string StringWithOnlyLettersPattern = @"^[ a-zA-Zа-яА-ЯЁё.-]+$";
        private static readonly string StringWithLettersAndNumbers = @"^([ 0-9a-zA-Zа-яА-ЯЁё/.-])+$";
        private static readonly string DotOrHyphenPattern = @"^[.-]$";
        private static readonly string SlashOrDotOrHyphenPattern = @"^[/.-]$";

        public string PostalCode { get; }
        public string Region { get; }
        public string City { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string? ApartmentNumber { get; }

        private Address(
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string? apartmentNumber = null)
        {
            PostalCode = postalCode;
            Region = region;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
        }

        public static Result<Address, List<string>> Create(
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string? apartmentNumber = null)
        {
            List<string> errorsList = [];

            Result<string> resultPostalCode = ValidatePostalCode(postalCode);

            if (resultPostalCode.IsFailure)
                errorsList.Add(resultPostalCode.Error);


            Result<string> resultRegion = ValidateRegion(region);

            if (resultRegion.IsFailure)
                errorsList.Add(resultRegion.Error);


            Result<string> resultCity = ValidateCity(city);

            if (resultCity.IsFailure)
                errorsList.Add(resultCity.Error);


            Result<string> resultStreet = ValidateStreet(street);

            if (resultStreet.IsFailure)
                errorsList.Add(resultStreet.Error);


            Result<string> resultHouseNumber = ValidateHouseNumber(houseNumber);

            if (resultHouseNumber.IsFailure)
                errorsList.Add(resultHouseNumber.Error);


            if (apartmentNumber is null)
            {
                if (errorsList.Count > 0)
                    return Result.Failure<Address, List<string>>(errorsList);

                Address validAddressWithNullApartmentNumber = new(
                    resultPostalCode.Value,
                    resultRegion.Value,
                    resultCity.Value,
                    resultStreet.Value,
                    resultHouseNumber.Value,
                    apartmentNumber);

                return Result.Success<Address, List<string>>(validAddressWithNullApartmentNumber);
            }

            Result<string> resultApartmentNumber = ValidateApartmentNumber(apartmentNumber!);

            if (resultApartmentNumber.IsFailure)
                errorsList.Add(resultApartmentNumber.Error);


            if (errorsList.Count > 0)
                return Result.Failure<Address, List<string>>(errorsList);

            Address validAddress = new(
                resultPostalCode.Value,
                resultRegion.Value,
                resultCity.Value,
                resultStreet.Value,
                resultHouseNumber.Value,
                resultApartmentNumber.Value);

            return Result.Success<Address, List<string>>(validAddress);
        }

        private static Result<string> ValidatePostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return Result.Failure<string>("postalCode is null or white space");

            string updatedpostalCode = postalCode.Trim();

            if (!Regex.IsMatch(updatedpostalCode, PostalCodePattern))
                return Result.Failure<string>("postalCode does not match the regular expression");

            return Result.Success(updatedpostalCode);
        }

        private static Result<string> ValidateRegion(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                return Result.Failure<string>("region is null or white space");

            string updatedRegion = Regex.Replace(region, RemoveWhiteSpacesPattern, match => " ").Trim();

            if (updatedRegion.Length > 32)
                return Result.Failure<string>("region exceeds maximum string length");

            if (!Regex.IsMatch(updatedRegion, StringWithOnlyLettersPattern) || Regex.IsMatch(updatedRegion, DotOrHyphenPattern))
                return Result.Failure<string>("region contains invalid characters");

            return Result.Success(updatedRegion);
        }

        private static Result<string> ValidateCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return Result.Failure<string>("city is null or white space");

            string updatedCity = Regex.Replace(city, RemoveWhiteSpacesPattern, match => " ").Trim();

            if (updatedCity.Length > 32)
                return Result.Failure<string>("city exceeds maximum string length");

            if (!Regex.IsMatch(updatedCity, StringWithOnlyLettersPattern) || Regex.IsMatch(updatedCity, DotOrHyphenPattern))
                return Result.Failure<string>("city contains invalid characters");

            return Result.Success(updatedCity);
        }

        private static Result<string> ValidateStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
                return Result.Failure<string>("street is null or white space");

            string updatedStreet = Regex.Replace(street, RemoveWhiteSpacesPattern, match => " ").Trim();

            if (updatedStreet.Length > 32)
                return Result.Failure<string>("street exceeds maximum string length");

            if (!Regex.IsMatch(updatedStreet, StringWithLettersAndNumbers) || Regex.IsMatch(updatedStreet, DotOrHyphenPattern))
                return Result.Failure<string>("street contains invalid characters");

            return Result.Success(updatedStreet);
        }

        private static Result<string> ValidateHouseNumber(string houseNumber)
        {
            if (string.IsNullOrWhiteSpace(houseNumber))
                return Result.Failure<string>("houseNumber is null or white space");

            string updatedHouseNumber = Regex.Replace(houseNumber, RemoveWhiteSpacesPattern, match => " ").Trim();

            if (updatedHouseNumber.Length > 8)
                return Result.Failure<string>("houseNumber exceeds maximum string length");

            if (!Regex.IsMatch(updatedHouseNumber, StringWithLettersAndNumbers) ||
                Regex.IsMatch(updatedHouseNumber, SlashOrDotOrHyphenPattern))
                return Result.Failure<string>("houseNumber contains invalid characters");

            return Result.Success(updatedHouseNumber);
        }

        private static Result<string> ValidateApartmentNumber(string apartmentNumber)
        {
            if (string.IsNullOrWhiteSpace(apartmentNumber))
                return Result.Failure<string>("apartmentNumber is null or white space");

            string updatedApartmentNumber = Regex.Replace(apartmentNumber, RemoveWhiteSpacesPattern, match => " ").Trim();

            if (updatedApartmentNumber.Length > 8)
                return Result.Failure<string>("apartmentNumber exceeds maximum string length");

            if (!Regex.IsMatch(updatedApartmentNumber, StringWithLettersAndNumbers) ||
                Regex.IsMatch(updatedApartmentNumber, SlashOrDotOrHyphenPattern))
                return Result.Failure<string>("apartmentNumber contains invalid characters");

            return Result.Success(updatedApartmentNumber);
        }

#nullable disable
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PostalCode;
            yield return Region;
            yield return City;
            yield return Street;
            yield return HouseNumber;
            yield return ApartmentNumber;
        }
#nullable restore
    }
}
