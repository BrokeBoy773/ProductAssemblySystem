using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Enums;
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

        public Order? CurrentOrder { get; private set; }

        private readonly List<Order> _completedOrders = [];
        public IReadOnlyList<Order> CompletedOrders => _completedOrders;

        private readonly List<Order> _cancelledOrders = [];
        public IReadOnlyList<Order> CancelledOrders => _cancelledOrders;

        private AssemblySite(
            Guid id,
            Name name,
            Employee supervisor,
            Storage storage)
        {
            Id = id;
            Name = name;
            Supervisor = supervisor;
            Storage = storage;
        }

        public static Result<AssemblySite, List<string>> Create(
            Guid id,
            string name,
            Employee supervisor,
            Storage storage)
        {
            List<string> errorsList = [];

            Result<Name, List<string>> resultName = Name.Create(name);

            if (resultName.IsFailure)
                errorsList.AddRange(resultName.Error);


            Result<Employee> resultSupervisor = ValidateSupervisor(supervisor);

            if (resultSupervisor.IsFailure)
                errorsList.Add(resultSupervisor.Error);


            Result<Storage> resultStorage = ValidateStorage(storage);

            if (resultStorage.IsFailure)
                errorsList.Add(resultStorage.Error);


            if (errorsList.Count > 0)
                return Result.Failure<AssemblySite, List<string>>(errorsList);

            AssemblySite validAssemblySite = new(
                id,
                resultName.Value,
                resultSupervisor.Value,
                resultStorage.Value);

            return Result.Success<AssemblySite, List<string>>(validAssemblySite);
        }

        private static Result<Employee> ValidateSupervisor(Employee supervisor)
        {
            if (supervisor is null)
                return Result.Failure<Employee>("supervisor is null");

            if (supervisor.Position != Position.Supervisor)
                return Result.Failure<Employee>("Employee must be qualified for the position of \"Supervisor\"");

            return Result.Success(supervisor);
        }

        private static Result<Storage> ValidateStorage(Storage storage)
        {
            if (storage is null)
                return Result.Failure<Storage>("storage is null");

            return Result.Success(storage);
        }
    }
}
