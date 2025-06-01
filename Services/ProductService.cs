using capicon.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using capicon.Settings;
public class ProductService
{
    private readonly CSDbContext _context;

    public ProductService(CSDbContext context)
    {
        _context = context;
    }

    public async Task CreateProduct(ProductViewModel? product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(ProductViewModel? product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductViewModel>> SearchProducts(string query, int page) =>
      await _context.Products
        .Include(p => p.Specifications)
        .Include(p => p.Details)
        .Where(p => p.Title.Contains(query) || 
                    p.Description1.Contains(query) || 
                    p.Description2.Contains(query))
        .Skip(page * PageSettings.PAGE_SIZE)
        .Take(PageSettings.PAGE_SIZE)
        .ToListAsync();

    public async Task<ProductViewModel?> GetProduct(int id, int page) =>
      await _context.Products
        .Include(p => p.Specifications)
        .Include(p => p.Details)
        .FirstOrDefaultAsync(p => p.Id == id);

    public async Task UpdateProduct(ProductViewModel? product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}