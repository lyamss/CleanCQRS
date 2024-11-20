using Microsoft.AspNetCore.Mvc.Filters;
using Application.Services;

namespace API.Filters
{
    internal sealed class AuthorizeAuth
        (
        IAuthorizeAuthSvsScoped authorizeAuthSvsScoped
        )
        : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizeAuthSvsScoped _authorizeAuthSvsScoped = authorizeAuthSvsScoped;
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        => await this._authorizeAuthSvsScoped.OnAuthorize(context);
    }
}