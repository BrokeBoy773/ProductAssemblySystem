using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.ValueObjectsTests
{
    public class NameTests
    {
        [Fact]
        public void CreateName_WithValidInput_ReturnsNameValueObject()
        {
            string itemName = "Деталь";

            Result<Name, List<string>> resultName = Name.Create(itemName);

            Assert.True(resultName.IsSuccess);
        }

        [Fact]
        public void CreateName_WithEmptyString_ReturnsNameValueObject()
        {
            string itemName = "";

            Result<Name, List<string>> resultName = Name.Create(itemName);

            Assert.True(resultName.IsFailure);
        }

#nullable disable
        [Fact]
        public void CreateName_WithNullString_ReturnsNameValueObject()
        {
            string itemName = null;

            Result<Name, List<string>> resultName = Name.Create(itemName);

            Assert.True(resultName.IsFailure);
        }
#nullable restore

        [Fact]
        public void CreateName_WithOnlySpaces_ReturnsNameValueObject()
        {
            string itemName = "      ";

            Result<Name, List<string>> resultName = Name.Create(itemName);

            Assert.True(resultName.IsFailure);
        }
    }
}
