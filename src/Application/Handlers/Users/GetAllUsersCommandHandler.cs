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
            CacheValue<List<GetUserQuery>>  dataInCache = await this._cacheService.GetInCacheAsync<List<GetUserQuery>>
                (_configStringSvs.KeyInCacheGetAllUsers, cancellationToken);

            if (!dataInCache.HasValue)
            {
                IEnumerable<User> data = await this._UserRepositoryExtensions.GetAllAsync(cancellationToken);

                if(data is not null)
                {
                    var mappData = data.Select(usr => this._userMapper.ToGetUserMapper(usr)).ToList();

                    await this._cacheService.SetInCacheAsync<List<GetUserQuery>>(cancellationToken, _configStringSvs.KeyInCacheGetAllUsers, mappData, TimeSpan.FromSeconds(30));

                    return ApiResponseDto.Success("User(s) found", mappData);
                }

                await this._cacheService.SetInCacheAsync<List<GetUserQuery>>(cancellationToken, _configStringSvs.KeyInCacheGetAllUsers, default(List<GetUserQuery>), TimeSpan.FromSeconds(30));

                return ApiResponseDto.Failure("No Users in DB");
            }

            return ApiResponseDto.Success("User(s) found", dataInCache.Value);
        }
    }
}