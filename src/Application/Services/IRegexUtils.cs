using Domain.Commands.Users;

namespace Application.Services
{
    public interface IRegexUtils
    {
        bool CheckEmail(string email);
        bool CheckPassword(string password);
        (bool, string) CheckSetUserRegistration(CreateUserCommand userRegistrationDto);
    }
}