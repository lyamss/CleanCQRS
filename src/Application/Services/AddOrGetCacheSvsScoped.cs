using Domain.Dtos.Query.AuthToken;
using Domain.Dtos.Query.Users;
using Domain.Mappers.AuthToken;
using Domain.Mappers.Users;
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
        IAuthTokenRepository authTokenRepository,
        UserMapper userMapper,
        AuthTokenMapper authTokenMapper
    )
        : IAddOrGetCacheSvsScoped
    {
        private readonly ICacheServiceRepository _cacheService = cacheService;
        private readonly IConfigStringSvs _configString = configString;
        private readonly IRepository<User> _dbSetExtensions = repository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthTokenRepository _authTokenRepository = authTokenRepository;
        private readonly UserMapper _userMapper = userMapper;
        private readonly AuthTokenMapper _authTokenMapper = authTokenMapper;


        public async Task<GetUserQuery> GetUserByIdAsyncCache(Guid idUser, CancellationToken cancellationToken)
        => await this._cacheService.SetOrGetInDbOrCache
        (
        this._configString.KeyInCacheGetByIdAsync + idUser,
        TimeSpan.FromSeconds(30),
        cancellationToken => this._dbSetExtensions.GetByIdAsync(idUser, cancellationToken),
        user => this._userMapper.ToGetUserMapper(user),
        cancellationToken
        );


        public async Task<GetUserQuery2> GetUserWithEmailAsyncCache(string Email, CancellationToken cancellationToken)
        => await this._cacheService.SetOrGetInDbOrCache
        (
            this._configString.KeyInCacheGetUserWithEmail + Email.ToLower(),
            TimeSpan.FromSeconds(30),
            cancellationToken => this._userRepository.GetUserWithEmail(Email, cancellationToken),
            user => this._userMapper.ToGetUserAndGetPasswordMapper(user),
            cancellationToken
        );


        public async Task<GetAuthTokenQuery> GetAuthTokenWithTokenAsyncCache(string jwt, CancellationToken cancellationToken)
        => await this._cacheService.SetOrGetInDbOrCache
        (
            this._configString.KeyInCacheGetUsersWithToken + jwt,
            TimeSpan.FromSeconds(30),
            cancellationToken => this._authTokenRepository.GetAuthTokenWithToken(jwt, cancellationToken),
            tkn => this._authTokenMapper.ToGetAuthTokenMapper(tkn),
            cancellationToken
        );
    }

    public interface IAddOrGetCacheSvsScoped
    {
        Task<GetUserQuery> GetUserByIdAsyncCache(Guid idUser, CancellationToken cancellationToken);
        Task<GetUserQuery2> GetUserWithEmailAsyncCache(string Email, CancellationToken cancellationToken);
        Task<GetAuthTokenQuery> GetAuthTokenWithTokenAsyncCache(string jwt, CancellationToken cancellationToken);
    }
}