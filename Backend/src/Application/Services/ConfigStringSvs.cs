namespace Application.Services
{
    internal sealed class ConfigStringSvs : IConfigStringSvs
    {
        public string KeyInCacheGetByIdAsync => "KeyInCacheGetByIdAsync";

        public string KeyInCacheGetUserWithEmail => "KeyInCacheGetUserWithEmail";

        public string KeyInCacheGetAllUsers => "KeyInCacheGetAllUsers";

        public string KeyInCacheGetUsersWithToken => "KeyInCacheGetUsersWithToken";

        public string CookieUserConnected => "CookieUserConnectedTP";
    }

    public interface IConfigStringSvs
    {
        public string KeyInCacheGetByIdAsync { get; }
        public string KeyInCacheGetUserWithEmail { get; }
        public string KeyInCacheGetAllUsers { get; }
        public string KeyInCacheGetUsersWithToken { get; }
        public string CookieUserConnected { get; }
    }
}