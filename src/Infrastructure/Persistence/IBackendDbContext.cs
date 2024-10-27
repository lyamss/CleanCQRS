namespace Infrastructure.Persistence;

public interface IBackendDbContext
{
    void Migrate();
}