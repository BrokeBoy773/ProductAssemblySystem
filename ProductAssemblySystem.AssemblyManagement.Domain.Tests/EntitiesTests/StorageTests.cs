using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.EntitiesTests
{
    public class StorageTests
    {
        [Fact]
        public void CreateStorage_WithValidInput_ReturnsStorage()
        {
            Result<Storage, List<string>> resultStorage = Storage.Create(Guid.NewGuid());

            Assert.True(resultStorage.IsSuccess);
        }
    }
}
