using capicon_backend.Database;
using capicon_backend.Models.Catalog;
using Microsoft.EntityFrameworkCore;

namespace capicon_backend.Services;

public class ProductService(CapiconDBContext context)
{
    public async Task CreateProduct(ProductModel product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        var res = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (res == null) return;
        context.Products.Remove(res);
        await context.SaveChangesAsync();
    }

    public async Task<List<ProductModel>> SearchProducts(string query, int skip, int take) =>
        await context.Products
            .Where(p => p.Title.Contains(query) || 
                        p.Description1.Contains(query) || 
                        p.Description2.Contains(query))
            .Skip(skip)
            .Take(take)
            .ToListAsync();

    public async Task<ProductModel?> GetProduct(int id) =>
        await context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task UpdateProduct(ProductModel product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }
}