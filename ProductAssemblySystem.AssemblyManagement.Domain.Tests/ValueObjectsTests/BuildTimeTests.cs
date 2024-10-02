using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;
using Xunit;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Tests.ValueObjectsTests
{
    public class BuildTimeTests
    {
        [Fact]
        public void CreateBuildTime_WithValidInput_ReturnsBuildTimeValueObject()
        {
            string timeString = "01:22:31";

            Result<BuildTime, List<string>> resultBuildTime = BuildTime.Create(timeString);

            Assert.True(resultBuildTime.IsSuccess);
        }

        [Fact]
        public void CreateBuildTime_WithInvalidInput_ReturnsBuildTimeValueObject()
        {
            string timeString = "string";

            Result<BuildTime, List<string>> resultBuildTime = BuildTime.Create(timeString);

            Assert.True(resultBuildTime.IsFailure);
        }

        [Fact]
        public void CreateBuildTime_WithEmptyString_ReturnsBuildTimeValueObject()
        {
            string timeString = "";

            Result<BuildTime, List<string>> resultBuildTime = BuildTime.Create(timeString);

            Assert.True(resultBuildTime.IsFailure);
        }

#nullable disable
        [Fact]
        public void CreateBuildTime_WithNullString_ReturnsBuildTimeValueObject()
        {
            string timeString = null;

            Result<BuildTime, List<string>> resultBuildTime = BuildTime.Create(timeString);

            Assert.True(resultBuildTime.IsFailure);
        }
#nullable restore

        [Fact]
        public void CreateBuildTime_WithOnlySpaces_ReturnsBuildTimeValueObject()
        {
            string timeString = "      ";

            Result<BuildTime, List<string>> resultBuildTime = BuildTime.Create(timeString);

            Assert.True(resultBuildTime.IsFailure);
        }
    }
}
