using capicon.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly CSDbContext _context;

    public ProductService(CSDbContext context)
    {
        _context = context;
    }

    public async Task CreateProduct(ProductViewModel product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductViewModel>> SearchProducts(string query)
    {
        return await _context.Products
            .Include(p => p.Specifications)
            .Include(p => p.Details)
            .Where(p => p.Title.Contains(query) || 
                      p.Description1.Contains(query) || 
                      p.Description2.Contains(query))
            .ToListAsync();
    }

    public async Task<ProductViewModel> GetProduct(int id)
    {
        return await _context.Products
            .Include(p => p.Specifications)
            .Include(p => p.Details)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateProduct(ProductViewModel product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}