using Application.Services;
using Domain.Commands.Users;

namespace Tests.Application.ServicesTests.RegexTests
{
    public class RegexUtilsTests
    {
        private readonly RegexUtils _regexUtils;

        public RegexUtilsTests()
        {
            _regexUtils = new RegexUtils();
        }

        [Fact]
        public void CheckPassword_ValidPassword_ReturnsTrue()
        {
            // Arrange
            var password = "ValidPassword123";

            // Act
            var result = _regexUtils.CheckPassword(password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckPassword_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            var password = "short";

            // Act
            var result = _regexUtils.CheckPassword(password);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckSetUserRegistrationOAuth_ValidData_ReturnsTrue()
        {
            // Arrange
            var userDto = new CreateUserCommand
            {
                Email = "toto@gmail.com",
                Password = "ValidPassword123"
            };

            // Act
            var result = _regexUtils.CheckSetUserRegistration(userDto);

            // Assert
            Assert.True(result.Item1);
        }

        [Fact]
        public void CheckSetUserRegistrationOAuth_InvalidData_ReturnsFalse()
        {
            // Arrange
            var userDto = new CreateUserCommand
            {
                Email = "ValidNamezjehdbzhjebdhjzebdjezbdhezbhd", // false
                Password = "short"
            };

            // Act
            var result = _regexUtils.CheckSetUserRegistration(userDto);

            // Assert
            Assert.False(result.Item1);
        }
    }
}