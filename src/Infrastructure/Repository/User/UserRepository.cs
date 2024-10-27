using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repository.User
{
    internal sealed class UserRepository : Repository<Domain.Models.User> ,IUserRepository
    {
        public UserRepository(BackendDbContext backendDbContext) : base(backendDbContext) { }

        public async Task<Domain.Models.User> GetUserWithPseudo(string pseudo, CancellationToken cancellationToken)
            => await this._context.Users
            .FirstOrDefaultAsync(u => u.Pseudo.ToLower() == pseudo.ToLower(), cancellationToken);
    }
}