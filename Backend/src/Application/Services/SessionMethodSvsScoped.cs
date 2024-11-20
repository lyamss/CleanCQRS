using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    internal sealed class SessionMethodSvsScoped : ISessionMethodSvsScoped
    {
        public void SetSessionCookie(HttpResponse Response, string nameOfCookie, DateTime expirationCookie, string valueUser)
        {

            DateTimeOffset dateExpiration = expirationCookie;
            DateTimeOffset currentDate = DateTimeOffset.UtcNow;
            TimeSpan maxAge = dateExpiration - currentDate;


            Response.Cookies.Append(
                nameOfCookie,
                valueUser,
                new CookieOptions
                {
                    Path = "/",
                    MaxAge = maxAge,
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
                }
            );
        }
    }

    public interface ISessionMethodSvsScoped
    {
        void SetSessionCookie(HttpResponse Response, string nameOfCookie, DateTime expirationCookie, string valueUser);
    }
}