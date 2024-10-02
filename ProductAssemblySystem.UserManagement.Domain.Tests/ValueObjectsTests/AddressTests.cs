using CSharpFunctionalExtensions;
using ProductAssemblySystem.UserManagement.Domain.ValueObjects;
using Xunit;

namespace ProductAssemblySystem.UserManagement.Domain.Tests.ValueObjectsTests
{
    public class AddressTests
    {
        [Fact]
        public void CreateAddress_WithNullApartmentNumber_ReturnsAddressValueObject()
        {
            string postalCode = "101000";
            string region = "осковская обл.";
            string city = "г. Москва";
            string street = "3-я ул. Марьиной Рощи";
            string houseNumber = "6";

            Result<Address, List<string>> result = Address.Create(postalCode, region, city, street, houseNumber);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData("101000", "Московская обл.", "г. Москва", "3-я ул. Марьиной Рощи", "6", "10")]
        [InlineData("660122", "Красноярский край", "город Красноярск", "улица 60 лет Октября", "161Г", "12")]
        public void CreateAddress_WithAllPropertiesValid_ReturnsAddressValueObject(
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string apartmentNumber)
        {
            Result<Address, List<string>> result = Address.Create(postalCode, region, city, street, houseNumber, apartmentNumber);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData("  101000  ", " Московская  обл. ", "  г.  Москва  ", " 3-я   ул.  Марьиной   Рощи ", " 6 ", "  10 ")]
        public void CreateAddress_WithAllPropertiesValidWithSpaces_ReturnsAddressValueObject(
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string apartmentNumber)
        {
            Result<Address, List<string>> result = Address.Create(postalCode, region, city, street, houseNumber, apartmentNumber);

            Assert.Equal("101000", result.Value.PostalCode);
            Assert.Equal("Московская обл.", result.Value.Region);
            Assert.Equal("г. Москва", result.Value.City);
            Assert.Equal("3-я ул. Марьиной Рощи", result.Value.Street);
            Assert.Equal("6", result.Value.HouseNumber);
            Assert.Equal("10", result.Value.ApartmentNumber);
        }

        [Fact]
        public void CreateAddress_WithEmptyStrings_ReturnsAddressValueObject()
        {
            string postalCode = "";
            string region = "";
            string city = "";
            string street = "";
            string houseNumber = "";
            string apartmentNumber = "";

            Result<Address, List<string>> result = Address.Create(postalCode, region, city, street, houseNumber, apartmentNumber);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateAddress_WithOnlySpaces_ReturnsAddressValueObject()
        {
            string postalCode = "     ";
            string region = "    ";
            string city = "     ";
            string street = "    ";
            string houseNumber = "    ";
            string apartmentNumber = "   ";

            Result<Address, List<string>> result = Address.Create(postalCode, region, city, street, houseNumber, apartmentNumber);

            Assert.True(result.IsFailure);
        }

#nullable disable
        [Fact]
        public void CreateAddress_WithNullStrings_ReturnsAddressValueObject()
        {
            string postalCode = null;
            string region = null;
            string city = null;
            string street = null;
            string houseNumber = null;
            string apartmentNumber = null;

            Result<Address, List<string>> result = Address.Create(postalCode, region, city, street, houseNumber, apartmentNumber);

            Assert.True(result.IsFailure);
        }
#nullable restore
    }
}
