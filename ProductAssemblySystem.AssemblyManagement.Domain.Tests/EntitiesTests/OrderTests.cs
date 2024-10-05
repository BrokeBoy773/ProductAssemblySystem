using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Auxiliaries;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using ProductAssemblySystem.AssemblyManagement.Domain.Enums;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.EntitiesTests
{
    public class OrderTests
    {
        [Fact]
        public void CreateOrder_WithValidInput_ReturnsOrder()
        {
            Result<Customer, List<string>> resultCustomer = Customer.Create(Guid.NewGuid());

            Result<AssemblySite, List<string>> resultAssemblySite = AssemblySite.Create(
                Guid.NewGuid(),
                "Сборочная площадка",
                Employee.Create(Guid.NewGuid(), Position.Supervisor).Value,
                Storage.Create(Guid.NewGuid()).Value);

            Result<PartQuantity, List<string>> resultPartQuantity1 = PartQuantity.Create(
                Part.Create(Guid.NewGuid(), "Деталь #1", "00:10:10").Value, 5);

            Result<PartQuantity, List<string>> resultPartQuantity2 = PartQuantity.Create(
                Part.Create(Guid.NewGuid(), "Деталь #2", "00:05:10").Value, 10);

            Result<Part, List<string>> resultPart1 = Part.Create(Guid.NewGuid(), "Деталь #3", "00:02:05").Value;
            Result<Part, List<string>> resultPart2 = Part.Create(Guid.NewGuid(), "Деталь #4", "00:03:00").Value;

            Result<SetQuantity, List<string>> resultSetQuantity = SetQuantity.Create(
                Set.Create(Guid.NewGuid(), "Комплект", [resultPart1.Value, resultPart2.Value]).Value, 2);


            Result<Order, List<string>> resultOrder = Order.Create(
                Guid.NewGuid(),
                "Заказ #1",
                resultCustomer.Value,
                DateTimeOffset.UtcNow,
                resultAssemblySite.Value,
                [resultPartQuantity1.Value, resultPartQuantity2.Value],
                [resultSetQuantity.Value]);

            Assert.Equal(TimeSpan.Parse("01:52:40"), resultOrder.Value.OrderFulfillmentDate);
            Assert.True(resultOrder.IsSuccess);
        }
    }
}
