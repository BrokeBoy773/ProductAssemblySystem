using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.EntitiesTests
{
    public class PartTests
    {
        [Fact]
        public void CreatePart_WithValidInput_ReturnsPart()
        {
            string timeString = "01:22:31";
            string itemName = "Деталь";

            Result<Part, List<string>> resultPart = Part.Create(Guid.NewGuid(), itemName, timeString);

            Assert.True(resultPart.IsSuccess);
        }
    }
}
