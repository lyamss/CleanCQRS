using Domain.Commands.Users;
using Domain.Dtos.AppLayerDtos;
using Domain.Mappers.Users;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users
{
    internal sealed class GetUserByIdCommandHandler
        (
        UserMapper userMapper,
        IRepository<User> UserRepositoryExtensions
        )
        : IRequestHandler<GetUserByIdCommand, ApiResponseDto>
    {
        private readonly IRepository<User> _UserRepositoryExtensions = UserRepositoryExtensions;
        private readonly UserMapper _userMapper = userMapper;

        public async Task<ApiResponseDto> Handle(GetUserByIdCommand command, CancellationToken cancellationToken)
        {
            User usr = await this._UserRepositoryExtensions.GetByIdAsync(command.UserIdToGet, cancellationToken);

            if (usr is null)
            {
                return ApiResponseDto.Failure("User no exist");
            }

            var getUserDtos = this._userMapper.ToGetUserMapper(usr);

            return ApiResponseDto.Success("User(s) found", getUserDtos);
        }
    }
}