using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Entities;
using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Auxiliaries
{
    public class PartQuantity
    {
        public Part Part { get; } = null!;
        public Quantity Quantity { get; } = null!;

        private PartQuantity(
            Part part,
            Quantity quantity)
        {
            Part = part;
            Quantity = quantity;
        }

        public static Result<PartQuantity, List<string>> Create(Part part, int quantity)
        {
            List<string> errorsList = [];

            Result<Part> resultPart = ValidatePart(part);

            if (resultPart.IsFailure)
                errorsList.Add(resultPart.Error);


            Result<Quantity, List<string>> resultQuantity = Quantity.Create(quantity);

            if (resultQuantity.IsFailure)
                errorsList.AddRange(resultQuantity.Error);


            if (errorsList.Count > 0)
                return Result.Failure<PartQuantity, List<string>>(errorsList);

            PartQuantity validPartQuantity = new(
                resultPart.Value,
                resultQuantity.Value);

            return Result.Success<PartQuantity, List<string>>(validPartQuantity);
        }

        private static Result<Part> ValidatePart(Part part)
        {
            if (part is null)
                return Result.Failure<Part>("part cannot be null");

            return Result.Success(part);
        }
    }
}
