using CSharpFunctionalExtensions;

namespace ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects
{
    public class BuildTime : ValueObject
    {
        public TimeSpan Time { get; }

        private BuildTime(TimeSpan time)
        {
            Time = time;
        }

        public static Result<BuildTime, List<string>> Create(string timeString)
        {
            List<string> errorsList = [];

            Result<TimeSpan> resultTime = ValidateTime(timeString);

            if (resultTime.IsFailure)
                errorsList.Add(resultTime.Error);

            if (errorsList.Count > 0)
                return Result.Failure<BuildTime, List<string>>(errorsList);

            BuildTime validBuildTime = new(resultTime.Value);

            return Result.Success<BuildTime, List<string>>(validBuildTime);
        }

        private static Result<TimeSpan> ValidateTime(string timeString)
        {
            if (string.IsNullOrWhiteSpace(timeString))
                return Result.Failure<TimeSpan>("timeString is null or white space");

            if (!TimeSpan.TryParse(timeString, out TimeSpan time))
                return Result.Failure<TimeSpan>("Invalid time format");

            return Result.Success(time);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Time;
        }
    }
}
