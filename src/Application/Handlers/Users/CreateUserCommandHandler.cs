using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Models;
using MediatR;
using Infrastructure.Repository;
using Domain.Commands.Users;
using Domain.Mappers.Users;
using Infrastructure.Repository.User;
using Domain.Mappers.AuthToken;
namespace Application.Handlers.Users
{
    public class CreateUserCommandHandler(
        IRegexUtils regexUtils,
        IRepository<User> dbSetExtensions,
        IRepository<AuthToken> repositoryAuthToken,
        UserMapper userMapper,
        IUserRepository userRepository,
        AuthTokenMapper authTokenMapper
        )
        : IRequestHandler<CreateUserCommand, ApiResponseDto>
    {
        private readonly IRegexUtils _regexUtils = regexUtils;
        private readonly IRepository<User> _dbSetExtensions = dbSetExtensions;
        private readonly UserMapper _userMapper = userMapper;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRepository<AuthToken> _repositoryAuthToken = repositoryAuthToken;
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

            await this._dbSetExtensions.AddAsync(UserWhichWillBeCreated, cancellationToken);
            int NewUser = await this._dbSetExtensions.SaveChangesAsync(cancellationToken);

            if (NewUser > 0)
            {
                var NewAuthToken = new AuthToken
                {
                    ExpirationDate = DateTime.UtcNow.AddHours(1).ToUniversalTime(),
                    Token = Guid.NewGuid().ToString(),
                    IdUser = UserWhichWillBeCreated.Id_User,
                };

                await this._repositoryAuthToken.AddAsync(NewAuthToken, cancellationToken);
                int newAuthTokenCreate = await this._repositoryAuthToken.SaveChangesAsync(cancellationToken); 

                if (newAuthTokenCreate > 0)
                {
                    var result = new Dictionary<string, object>()
                    {
                        {"User",  this._userMapper.ToGetUserMapper(UserWhichWillBeCreated) },
                        {"Token Session",  this._authTokenMapper.ToGetAuthTokenMapper(NewAuthToken) },
                    };

                    return ApiResponseDto.Success("Your Account is created with succes ! :)", result);
                }
                return ApiResponseDto.Failure("Failed to create authToken user");
            }

            return ApiResponseDto.Failure("Failed to create user");
        }
    }
}