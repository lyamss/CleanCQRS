using Domain.Models;

namespace Application.UseCases
{
    public interface IAddOrGetCache
    {
        Task<User> GetUserByIdAsyncCache(int idUser, CancellationToken cancellationToken);
        Task<User> GetUserWithPseudoAsyncCache(string Pseudo, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetAllUsersAsyncCache(CancellationToken cancellationToken);
    }
}