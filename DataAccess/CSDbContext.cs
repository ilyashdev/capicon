using capicon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class CSDbContext : IdentityDbContext<User, UserRole, int>
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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
        {
            entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
        });

        modelBuilder.Entity<IdentityUserRole<int>>(entity =>
        {
            entity.HasKey(r => new { r.UserId, r.RoleId });
        });

        modelBuilder.Entity<IdentityUserToken<int>>(entity =>
        {
            entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        });
    }
}