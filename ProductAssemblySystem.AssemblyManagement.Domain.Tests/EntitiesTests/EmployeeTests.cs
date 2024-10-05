using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using ProductAssemblySystem.AssemblyManagement.Domain.Enums;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.EntitiesTests
{
    public class EmployeeTests
    {
        [Fact]
        public void CreateEmployee_WithValidInput_ReturnsEmployee()
        {
            Result<AssemblySite, List<string>> resultAssemblySite = AssemblySite.Create(
                Guid.NewGuid(),
                "Сборочная площадка",
                Employee.Create(Guid.NewGuid(), Position.Supervisor).Value,
                Storage.Create(Guid.NewGuid()).Value);

            Result<Employee, List<string>> resultEmployee = Employee.Create(
                Guid.NewGuid(),
                Position.Worker,
                resultAssemblySite.Value);

            Assert.True(resultEmployee.IsSuccess);
        }
    }
}
