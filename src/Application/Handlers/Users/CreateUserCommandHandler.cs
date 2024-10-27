using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Models;
using MediatR;
using Infrastructure.Repository;
using Application.UseCases;
using Domain.Commands.Users;
using Domain.Mappers.Users;
namespace Application.Handlers.Users
{
    public class CreateUserCommandHandler(
        IRegexUtils regexUtils,
        IRepository<User> dbSetExtensions,
        IAddOrGetCache addOrGetCache,
        UserMapper userMapper
        )
        : IRequestHandler<CreateUserCommand, ApiResponseDto>
    {
        private readonly IRegexUtils _regexUtils = regexUtils;
        private readonly IRepository<User> _dbSetExtensions = dbSetExtensions;
        private readonly IAddOrGetCache _addOrGetCache = addOrGetCache;
        private readonly UserMapper _userMapper = userMapper;

        public async Task<ApiResponseDto> Handle(CreateUserCommand setuserRegistrationDto, CancellationToken cancellationToken)
        {
            var (success, message) = this._regexUtils.CheckSetUserRegistration(setuserRegistrationDto);

            if (!success)
            { return ApiResponseDto.Failure(message); }

            User userI = await this.
                _addOrGetCache.GetUserWithPseudoAsyncCache(setuserRegistrationDto.Pseudo, cancellationToken);


            if (userI is not null)
            { return ApiResponseDto.Failure("Pseudo already use"); }

            var UserWhichWillBeCreated = new User
            {
                Pseudo = setuserRegistrationDto.Pseudo,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(setuserRegistrationDto.Password, BCrypt.Net.BCrypt.GenerateSalt(12)),
            };

            await this._dbSetExtensions.AddAsync(UserWhichWillBeCreated, cancellationToken);
            int NewUser = await this._dbSetExtensions.SaveChangesAsync(cancellationToken);

            if (NewUser > 0)
            {
                return ApiResponseDto.Success("Your Account is created with succes ! :)", this._userMapper.ToGetUserMapper(UserWhichWillBeCreated));
            }

            return ApiResponseDto.Failure("Failed to create user");
        }
    }
}