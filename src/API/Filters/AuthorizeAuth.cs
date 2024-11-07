using Microsoft.AspNetCore.Mvc.Filters;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace API.Filters
{
    internal sealed class AuthorizeAuth
        (
        IAddOrGetCacheSvsScoped authTokenRepository
        )
        : IAsyncAuthorizationFilter
    {
        private readonly IAddOrGetCacheSvsScoped _authTokenRepository = authTokenRepository;
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string jwt = null;

            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                if (authorizationHeader.ToString().StartsWith("Bearer "))
                {
                    jwt = authorizationHeader.ToString().Substring(7);
                }
            }
            
            if(jwt is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            AuthToken authToken = await this._authTokenRepository.GetAuthTokenWithTokenAsyncCache(jwt, context.HttpContext.RequestAborted);

            if (authToken is null || authToken.ExpirationDate < DateTime.UtcNow) 
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}