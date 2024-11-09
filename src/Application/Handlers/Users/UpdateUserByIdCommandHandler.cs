using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Users;
using Domain.Mappers.Users;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users
{
    internal sealed class UpdateUserByIdCommandHandler
        (
        IRepository<User> UserRepositoryExtensions,
        IdDtoValidator validator,
        EmailDtoValidator validatorEmail,
        PasswordValidator validatorPassword,
        UserMapper userMapper
        ) : IRequestHandler<UpdateUserCommand, ApiResponseDto>
    {
        private readonly IRepository<User> _UserRepositoryExtensions = UserRepositoryExtensions;
        private readonly IdDtoValidator _validator = validator;
        private readonly UserMapper _userMapper = userMapper;
        private readonly EmailDtoValidator _validatorEmail = validatorEmail;
        private readonly PasswordValidator _validatorPassword = validatorPassword;
        public async Task<ApiResponseDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var rsl = await this._validator.ValidateAsync(command.IdUser, cancellationToken);

            if (!rsl.IsValid)
            {
                return ApiResponseDto.Failure(rsl.Errors.Select(e => e.ErrorMessage).ToList());
            }

            User usr = await this._UserRepositoryExtensions.GetByIdAsync(command.IdUser, cancellationToken);

            if (usr is null)
            {
                return ApiResponseDto.Failure("User no exist");
            }

            var rslEmail = await this._validatorEmail.ValidateAsync(command.Email, cancellationToken);
            var rslPassword = await this._validatorPassword.ValidateAsync(command.Password, cancellationToken);

            usr.UpdateUser(
                usr, rslEmail.IsValid ? command.Email : usr.Email,
                rslPassword.IsValid ? BCrypt.Net.BCrypt.HashPassword(command.Password, BCrypt.Net.BCrypt.GenerateSalt(12)) : usr.PasswordHash);

            await this._UserRepositoryExtensions.SaveChangesAsync(cancellationToken);

            var responseApi = new Dictionary<string, object>()
            {
                {"User", this._userMapper.ToGetUserMapper(usr)},
                {"EmailErrorNoChange", rslEmail.IsValid ? null : rslEmail.Errors.Select(e => e.ErrorMessage).ToList()},
                {"PasswordErrorNoChange", rslPassword.IsValid ? null : rslPassword.Errors.Select(e => e.ErrorMessage).ToList()}
            };

            return ApiResponseDto.Success("User update", responseApi);
        }
    }
}