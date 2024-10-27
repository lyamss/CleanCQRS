namespace Application.Services
{
    public interface IConfigString
    {
        public string KeyInCacheGetByIdAsync { get; }
        public string KeyInCacheGetUserWithPseudo { get; }
    }
}