using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Query.Users;
using Domain.Mappers.Users;
using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
namespace Application.Handlers.Users
{
    internal sealed class GetAllUsersCommandHandler
        (
            UserMapper userMapper,
            IRepository<User> UserRepositoryExtensions,
            ICacheServiceRepository cacheService,
            IConfigStringSvs configStringSvs
        )
        : IRequestHandler<GetUserQuery, ApiResponseDto>
    {
        private readonly UserMapper _userMapper = userMapper;
        private readonly IRepository<User> _UserRepositoryExtensions = UserRepositoryExtensions;
        private readonly ICacheServiceRepository _cacheService = cacheService;
        private readonly IConfigStringSvs _configStringSvs = configStringSvs;

        public async Task<ApiResponseDto> Handle(GetUserQuery getUserCommand, CancellationToken cancellationToken)
        {
            var users = await this._cacheService.SetOrGetInDbOrCache
            (
                this._configStringSvs.KeyInCacheGetAllUsers,
                TimeSpan.FromSeconds(30),
                cancellationToken => this._UserRepositoryExtensions.GetAllAsync(cancellationToken),
                users => users.Select(user => this._userMapper.ToGetUserMapper(user)).ToList(),
                cancellationToken
            );

            if (!users.Any()) 
            { 
                return ApiResponseDto.Success("no users in database", users);
            }

            return ApiResponseDto.Success("User(s) found", users);
        }
    }
}