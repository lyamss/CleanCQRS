using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    internal sealed class AuthTokenRepository : Repository<Domain.Models.AuthToken>, IAuthTokenRepository
    {
        public AuthTokenRepository(BackendDbContext backendDbContext) : base(backendDbContext) { }

        public async Task<Domain.Models.AuthToken> GetAuthTokenWithIdUser(int idUser, CancellationToken cancellationToken)
        => await this._context.AuthTokens
        .FirstOrDefaultAsync(u => u.IdUser == idUser, cancellationToken);

        public async Task<Domain.Models.AuthToken> GetAuthTokenWithToken(string token, CancellationToken cancellationToken)
        => await this._context.AuthTokens
            .FirstOrDefaultAsync(u => u.Token == token, cancellationToken);
    }

    public interface IAuthTokenRepository
    {
        Task<Domain.Models.AuthToken> GetAuthTokenWithIdUser(int idUser, CancellationToken cancellationToken);

        Task<Domain.Models.AuthToken> GetAuthTokenWithToken(string token, CancellationToken cancellationToken);
    }
}