using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class Part
    {
        public Guid Id { get; }
        public Name Name { get; private set; } = null!;
        public BuildTime BuildTime { get; private set; } = null!;

        private Part()
        {
        }

        private Part(
            Guid id,
            Name name,
            BuildTime buildTime)
        {
            Id = id;
            Name = name;
            BuildTime = buildTime;
        }

        public static Result<Part, List<string>> Create(
            Guid id,
            string name,
            string timeString)
        {
            List<string> errorsList = [];

            Result<Name, List<string>> resultName = Name.Create(name);

            if (resultName.IsFailure)
                errorsList.AddRange(resultName.Error);


            Result<BuildTime, List<string>> resultBuildTime = BuildTime.Create(timeString);

            if (resultBuildTime.IsFailure)
                errorsList.AddRange(resultBuildTime.Error);


            if (errorsList.Count > 0)
                return Result.Failure<Part, List<string>>(errorsList);

            Part validPart = new(
                id,
                resultName.Value,
                resultBuildTime.Value);

            return Result.Success<Part, List<string>>(validPart);
        }
    }
}
