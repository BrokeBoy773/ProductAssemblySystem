using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class Set
    {
        public Guid Id { get; }
        public Name Name { get; private set; } = null!;
        public TimeSpan BuildTime { get; private set; }

        private readonly List<Part> _parts = [];
        public IReadOnlyList<Part> Parts => _parts;

        private Set()
        {
        }

        private Set(
            Guid id,
            Name name,
            TimeSpan buildTime,
            List<Part> setParts)
        {
            Id = id;
            Name = name;
            BuildTime = buildTime;
            _parts.AddRange(setParts);
        }

        public static Result<Set, List<string>> Create(
            Guid id,
            string name,
            List<Part> setParts
            )
        {
            List<string> errorsList = [];

            Result<Name, List<string>> resultName = Name.Create(name);

            if (resultName.IsFailure)
                errorsList.AddRange(resultName.Error);


            if (errorsList.Count > 0)
                return Result.Failure<Set, List<string>>(errorsList);

            if (setParts is null || setParts.Count <= 1)
            {
                errorsList.Add("setParts list is null or contains fewer than two elements");
                return Result.Failure<Set, List<string>>(errorsList);
            }


            Result<TimeSpan> resultBuildTime = CombinePartsBuildTime(setParts);

            if (resultBuildTime.IsFailure)
            {
                errorsList.Add(resultBuildTime.Error);
                return Result.Failure<Set, List<string>>(errorsList);
            }

            Set validSet = new(
                id,
                resultName.Value,
                resultBuildTime.Value,
                setParts);

            return Result.Success<Set, List<string>>(validSet);
        }

        public static Result<TimeSpan> CombinePartsBuildTime(List<Part> setParts)
        {
            return setParts
                .Select(p => p.BuildTime.Time)
                .Aggregate(TimeSpan.Zero, (total, next) => total + next);
        }
    }
}
