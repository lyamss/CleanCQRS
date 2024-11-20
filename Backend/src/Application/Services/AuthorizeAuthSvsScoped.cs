using Domain.Dtos.Query.AuthToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Services
{
    internal sealed class AuthorizeAuthSvsScoped
        (
        IAddOrGetCacheSvsScoped addOrGetCacheSvsScoped,
        IConfigStringSvs configStringSvs
        ) : IAuthorizeAuthSvsScoped
    {
        private readonly IAddOrGetCacheSvsScoped _addOrGetCacheSvsScoped = addOrGetCacheSvsScoped;
        private readonly IConfigStringSvs _configStringSvs = configStringSvs;

        public async Task OnAuthorize(AuthorizationFilterContext context)
        {
            string cookie = null;

            if (context.HttpContext.Request.Cookies.TryGetValue(this._configStringSvs.CookieUserConnected, out string co))
            {

                cookie = co;

            }

            if (cookie is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            GetAuthTokenQuery authToken = await this._addOrGetCacheSvsScoped.GetAuthTokenWithTokenAsyncCache(cookie, context.HttpContext.RequestAborted);

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