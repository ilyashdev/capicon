using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using capicon.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking; 

namespace DataAccess;

public class CSDbContext : IdentityDbContext
{
    private readonly IConfiguration _configuration;

    public CSDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<PostModel> News { get; set; } = null!;
    public DbSet<ProductViewModel?> Products { get; set; } = null!;
    public DbSet<ProductSpecification> Specifications { get; set; } = null!;
    public DbSet<ProductDetails> Details { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("DataBase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(x => new { x.LoginProvider, x.ProviderKey });
        });
        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.RoleId });
        });
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        });
         modelBuilder.Entity<ProductViewModel>()
            .HasMany(p => p.Specifications)
            .WithOne()
            .HasForeignKey("ProductId");

        modelBuilder.Entity<ProductViewModel>()
            .HasOne(p => p.Details)
            .WithOne()
            .HasForeignKey<ProductDetails>(d => d.ProductId);

        // Конвертация Dictionary в JSON
        modelBuilder.Entity<ProductSpecification>()
         .Property(s => s.Parameters)
         .HasConversion(
             v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
             v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions())!,
             new ValueComparer<Dictionary<string, string>>(
                 (c1, c2) => c1.SequenceEqual(c2),
                 c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                 c => c.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
        }
}