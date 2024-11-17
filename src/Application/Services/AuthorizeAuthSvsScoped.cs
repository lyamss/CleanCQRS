using Domain.Dtos.Query.AuthToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Services
{
    internal sealed class AuthorizeAuthSvsScoped
        (
        IAddOrGetCacheSvsScoped addOrGetCacheSvsScoped
        ) : IAuthorizeAuthSvsScoped
    {
        private readonly IAddOrGetCacheSvsScoped _addOrGetCacheSvsScoped = addOrGetCacheSvsScoped;

        public async Task OnAuthorize(AuthorizationFilterContext context)
        {
            string jwt = null;

            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                if (authorizationHeader.ToString().StartsWith("Bearer "))
                {
                    jwt = authorizationHeader.ToString().Substring(7);
                }
            }

            if (jwt is null)
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

    public interface IAuthorizeAuthSvsScoped
    {
        Task OnAuthorize(AuthorizationFilterContext context);
    }
}