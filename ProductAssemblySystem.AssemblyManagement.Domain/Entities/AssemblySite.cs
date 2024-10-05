using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class AssemblySite
    {
        public Guid Id { get; }
        public Name Name { get; private set; } = null!;

        public Employee Supervisor { get; private set; } = null!;

        private readonly List<Employee> _workers = [];
        public IReadOnlyList<Employee> Workers => _workers;

        private readonly List<Set> _producеSets = [];
        public IReadOnlyList<Set> ProducеSets => _producеSets;

        private readonly List<Part> _producеParts = [];
        public IReadOnlyList<Part> ProducеParts => _producеParts;

        public Storage Storage { get; private set; } = null!;

        public Order CurrentOrder { get; private set; } = null!;

        private readonly List<Order> _completedOrders = [];
        public IReadOnlyList<Order> CompletedOrders => _completedOrders;

        private readonly List<Order> _cancelledOrders = [];
        public IReadOnlyList<Order> CancelledOrders => _cancelledOrders;
    }
}
