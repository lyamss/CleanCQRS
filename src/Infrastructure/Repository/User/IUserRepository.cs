namespace Infrastructure.Repository.User
{
    public interface IUserRepository
    {
        Task<Domain.Models.User> GetUserWithEmail(string email, CancellationToken cancellationToken);
    }
}