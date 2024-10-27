using Application.Services;
using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Repository.User;

namespace Application.UseCases
{
    internal class AddOrGetCache
        (
        ICacheService cacheService, 
        IConfigString configString,
        IRepository<User> repository,
        IUserRepository userRepository
        ) : IAddOrGetCache
    {
        private readonly ICacheService _cacheService = cacheService;
        private readonly IConfigString _configString = configString;
        private readonly IRepository<User> _dbSetExtensions = repository;
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<User> GetUserByIdAsyncCache(int idUser, CancellationToken cancellationToken)
        => await _cacheService.SetOrGetInDbOrCache
                (
                idUser.ToString() + this._configString.KeyInCacheGetByIdAsync,
                TimeSpan.FromSeconds(30),
                (cancellationToken) => this._dbSetExtensions.GetByIdAsync(idUser, cancellationToken),
                cancellationToken
                );

        public async Task<User> GetUserWithPseudoAsyncCache(string Pseudo, CancellationToken cancellationToken)
        => await _cacheService.SetOrGetInDbOrCache
                (
                    Pseudo.ToString().ToLower() + this._configString.KeyInCacheGetUserWithPseudo,
        TimeSpan.FromSeconds(30),
                    (cancellationToken) => this._userRepository.GetUserWithPseudo(Pseudo, cancellationToken),
                    cancellationToken
                );

        public async Task<IEnumerable<User>> GetAllUsersAsyncCache(CancellationToken cancellationToken)
            => await _cacheService.SetOrGetInDbOrCache
                (
                    this._configString.KeyInCacheGetAllUsers,
        TimeSpan.FromSeconds(30),
                    (cancellationToken) => this._dbSetExtensions.GetAllAsync(cancellationToken),
                    cancellationToken
                );
    }
}