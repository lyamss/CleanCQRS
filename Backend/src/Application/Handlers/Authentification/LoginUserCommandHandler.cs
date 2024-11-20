using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Authentification;
using Domain.Dtos.Query.Users;
using Domain.Mappers.Users;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Handlers.Authentification
{
    internal sealed class LoginUserCommandHandler
    (
        EmailDtoValidator regexUtils,
        IRepository<AuthToken> authTokenRepositoryExtensions,
        IAuthTokenRepository authTokenRepository,
        UserMapper userMapper,
        IAddOrGetCacheSvsScoped addOrGetCache,
        ISessionMethodSvsScoped sessionMethodSvsScoped,
        IHttpContextAccessor httpContextAccessor,
        IConfigStringSvs configStringSvs
    )
        : IRequestHandler<LoginUserCommand, ApiResponseDto>
    {
        private readonly EmailDtoValidator _regexUtils = regexUtils;
        private readonly IRepository<AuthToken> _authTokenRepositoryExtensions = authTokenRepositoryExtensions;
        private readonly IAuthTokenRepository _authTokenRepository = authTokenRepository;
        private readonly IAddOrGetCacheSvsScoped _addOrGetCacheSvsScoped = addOrGetCache;
        private readonly UserMapper _userMapper = userMapper;
        private readonly ISessionMethodSvsScoped _sessionMethodSvsScoped = sessionMethodSvsScoped;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IConfigStringSvs _configStringSvs = configStringSvs;

        public async Task<ApiResponseDto> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var success = await this._regexUtils.ValidateAsync(command.Email, cancellationToken);

            if (!success.IsValid)
            {
                return ApiResponseDto.Failure(success.Errors.Select(e => e.ErrorMessage).ToList());
            }

            GetUserQuery2 usr = await this._addOrGetCacheSvsScoped.GetUserWithEmailAsyncCache(command.Email, cancellationToken);
            
            if(usr is null)
            {
                return ApiResponseDto.Failure("Email or password invalid");
            }


            if(!BCrypt.Net.BCrypt.Verify(command.Password, usr.PasswordHash))
            {
                return ApiResponseDto.Failure("Email or password invalid");
            }

            AuthToken authToken = await this._authTokenRepository.GetAuthTokenWithIdUser(usr.Id_User, cancellationToken);

            authToken.UpdateAuthToken(authToken, DateTime.UtcNow.AddHours(1).ToUniversalTime(), Guid.NewGuid().ToString());

            await this._authTokenRepositoryExtensions.SaveChangesAsync(cancellationToken);

            this._sessionMethodSvsScoped
            .SetSessionCookie
            (
            this._httpContextAccessor.HttpContext.Response,
            this._configStringSvs.CookieUserConnected,
            authToken.ExpirationDate,
            authToken.Token
            );

            return ApiResponseDto.Success("Login succes ! :)", this._userMapper.ToGetUserAndWithoutGetPasswordMapper(usr));
        }
    }
}