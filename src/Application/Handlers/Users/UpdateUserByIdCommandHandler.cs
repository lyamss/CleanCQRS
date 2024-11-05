using Application.Services;
using Domain.Commands.Users;
using Domain.Dtos.AppLayerDtos;
using Domain.Mappers.Users;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users
{
    internal sealed class UpdateUserByIdCommandHandler
        (
        IRepository<User> UserRepositoryExtensions,
        IRegexUtils regexUtils,
        UserMapper userMapper
        ) : IRequestHandler<UpdateUserCommand, ApiResponseDto>
    {
        private readonly IRepository<User> _UserRepositoryExtensions = UserRepositoryExtensions;
        private readonly IRegexUtils _regexUtils = regexUtils;
        private readonly UserMapper _userMapper = userMapper;
        public async Task<ApiResponseDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            User usr = await this._UserRepositoryExtensions.GetByIdAsync(command.IdUser, cancellationToken);

            if (usr is null)
            {
                return ApiResponseDto.Failure("User no exist");
            }

            usr.Email = this._regexUtils.CheckEmail(command.Email) ? command.Email : usr.Email;
            usr.PasswordHash = this._regexUtils.CheckPassword(command.Password) ?command.Password : usr.PasswordHash;

            await this._UserRepositoryExtensions.SaveChangesAsync(cancellationToken);

            return ApiResponseDto.Success("User update =>", this._userMapper.ToGetUserMapper(usr));
        }
    }
}