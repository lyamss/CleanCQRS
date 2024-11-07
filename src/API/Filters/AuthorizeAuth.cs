using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Dtos.Query.AuthToken;

namespace API.Filters
{
    internal sealed class AuthorizeAuth
        (
        IAddOrGetCacheSvsScoped addOrGetCacheSvsScoped
        )
        : IAsyncAuthorizationFilter
    {
        private readonly IAddOrGetCacheSvsScoped _addOrGetCacheSvsScoped = addOrGetCacheSvsScoped;
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

            GetAuthTokenQuery authToken = await this._addOrGetCacheSvsScoped.GetAuthTokenWithTokenAsyncCache(jwt, context.HttpContext.RequestAborted);

            if (authToken is null || authToken.ExpirationDate < DateTime.UtcNow) 
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}