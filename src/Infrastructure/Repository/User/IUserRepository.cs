namespace Infrastructure.Repository.User
{
    public interface IUserRepository
    {
        Task<Domain.Models.User> GetUserWithPseudo(string pseudo, CancellationToken cancellationToken);
    }
}