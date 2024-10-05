using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.ValueObjectsTests
{
    public class QuantityTests
    {
        [Fact]
        public void CreateQuantity_WithValidInput_ReturnsQuantityValueObject()
        {
            int quantity = 1;

            Result<Quantity, List<string>> resultQuantity = Quantity.Create(quantity);

            Assert.True(resultQuantity.IsSuccess);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateQuantity_WithInvalidInput_ReturnsQuantityValueObject(int quantity)
        {
            Result<Quantity, List<string>> resultQuantity = Quantity.Create(quantity);

            Assert.True(resultQuantity.IsFailure);
        }
    }
}
