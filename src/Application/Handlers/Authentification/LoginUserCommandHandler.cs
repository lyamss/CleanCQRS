using Application.Services;
using Domain.Commands.Authentification;
using Domain.Dtos.AppLayerDtos;
using Domain.Mappers.AuthToken;
using Domain.Mappers.Users;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;

namespace Application.Handlers.Authentification
{
    internal sealed class LoginUserCommandHandler
        (
        IRegexUtils regexUtils,
        IUserRepository userRepository,
        IRepository<AuthToken> authTokenRepositoryExtensions,
        IAuthTokenRepository authTokenRepository,
        UserMapper userMapper,
        AuthTokenMapper authTokenMapper
        ) 
        : IRequestHandler<LoginUserCommand, ApiResponseDto>
    {
        private readonly IRegexUtils _regexUtils = regexUtils;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRepository<AuthToken> _authTokenRepositoryExtensions = authTokenRepositoryExtensions;
        private readonly IAuthTokenRepository _authTokenRepository = authTokenRepository;
        private readonly UserMapper _userMapper = userMapper;
        private readonly AuthTokenMapper _authTokenMapper = authTokenMapper;
        public async Task<ApiResponseDto> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var success = this._regexUtils.CheckEmail(command.Email);

            if (!success)
            {
                return ApiResponseDto.Failure("Email syntax invalid");
            }


            Domain.Models.User usr = await this._userRepository.GetUserWithEmail(command.Email, cancellationToken);
            
            if(usr is null)
            {
                return ApiResponseDto.Failure("Email or password invalid");
            }


            if(!BCrypt.Net.BCrypt.Verify(command.Password, usr.PasswordHash))
            {
                return ApiResponseDto.Failure("Email or password invalid");
            }


            AuthToken authToken = await this._authTokenRepository.GetAuthTokenWithIdUser(usr.Id_User, cancellationToken);


            authToken.ExpirationDate = DateTime.UtcNow.AddHours(1).ToUniversalTime();
            authToken.Token = Guid.NewGuid().ToString();


            await this._authTokenRepositoryExtensions.SaveChangesAsync(cancellationToken);


            var result = new Dictionary<string, object>()
            {
                {"User",  this._userMapper.ToGetUserMapper(usr) },
                {"Token Session",  this._authTokenMapper.ToGetAuthTokenMapper(authToken) },
            };


            return ApiResponseDto.Success("Login succes ! :)", result);
        }
    }
}