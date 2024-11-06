namespace Domain.Dtos.Commands.Authentification
{
    public record class LoginUserCommand : CreateUserCommand
    {
        public LoginUserCommand(string email, string password) : base(email, password) { }
    }
}