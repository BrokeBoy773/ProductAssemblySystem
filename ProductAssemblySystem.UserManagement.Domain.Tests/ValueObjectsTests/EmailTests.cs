using CSharpFunctionalExtensions;
using ProductAssemblySystem.UserManagement.Domain.ValueObjects;
using Xunit;

namespace ProductAssemblySystem.UserManagement.Domain.Tests.ValueObjectsTests
{
    public class EmailTests
    {
        [Fact]
        public void CreateEmail_WithValidInput_ReturnsEmailValueObject()
        {
            List<string> existingEmails = ["bob@gmail.com", "tom@gmail.com"];
            string email = "example@gmail.com";

            Result<Email, List<string>> result = Email.Create(email, existingEmails);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CreateEmail_WithValidInputWithSpaces_ReturnsEmailValueObject()
        {
            List<string> existingEmails = [];
            string email = "       example@gmail.com           ";

            Result<Email, List<string>> result = Email.Create(email, existingEmails);

            Assert.Equal("example@gmail.com", result.Value.EmailAddress);
        }

        [Theory]
        [InlineData("examplegmail.com")]
        [InlineData("example@@gmail.com")]
        [InlineData("exa mple@gmail.com")]
        [InlineData("example@gmail")]
        [InlineData("example@")]
        [InlineData("@gmail.com")]
        [InlineData("@@gmail.com")]
        [InlineData("-@gmail.com")]
        public void CreateEmail_WithInvalidInput_ReturnsEmailValueObject(string email)
        {
            List<string> existingEmails = [];
            Result<Email, List<string>> result = Email.Create(email, existingEmails);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateEmail_WithExistingEmail_ReturnsEmailValueObject()
        {
            List<string> existingEmails = ["example@gmail.com"];
            string email = "example@gmail.com";

            Result<Email, List<string>> result = Email.Create(email, existingEmails);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateEmail_WithEmptyString_ReturnsEmailValueObject()
        {
            List<string> existingEmails = [];
            string email = "";

            Result<Email, List<string>> result = Email.Create(email, existingEmails);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateEmail_WithOnlySpaces_ReturnsEmailValueObject()
        {
            List<string> existingEmails = [];
            string email = "     ";

            Result<Email, List<string>> result = Email.Create(email, existingEmails);

            Assert.True(result.IsFailure);
        }

#nullable disable
        [Fact]
        public void CreateEmail_WithNullString_ReturnsEmailValueObject()
        {
            List<string> existingEmails = [];
            string email = null;

            Result<Email, List<string>> result = Email.Create(email, existingEmails);

            Assert.True(result.IsFailure);
        }
#nullable restore
    }
}
