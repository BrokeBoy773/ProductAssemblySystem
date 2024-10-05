using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Auxiliaries;
using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; }
        public Name Name { get; private set; } = null!;
        public Customer Customer { get; private set; } = null!;
        public DateTimeOffset OrderCreationDate { get; private set; }
        public AssemblySite AssemblySite { get; private set; } = null!;

        private readonly List<PartQuantity> _orderingParts = [];
        public IReadOnlyList<PartQuantity> OrderingParts => _orderingParts;

        private readonly List<SetQuantity> _orderingSets = [];
        public IReadOnlyList<SetQuantity> OrderingSets => _orderingSets;

        public TimeSpan OrderFulfillmentDate { get; private set; }

        private Order(
            Guid id,
            Name name,
            Customer customer,
            DateTimeOffset orderCreationDate,
            AssemblySite assemblySite,
            List<PartQuantity> orderingParts,
            List<SetQuantity> orderingSets,
            TimeSpan orderFulfillmentDate)
        {
            Id = id;
            Name = name;
            Customer = customer;
            OrderCreationDate = orderCreationDate;
            AssemblySite = assemblySite;
            _orderingParts.AddRange(orderingParts);
            _orderingSets.AddRange(orderingSets);
            OrderFulfillmentDate = orderFulfillmentDate;
        }

        public static Result<Order, List<string>> Create(
            Guid id,
            string name,
            Customer customer,
            DateTimeOffset orderCreationDate,
            AssemblySite assemblySite,
            List<PartQuantity> orderingParts,
            List<SetQuantity> orderingSets)
        {
            List<string> errorsList = [];

            Result<Name, List<string>> resultName = Name.Create(name);

            if (resultName.IsFailure)
                errorsList.AddRange(resultName.Error);


            Result<Customer> resultCustomer = ValidateCustomer(customer);

            if (resultCustomer.IsFailure)
                errorsList.Add(resultCustomer.Error);


            Result<AssemblySite> resultAssemblySite = ValidateAssemblySite(assemblySite);

            if (resultAssemblySite.IsFailure)
                errorsList.Add(resultAssemblySite.Error);


            Result<List<PartQuantity>> resultPartQuantity = ValidateOrderingParts(orderingParts);

            if (resultPartQuantity.IsFailure)
                errorsList.Add(resultPartQuantity.Error);

            Result<List<SetQuantity>> resultSetQuantity = ValidateOrderingSets(orderingSets);

            if (resultSetQuantity.IsFailure)
                errorsList.Add(resultSetQuantity.Error);

            if (resultPartQuantity.Value.Count == 0 && resultSetQuantity.Value.Count == 0)
            {
                errorsList.Add("orderingParts and orderingSets cannot both have zero elements");
                return Result.Failure<Order, List<string>>(errorsList);
            }


            Result<TimeSpan> resultOrderFulfillmentDate = GetOrderFulfillmentDate(orderingParts, orderingSets);


            if (errorsList.Count > 0)
                return Result.Failure<Order, List<string>>(errorsList);

            Order validOrder = new(
                id,
                resultName.Value,
                resultCustomer.Value,
                orderCreationDate,
                assemblySite!,
                orderingParts,
                orderingSets,
                resultOrderFulfillmentDate.Value);

            return Result.Success<Order, List<string>>(validOrder);
        }

        private static Result<TimeSpan> GetOrderFulfillmentDate(
            List<PartQuantity> orderingParts,
            List<SetQuantity> orderingSets)
        {
            TimeSpan orderingPartsFulfillmentDate;

            if (orderingParts.Count == 0)
            {
                orderingPartsFulfillmentDate = TimeSpan.Zero;
            }
            else
            {
                orderingPartsFulfillmentDate = orderingParts
                    .Select(p => p.Part.BuildTime.Time.Multiply(p.Quantity.ItemQuantity))
                    .Aggregate(TimeSpan.Zero, (total, next) => total + next);
            }

            TimeSpan orderingSetsFulfillmentDate;

            if (orderingSets.Count == 0)
            {
                orderingSetsFulfillmentDate = TimeSpan.Zero;
            }
            else
            {
                orderingSetsFulfillmentDate = orderingSets
                    .Select(s => s.Set.BuildTime.Multiply(s.Quantity.ItemQuantity))
                    .Aggregate(TimeSpan.Zero, (total, next) => total + next);
            }

            TimeSpan orderFulfillmentDate = orderingPartsFulfillmentDate + orderingSetsFulfillmentDate;

            return orderFulfillmentDate;
        }

        private static Result<Customer> ValidateCustomer(Customer customer)
        {
            if (customer is null)
                return Result.Failure<Customer>("customer is null");

            return Result.Success(customer);
        }

        private static Result<AssemblySite> ValidateAssemblySite(AssemblySite assemblySite)
        {
            if (assemblySite is null)
                return Result.Failure<AssemblySite>("assemblySite is null");

            return Result.Success(assemblySite);
        }

        private static Result<List<PartQuantity>> ValidateOrderingParts(List<PartQuantity> orderingParts)
        {
            if (orderingParts is null)
                return Result.Failure<List<PartQuantity>>("orderingParts is null");

            return Result.Success(orderingParts);
        }

        private static Result<List<SetQuantity>> ValidateOrderingSets(List<SetQuantity> orderingSets)
        {
            if (orderingSets is null)
                return Result.Failure<List<SetQuantity>>("orderingSets is null");

            return Result.Success(orderingSets);
        }
    }
}
