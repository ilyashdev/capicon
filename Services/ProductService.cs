// ProductService.cs
using capicon.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using capicon.Settings;

namespace capicon.Services;

public class ProductService
{
    private readonly CSDbContext _context;
    private readonly ImageService _imageService;

    public ProductService(CSDbContext context, ImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }


    public async Task CreateProduct(ProductViewModel product)
    {
        if (product.ImageFile != null)
        {
            product.MainImage = _imageService.SaveImage(product.ImageFile);
        }

        var entity = new ProductViewModel
        {
            Title = product.Title,
            Subtitle = product.Subtitle,
            MainImage = product.MainImage,
            Description1 = product.Description1,
            Description2 = product.Description2,
            Usage = product.Usage,
            Warning = product.Warning,
            StoragePeriod = product.StoragePeriod,
            Recomendation = product.Recomendation,
            Specifications = product.Specifications
                .Where(s => !string.IsNullOrWhiteSpace(s.Text))
                .Select(s => new ProductSpecification
                {
                    Text = s.Text,
                    Amount = s.Amount
                }).ToList()
        };

        _context.Products.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduct(ProductViewModel product)
    {
        if (product.ImageFile != null)
        {
            product.MainImage = _imageService.SaveImage(product.ImageFile);
        }

        var existingProduct = await _context.Products
            .Include(p => p.Specifications)
            .FirstOrDefaultAsync(p => p.Id == product.Id);
        var oldImage = existingProduct.MainImage;

        // Обновляем изображение
        if (product.ImageFile != null)
        {
            existingProduct.MainImage = _imageService.SaveImage(product.ImageFile);
        
            // Удаляем старое изображение
            if (!string.IsNullOrEmpty(oldImage))
            {
                _imageService.DeleteImage(oldImage);
            }
        }
        else if (!string.IsNullOrEmpty(product.MainImage))
        {
            existingProduct.MainImage = product.MainImage;
        }
        if (existingProduct == null) return;
        existingProduct.Title = product.Title;
        existingProduct.Subtitle = product.Subtitle;
        existingProduct.MainImage = product.MainImage;
        existingProduct.Description1 = product.Description1;
        existingProduct.Description2 = product.Description2;
        existingProduct.Usage = product.Usage;
        existingProduct.Warning = product.Warning;
        existingProduct.StoragePeriod = product.StoragePeriod;
        existingProduct.Recomendation = product.Recomendation;
        _context.Specifications.RemoveRange(existingProduct.Specifications);
        foreach (var spec in product.Specifications
            .Where(s => !string.IsNullOrWhiteSpace(s.Text)))
        {
            existingProduct.Specifications.Add(new ProductSpecification
            {
                Text = spec.Text,
                Amount = spec.Amount
            });
        }

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(ProductViewModel product)
    {
        if (!string.IsNullOrEmpty(product.MainImage))
        {
            _imageService.DeleteImage(product.MainImage);
        }
    
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCount() => 
        await _context.Products.CountAsync();

    public async Task<List<ProductViewModel>> SearchProducts(string query, int page) =>
      await _context.Products
        .Include(p => p.Specifications)
        .Where(p => p.Title.Contains(query) || 
                    p.Description1.Contains(query) || 
                    p.Description2.Contains(query))
        .Skip(page * PageSettings.PAGE_SIZE)
        .Take(PageSettings.PAGE_SIZE)
        .ToListAsync();

    public async Task<ProductViewModel?> GetProduct(int id, int page) =>
      await _context.Products
        .Include(p => p.Specifications)
        .FirstOrDefaultAsync(p => p.Id == id);

}