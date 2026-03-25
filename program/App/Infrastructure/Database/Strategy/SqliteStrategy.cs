using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Strategy;

public class SqliteStrategy : IConnectionStrategy
{
    private readonly string _connectionString;
    
    public SqliteStrategy(string connectionString = "DataSource=:memory:")
    {
        _connectionString = connectionString;
    }
	public void Configure(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(_connectionString);
	}
}