using CSharpFunctionalExtensions;
using ProductAssemblySystem.UserManagement.Domain.ValueObjects;
using Xunit;

namespace ProductAssemblySystem.UserManagement.Domain.Tests.ValueObjectsTests
{
    public class PasswordHashTests
    {
#nullable disable
        [Fact]
        public void CreatePasswordHash_WithNullString_ReturnsPasswordHashValueObject()
        {
            string passwordHash = null;

            Result<PasswordHash, List<string>> result = PasswordHash.Create(passwordHash);

            Assert.True(result.IsFailure);
        }
#nullable restore
    }
}
