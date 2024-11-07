﻿using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Authentification;
using Domain.Dtos.Query.Users;
using Domain.Mappers.AuthToken;
using Domain.Mappers.Users;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;

namespace Application.Handlers.Authentification
{
    internal sealed class LoginUserCommandHandler
    (
        EmailDtoValidator regexUtils,
        IRepository<AuthToken> authTokenRepositoryExtensions,
        IAuthTokenRepository authTokenRepository,
        AuthTokenMapper authTokenMapper,
        UserMapper userMapper,
        IAddOrGetCacheSvsScoped addOrGetCache
    )
        : IRequestHandler<LoginUserCommand, ApiResponseDto>
    {
        private readonly EmailDtoValidator _regexUtils = regexUtils;
        private readonly IRepository<AuthToken> _authTokenRepositoryExtensions = authTokenRepositoryExtensions;
        private readonly IAuthTokenRepository _authTokenRepository = authTokenRepository;
        private readonly AuthTokenMapper _authTokenMapper = authTokenMapper;
        private readonly IAddOrGetCacheSvsScoped _addOrGetCacheSvsScoped = addOrGetCache;
        private readonly UserMapper _userMapper = userMapper;

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

            var result = new Dictionary<string, object>()
            {
                {"User", this._userMapper.ToGetUserAndWithoutGetPasswordMapper(usr) },
                {"Token Session",  this._authTokenMapper.ToGetAuthTokenMapper(authToken) },
            };

            return ApiResponseDto.Success("Login succes ! :)", result);
        }
    }
}