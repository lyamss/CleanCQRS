using System.Text.RegularExpressions;
using Domain.Commands.Users;
namespace Application.Services
{
    public class RegexUtils : IRegexUtils
    {
        private readonly Regex PseudoRegex = new(@"^[a-zA-Z0-9_]{1,15}$", RegexOptions.Compiled);
        private readonly Regex PasswordRegex = new(@"^.{8,100}$", RegexOptions.Compiled);

        public bool CheckPseudo(string Pseudo)
        {
            if (string.IsNullOrEmpty(Pseudo))
                return false;

            if (!PseudoRegex.IsMatch(Pseudo))
                return false;

            return true;
        }

        public (bool, string) CheckSetUserRegistration(CreateUserCommand userRegistrationDto)
        {
            if (!CheckPseudo(userRegistrationDto.Pseudo)) return (false, "Invalid pseudo or this length");

            if (!CheckPassword(userRegistrationDto.Password)) return (false, "Invalid Password Minimum 8 characters and 100 max");

            return (true, "All Regex passed :)");
        }
      
        public bool CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password.ToString())) return false;
            if(!PasswordRegex.IsMatch(password.ToString())) return false;
            return true;
        }
    }
}