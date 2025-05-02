using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class CSDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public CSDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("DataBase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}