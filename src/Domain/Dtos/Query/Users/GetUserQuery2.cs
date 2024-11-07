namespace Domain.Dtos.Query.Users
{
    public record GetUserQuery2
    {
        public Guid Id_User { get; init; }
        public string Email { get; init; }
        public DateTime AccountCreatedAt { get; init; }
        public string PasswordHash { get; init; }
    }
}