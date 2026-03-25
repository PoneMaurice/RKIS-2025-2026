using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Strategy;

public class PostgresqlStrategy : IConnectionStrategy
{
    private readonly string _connectionString;
    
    public PostgresqlStrategy(string connectionString)
    {
        _connectionString = connectionString;
    }
	public void Configure(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(_connectionString);
	}
}