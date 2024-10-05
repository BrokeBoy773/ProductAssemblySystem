using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Enums;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; }
        public AssemblySite? AssemblySite { get; private set; }
        public Position Position { get; private set; }

        private Employee(
            Guid id,
            Position position,
            AssemblySite? assemblySite = null)
        {
            Id = id;
            AssemblySite = assemblySite;
            Position = position;
        }

        public static Result<Employee, List<string>> Create(Guid id, Position position, AssemblySite? assemblySite = null)
        {
            List<string> errorsList = [];
            Employee validEmployee;

            if (assemblySite == null)
            {
                validEmployee = new(
                    id,
                    position,
                    assemblySite);

                return Result.Success<Employee, List<string>>(validEmployee);
            }


            Result<AssemblySite> resultAssemblySite = ValidateAssemblySite(assemblySite);

            if (resultAssemblySite.IsFailure)
                errorsList.Add(resultAssemblySite.Error);


            if (errorsList.Count > 0)
                return Result.Failure<Employee, List<string>>(errorsList);

            validEmployee = new(
                id,
                position,
                resultAssemblySite.Value);

            return Result.Success<Employee, List<string>>(validEmployee);
        }

        private static Result<AssemblySite> ValidateAssemblySite(AssemblySite assemblySite)
        {
            if (assemblySite is null)
                return Result.Failure<AssemblySite>("part cannot be null");

            return Result.Success(assemblySite);
        }
    }
}
