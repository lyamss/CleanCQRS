namespace Application.Services
{
    internal sealed class ConfigString : IConfigString
    {
        public string Test => "test";
    }

    public interface IConfigString
    {
        public string Test { get; }
    }
}