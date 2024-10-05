using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using ProductAssemblySystem.AssemblyManagement.Domain.Enums;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.EntitiesTests
{
    public class AssemblySiteTests
    {
        [Fact]
        public void CreateAssemblySite_WithValidInput_ReturnsAssemblySite()
        {
            Result<AssemblySite, List<string>> resultAssemblySite = AssemblySite.Create(
                Guid.NewGuid(),
                "Сборочная площадка",
                Employee.Create(Guid.NewGuid(), Position.Supervisor).Value,
                Storage.Create(Guid.NewGuid()).Value);

            Assert.True(resultAssemblySite.IsSuccess);
        }
    }
}
