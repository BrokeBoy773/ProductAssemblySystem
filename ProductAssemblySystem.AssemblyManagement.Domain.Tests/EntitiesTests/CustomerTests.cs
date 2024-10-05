using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.EntitiesTests
{
    public class CustomerTests
    {
        [Fact]
        public void CreateCustomer_WithValidInput_ReturnsCustomer()
        {
            Result<Customer, List<string>> resultCustomer = Customer.Create(Guid.NewGuid());

            Assert.True(resultCustomer.IsSuccess);
        }
    }
}
