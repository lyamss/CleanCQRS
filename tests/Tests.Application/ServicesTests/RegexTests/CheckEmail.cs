using Application.Services;

namespace Tests.Application.ServicesTests.RegexTests
{
    public class CheckEmail
    {
        private readonly RegexUtils _regexUtils = new();


        public CheckEmail()
        {
            _regexUtils = new RegexUtils();
        }

        [Fact]
        public void CheckEmail_ValidEmail_ReturnsTrue()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var result = _regexUtils.CheckEmail(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckEmail_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            var email = "invalid-email";

            // Act
            var result = _regexUtils.CheckEmail(email);

            // Assert
            Assert.False(result);
        }
    }
}