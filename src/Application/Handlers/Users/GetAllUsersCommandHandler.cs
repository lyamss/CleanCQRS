using Domain.Dtos.AppLayerDtos;
using Domain.Mappers.Users;
using Domain.Models;
using Domain.Query.Users;
using Infrastructure.Repository;
using MediatR;
namespace Application.Handlers.Users
{
    public sealed class GetAllUsersCommandHandler
        (
            UserMapper userMapper,
            IRepository<User> UserRepositoryExtensions
        )
        : IRequestHandler<GetUserQuery, ApiResponseDto>
    {
        private readonly UserMapper _userMapper = userMapper;
        private readonly IRepository<User> _UserRepositoryExtensions = UserRepositoryExtensions;

        public async Task<ApiResponseDto> Handle(GetUserQuery getUserCommand, CancellationToken cancellationToken)
        {
            IEnumerable<User> dataUserNowConnect = await this._UserRepositoryExtensions.GetAllAsync(cancellationToken);

            if (!dataUserNowConnect.Any()) 
            { 
                return ApiResponseDto.Failure("No Users in DB"); 
            }

            var getUserDtos = dataUserNowConnect.Select(user => this._userMapper.ToGetUserMapper(user)).ToList();

            return ApiResponseDto.Success("User(s) found", getUserDtos);
        }
    }
}