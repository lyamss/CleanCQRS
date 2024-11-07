using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repository;

namespace Application.Services
{
    internal sealed class AddOrGetCacheSvsScoped
  (
        ICacheServiceRepository cacheService,
        IConfigStringSvs configString,
        IRepository<User> repository,
        IUserRepository userRepository,
        IAuthTokenRepository authTokenRepository
        ) : IAddOrGetCacheSvsScoped
    {
        private readonly ICacheServiceRepository _cacheService = cacheService;
        private readonly IConfigStringSvs _configString = configString;
        private readonly IRepository<User> _dbSetExtensions = repository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthTokenRepository _authTokenRepository = authTokenRepository;
        public async Task<User> GetUserByIdAsyncCache(int idUser, CancellationToken cancellationToken)
        => await this._cacheService.SetOrGetInDbOrCache
                (
                this._configString.KeyInCacheGetByIdAsync + idUser.ToString(),
                TimeSpan.FromSeconds(30),
                (cancellationToken) => this._dbSetExtensions.GetByIdAsync(idUser, cancellationToken),
                cancellationToken
                );

        public async Task<User> GetUserWithEmailAsyncCache(string Email, CancellationToken cancellationToken)
        => await this._cacheService.SetOrGetInDbOrCache
                (
                    this._configString.KeyInCacheGetUserWithEmail + Email.ToLower(),
        TimeSpan.FromSeconds(30),
                    (cancellationToken) => this._userRepository.GetUserWithEmail(Email, cancellationToken),
                    cancellationToken
                );


        public async Task<AuthToken> GetAuthTokenWithTokenAsyncCache(string jwt, CancellationToken cancellationToken)
    => await this._cacheService.SetOrGetInDbOrCache
        (
            this._configString.KeyInCacheGetUsersWithToken + jwt,
TimeSpan.FromSeconds(30),
            (cancellationToken) => this._authTokenRepository.GetAuthTokenWithToken(jwt, cancellationToken),
            cancellationToken
        );


    }

    public interface IAddOrGetCacheSvsScoped
    {
        Task<User> GetUserByIdAsyncCache(int idUser, CancellationToken cancellationToken);
        Task<User> GetUserWithEmailAsyncCache(string Email, CancellationToken cancellationToken);
        Task<AuthToken> GetAuthTokenWithTokenAsyncCache(string jwt, CancellationToken cancellationToken);
    }
}