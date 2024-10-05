using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Auxiliaries;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.AuxiliariesTests
{
    public class PartQuantityTests
    {
        [Fact]
        public void CreatePartQuantity_WithValidInput_ReturnsPartQuantity()
        {
            Result<Part, List<string>> resultPart = Part.Create(
                Guid.NewGuid(),
                "Деталь #1",
                "00:25:11");

            Result<PartQuantity, List<string>> resultPartQuantity = PartQuantity.Create(
                resultPart.Value,
                25);

            Assert.True(resultPartQuantity.IsSuccess);
        }
    }
}
