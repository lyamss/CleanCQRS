using System.Text.RegularExpressions;
using Domain.Commands.Users;
namespace Application.Services
{
    public class RegexUtils : IRegexUtils
    {
        private readonly Regex PasswordRegex = new(@"^.{8,100}$", RegexOptions.Compiled);
        private readonly Regex EmailRegex = new(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.Compiled);

        public bool CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email.ToString())) return false;
            if (!this.EmailRegex.IsMatch(email.ToString())) return false;
            return true;
        }

        public (bool, string) CheckSetUserRegistration(CreateUserCommand userRegistrationDto)
        {
            if (!this.CheckEmail(userRegistrationDto.Email)) return (false, "Invalid email or this length");

            if (!this.CheckPassword(userRegistrationDto.Password)) return (false, "Invalid Password Minimum 8 characters and 100 max");

            return (true, "All Regex passed :)");
        }
      
        public bool CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password.ToString())) return false;
            if(!this.PasswordRegex.IsMatch(password.ToString())) return false;
            return true;
        }
    }
}