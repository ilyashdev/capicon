using capicon_backend.Models;
using capicon_backend.Models.Catalog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace capicon_backend.Database;

public class CapiconDBContext(DbContextOptions<CapiconDBContext> options) : IdentityDbContext(options)
{
    
    public DbSet<NewsPostModel> News { get; set; } = null!;
    public DbSet<ProductModel> Products { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(x => new { x.LoginProvider, x.ProviderKey });
        });
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.RoleId });
        });
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        });
    }
    
}