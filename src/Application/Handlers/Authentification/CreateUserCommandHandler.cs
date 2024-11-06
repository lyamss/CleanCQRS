using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Authentification;
using Domain.Mappers.AuthToken;
using Domain.Mappers.Users;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;
namespace Application.Handlers.Authentification
{
    public sealed class CreateUserCommandHandler(
        IRepository<User> userRepositoryExtensions,
        IRepository<AuthToken> authTokenRepositoryExtensions,
        IUserRepository userRepository,
        UserMapper userMapper,
        AuthTokenMapper authTokenMapper,
        ICreateUserCommandValidator validator
        )
        : IRequestHandler<CreateUserCommand, ApiResponseDto>
    {
        private readonly IRepository<User> _userRepositoryExtensions = userRepositoryExtensions;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRepository<AuthToken> _authTokenRepositoryExtensions = authTokenRepositoryExtensions;
        private readonly UserMapper _userMapper = userMapper;
        private readonly AuthTokenMapper _authTokenMapper = authTokenMapper;
        private readonly ICreateUserCommandValidator _validator = validator;

        public async Task<ApiResponseDto> Handle(CreateUserCommand setuserRegistrationDto, CancellationToken cancellationToken)
        {
            var result = await this._validator.ValidateAsync(setuserRegistrationDto, cancellationToken);

            if (!result.IsValid)
            {
                return ApiResponseDto.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }


            User userI = await this._userRepository.GetUserWithEmail(setuserRegistrationDto.Email, cancellationToken);

            if (userI is not null)
            {
                return ApiResponseDto.Failure("Email already use");
            }


            var UserWhichWillBeCreated = new User
                (setuserRegistrationDto.Email,
                BCrypt.Net.BCrypt.HashPassword(setuserRegistrationDto.Password,
                BCrypt.Net.BCrypt.GenerateSalt(12))
                );


            await this._userRepositoryExtensions.AddAsync(UserWhichWillBeCreated, cancellationToken);
            int NewUser = await this._userRepositoryExtensions.SaveChangesAsync(cancellationToken);


            if (NewUser > 0)
            {
                var NewAuthToken = new AuthToken
                    (DateTime.UtcNow.AddHours(1).ToUniversalTime(),
                    Guid.NewGuid().ToString(),
                    UserWhichWillBeCreated.Id_User
                    );


                await this._authTokenRepositoryExtensions.AddAsync(NewAuthToken, cancellationToken);
                int newAuthTokenCreate = await this._authTokenRepositoryExtensions.SaveChangesAsync(cancellationToken);


                if (newAuthTokenCreate > 0)
                {

                    var response = new Dictionary<string, object>()
                    {
                        {"Token Session",  this._authTokenMapper.ToGetAuthTokenMapper(NewAuthToken) },
                        {"User",  this._userMapper.ToGetUserMapper(UserWhichWillBeCreated) },
                    };

                    return ApiResponseDto.Success("Your Account is created with succes ! :)", response);
                }

                return ApiResponseDto.Failure("Failed to create authToken user");
            }

            return ApiResponseDto.Failure("Failed to create user");
        }
    }
}