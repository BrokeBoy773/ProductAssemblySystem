using CSharpFunctionalExtensions;

namespace ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects
{
    public class Quantity : ValueObject
    {
        public int ItemQuantity { get; }

        private Quantity(int itemQuantity)
        {
            ItemQuantity = itemQuantity;
        }

        public static Result<Quantity, List<string>> Create(int quantity)
        {
            List<string> errorsList = [];

            Result<int> resultQuantity = ValidateQuantity(quantity);

            if (resultQuantity.IsFailure)
                errorsList.Add(resultQuantity.Error);


            if (errorsList.Count > 0)
                return Result.Failure<Quantity, List<string>>(errorsList);

            Quantity validQuantity = new(quantity);

            return Result.Success<Quantity, List<string>>(validQuantity);
        }

        private static Result<int> ValidateQuantity(int quantity)
        {
            if (quantity < 1)
                return Result.Failure<int>("quantity cannot be negative or zero.");

            return Result.Success(quantity);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ItemQuantity;
        }
    }
}
