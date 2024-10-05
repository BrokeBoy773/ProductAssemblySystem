using CSharpFunctionalExtensions;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; }

        private readonly List<Order>? _currentOrders = null;
        public IReadOnlyList<Order>? CurrentOrders => _currentOrders;

        private readonly List<Order> _completedOrders = [];
        public IReadOnlyList<Order> CompletedOrders => _completedOrders;

        private readonly List<Order> _cancelledOrders = [];
        public IReadOnlyList<Order> CancelledOrders => _cancelledOrders;

        private Customer(Guid id)
        {
            Id = id;
        }

        public static Result<Customer, List<string>> Create(Guid id)
        {
            Customer validCustomer = new(id);

            return Result.Success<Customer, List<string>>(validCustomer);
        }
    }
}
