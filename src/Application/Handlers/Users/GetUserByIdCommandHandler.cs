using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands;
using Domain.Mappers.Users;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users
{
    internal sealed class GetUserByIdCommandHandler
        (
        UserMapper userMapper,
        IRepository<User> UserRepositoryExtensions,
        IIdDtoValidator validator
        )
        : IRequestHandler<ByIdCommand, ApiResponseDto>
    {
        private readonly IRepository<User> _UserRepositoryExtensions = UserRepositoryExtensions;
        private readonly UserMapper _userMapper = userMapper;
        private readonly IIdDtoValidator _validator = validator;

        public async Task<ApiResponseDto> Handle(ByIdCommand command, CancellationToken cancellationToken)
        {
            var rsl = await this._validator.ValidateAsync(command.ById, cancellationToken);

            if (!rsl.IsValid)
            {
                return ApiResponseDto.Failure(rsl.Errors.Select(e => e.ErrorMessage).ToList());
            }

            User usr = await this._UserRepositoryExtensions.GetByIdAsync(command.ById, cancellationToken);

            if (usr is null)
            {
                return ApiResponseDto.Failure("User no exist");
            }

            var getUserDtos = this._userMapper.ToGetUserMapper(usr);

            return ApiResponseDto.Success("User found", getUserDtos);
        }
    }
}