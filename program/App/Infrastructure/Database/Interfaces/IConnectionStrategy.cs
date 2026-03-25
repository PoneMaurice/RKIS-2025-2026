using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Interfaces;

public interface IConnectionStrategy
{
    void Configure(DbContextOptionsBuilder optionsBuilder);
}