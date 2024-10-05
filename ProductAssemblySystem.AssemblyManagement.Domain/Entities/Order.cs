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
        public TimeSpan OrderCreationDate { get; private set; }
        public AssemblySite AssemblySite { get; private set; } = null!;

        private readonly List<PartQuantity> _orderingParts = [];
        public IReadOnlyList<PartQuantity> OrderingParts => _orderingParts;

        private readonly List<SetQuantity> _orderingSets = [];
        public IReadOnlyList<SetQuantity> OrderingSets => _orderingSets;

        public TimeSpan OrderFulfillmentDate { get; private set; }

        private Order(
            Guid id,
            Name name,
            TimeSpan orderCreationDate,
            AssemblySite assemblySite,
            List<PartQuantity> orderingParts,
            List<SetQuantity> orderingSets,
            TimeSpan orderFulfillmentDate)
        {
            Id = id;
            Name = name;
            OrderCreationDate = orderCreationDate;
            AssemblySite = assemblySite;
            _orderingParts.AddRange(orderingParts);
            _orderingSets.AddRange(orderingSets);
            OrderFulfillmentDate = orderFulfillmentDate;
        }

        public static Result<Order, List<string>> Create(
            Guid id,
            string name,
            TimeSpan orderCreationDate,
            AssemblySite assemblySite,
            List<PartQuantity> orderingParts,
            List<SetQuantity> orderingSets)
        {
            List<string> errorsList = [];

            Result<Name, List<string>> resultName = Name.Create(name);

            if (resultName.IsFailure)
                errorsList.AddRange(resultName.Error);


            if (assemblySite is null)
                errorsList.Add("assemblySite is null");


            Result resultOrderingPartsAndOrderingSets = ValidateOrderingPartsAndOrderingSets(orderingParts, orderingSets);

            if (resultOrderingPartsAndOrderingSets.IsFailure)
            {
                errorsList.Add(resultOrderingPartsAndOrderingSets.Error);
                return Result.Failure<Order, List<string>>(errorsList);
            }


            Result<TimeSpan> resultOrderFulfillmentDate = GetOrderFulfillmentDate(orderingParts, orderingSets);


            if (errorsList.Count > 0)
                return Result.Failure<Order, List<string>>(errorsList);

            Order validOrder = new(
                id,
                resultName.Value,
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
            return Result.Success(TimeSpan.Zero);
        }

        private static Result ValidateOrderingPartsAndOrderingSets(
            List<PartQuantity> orderingParts,
            List<SetQuantity> orderingSets)
        {
            if (orderingParts is null)
                return Result.Failure("orderingParts is null");

            if (orderingSets is null)
                return Result.Failure("orderingSets is null");

            if (orderingParts!.Count == 0 && orderingSets!.Count == 0)
                return Result.Failure("orderingParts and orderingSets cannot both have zero elements");

            return Result.Success();
        }
    }
}
