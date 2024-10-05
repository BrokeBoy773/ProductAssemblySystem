using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Auxiliaries
{
    public class SetQuantity
    {
        public Set Set { get; } = null!;
        public Quantity Quantity { get; } = null!;

        private SetQuantity(
            Set set,
            Quantity quantity)
        {
            Set = set;
            Quantity = quantity;
        }

        public static Result<SetQuantity, List<string>> Create(Set set, int quantity)
        {
            List<string> errorsList = [];

            Result<Set> resultSet = ValidateSet(set);

            if (resultSet.IsFailure)
                errorsList.Add(resultSet.Error);


            Result<Quantity, List<string>> resultQuantity = Quantity.Create(quantity);

            if (resultQuantity.IsFailure)
                errorsList.AddRange(resultQuantity.Error);


            if (errorsList.Count > 0)
                return Result.Failure<SetQuantity, List<string>>(errorsList);

            SetQuantity validSetQuantity = new(
                resultSet.Value,
                resultQuantity.Value);

            return Result.Success<SetQuantity, List<string>>(validSetQuantity);
        }

        private static Result<Set> ValidateSet(Set set)
        {
            if (set is null)
                return Result.Failure<Set>("set cannot be null");

            return Result.Success(set);
        }
    }
}
