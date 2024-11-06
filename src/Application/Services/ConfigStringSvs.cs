namespace Application.Services
{
    internal sealed class ConfigStringSvs : IConfigStringSvs
    {
        public string Test => "test";
    }

    public interface IConfigStringSvs
    {
        public string Test { get; }
    }
}