using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.EntitiesTests
{
    public class SetTests
    {
        [Fact]
        public void CombinePartsBuildTime_WithThreeParts_ReturnsTimeSpan()
        {
            Result<Part, List<string>> resultPart1 = Part.Create(Guid.NewGuid(), "Деталь #1", "01:12:02");
            Result<Part, List<string>> resultPart2 = Part.Create(Guid.NewGuid(), "Деталь #2", "09:05:10");
            Result<Part, List<string>> resultPart3 = Part.Create(Guid.NewGuid(), "Деталь #3", "02:30:33");

            Result<TimeSpan> resultTimeSpan = Set.CombinePartsBuildTime([resultPart1.Value, resultPart2.Value, resultPart3.Value]);

            Assert.True(resultTimeSpan.IsSuccess);
            Assert.Equal(TimeSpan.Parse("12:47:45"), resultTimeSpan.Value);
        }

        [Fact]
        public void CreateSet_WithValidInput_ReturnsSet()
        {
            Result<Part, List<string>> resultPart1 = Part.Create(Guid.NewGuid(), "Деталь #1", "01:22:31");
            Result<Part, List<string>> resultPart2 = Part.Create(Guid.NewGuid(), "Деталь #2", "02:14:52");

            string itemName = "Набор";

            Result<Set, List<string>> resultSet = Set.Create(Guid.NewGuid(), itemName, [resultPart1.Value, resultPart2.Value]);

            Assert.True(resultSet.IsSuccess);
        }

        [Fact]
        public void CreateSet_WithoutParts_ReturnsSet()
        {
            string itemName = "Набор";

            Result<Set, List<string>> resultSet = Set.Create(Guid.NewGuid(), itemName, []);

            Assert.True(resultSet.IsFailure);
        }

        [Fact]
        public void CreateSet_WithOnePart_ReturnsSet()
        {
            Result<Part, List<string>> resultPart1 = Part.Create(Guid.NewGuid(), "Деталь #1", "01:22:31");

            string itemName = "Набор";

            Result<Set, List<string>> resultSet = Set.Create(Guid.NewGuid(), itemName, [resultPart1.Value]);

            Assert.True(resultSet.IsFailure);
        }
    }
}
