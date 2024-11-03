using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Models;
using MediatR;
using Infrastructure.Repository;
using Domain.Mappers.Users;
using Domain.Mappers.AuthToken;
using Domain.Commands.Authentification;
namespace Application.Handlers.Authentification
{
    public class CreateUserCommandHandler(
        IRegexUtils regexUtils,
        IRepository<User> userRepositoryExtensions,
        IRepository<AuthToken> authTokenRepositoryExtensions,
        IUserRepository userRepository,
        UserMapper userMapper,
        AuthTokenMapper authTokenMapper
        )
        : IRequestHandler<CreateUserCommand, ApiResponseDto>
    {
        private readonly IRegexUtils _regexUtils = regexUtils;
        private readonly IRepository<User> _userRepositoryExtensions = userRepositoryExtensions;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRepository<AuthToken> _authTokenRepositoryExtensions = authTokenRepositoryExtensions;
        private readonly UserMapper _userMapper = userMapper;
        private readonly AuthTokenMapper _authTokenMapper = authTokenMapper;

        public async Task<ApiResponseDto> Handle(CreateUserCommand setuserRegistrationDto, CancellationToken cancellationToken)
        {
            var (success, message) = this._regexUtils.CheckSetUserRegistration(setuserRegistrationDto);

            if (!success)
            {
                return ApiResponseDto.Failure(message);
            }


            User userI = await this._userRepository.GetUserWithEmail(setuserRegistrationDto.Email, cancellationToken);

            if (userI is not null)
            {
                return ApiResponseDto.Failure("Email already use");
            }


            var UserWhichWillBeCreated = new User
            {
                Email = setuserRegistrationDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(setuserRegistrationDto.Password, BCrypt.Net.BCrypt.GenerateSalt(12)),
            };


            await this._userRepositoryExtensions.AddAsync(UserWhichWillBeCreated, cancellationToken);
            int NewUser = await this._userRepositoryExtensions.SaveChangesAsync(cancellationToken);


            if (NewUser > 0)
            {
                var NewAuthToken = new AuthToken
                {
                    ExpirationDate = DateTime.UtcNow.AddHours(1).ToUniversalTime(),
                    Token = Guid.NewGuid().ToString(),
                    IdUser = UserWhichWillBeCreated.Id_User,
                };


                await this._authTokenRepositoryExtensions.AddAsync(NewAuthToken, cancellationToken);
                int newAuthTokenCreate = await this._authTokenRepositoryExtensions.SaveChangesAsync(cancellationToken);


                if (newAuthTokenCreate > 0)
                {

                    var result = new Dictionary<string, object>()
                    {
                        {"Token Session",  this._authTokenMapper.ToGetAuthTokenMapper(NewAuthToken) },
                        {"User",  this._userMapper.ToGetUserMapper(UserWhichWillBeCreated) },
                    };

                    return ApiResponseDto.Success("Your Account is created with succes ! :)", result);
                }

                return ApiResponseDto.Failure("Failed to create authToken user");
            }

            return ApiResponseDto.Failure("Failed to create user");
        }
    }
}