using Domain.Commands.Users;

namespace Application.Services
{
    public interface IRegexUtils
    {
        bool CheckPseudo(string Pseudo);
        bool CheckPassword(string password);
        (bool, string) CheckSetUserRegistration(CreateUserCommand userRegistrationDto);
    }
}