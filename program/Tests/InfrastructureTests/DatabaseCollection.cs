using Infrastructure.Database;
using Infrastructure.Database.Strategy;

namespace InfrastructureTests;

public class DatabaseCollection : IDisposable
{
    public TodoContext TodoContext { get; }
    private readonly SqliteInMemoryStrategy _sqliteInMemoryStrategy;
    public DatabaseCollection()
    {
        _sqliteInMemoryStrategy = new();
        TodoContext = new(_sqliteInMemoryStrategy);
        TodoContext.Database.EnsureCreated();
    }
    public void Dispose()
    {
        TodoContext.Dispose();
        _sqliteInMemoryStrategy.Dispose();
    }
}