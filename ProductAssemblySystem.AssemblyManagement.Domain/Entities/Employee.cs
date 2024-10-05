using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Enums;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; }
        public AssemblySite AssemblySite { get; private set; } = null!;
        public Position Position { get; private set; }

        private Employee(
            Guid id,
            AssemblySite assemblySite,
            Position position)
        {
            Id = id;
            AssemblySite = assemblySite;
            Position = position;
        }

        public static Result<Employee> Create(Guid id, AssemblySite assemblySite, Position position)
        {
            Result<AssemblySite> resultAssemblySite = ValidateAssemblySite(assemblySite);

            if (resultAssemblySite.IsFailure)
                return Result.Failure<Employee>(resultAssemblySite.Error);

            Employee validEmployee = new(
                id,
                resultAssemblySite.Value,
                position);

            return Result.Success(validEmployee);
        }

        private static Result<AssemblySite> ValidateAssemblySite(AssemblySite assemblySite)
        {
            if (assemblySite is null)
                return Result.Failure<AssemblySite>("part cannot be null");

            return Result.Success(assemblySite);
        }
    }
}
