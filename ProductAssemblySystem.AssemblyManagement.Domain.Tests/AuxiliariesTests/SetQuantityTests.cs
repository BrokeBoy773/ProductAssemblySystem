using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Auxiliaries;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.AuxiliariesTests
{
    public class SetQuantityTests
    {
        [Fact]
        public void CreateSetQuantity_WithValidInput_ReturnsSetQuantity()
        {
            Result<Part, List<string>> resultPart1 = Part.Create(
                Guid.NewGuid(),
                "Деталь #1",
                "00:25:11");

            Result<Part, List<string>> resultPart2 = Part.Create(
                Guid.NewGuid(),
                "Деталь #2",
                "00:44:32");

            Result<Set, List<string>> resultSet = Set.Create(
                Guid.NewGuid(),
                "Комплект #1",
                [resultPart1.Value, resultPart2.Value]);

            Result<SetQuantity, List<string>> resultSetQuantity = SetQuantity.Create(
                resultSet.Value,
                5);

            Assert.True(resultSetQuantity.IsSuccess);
        }
    }
}
