namespace capicon.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _env;
    private static readonly string[] AllowedImageTypes = 
    {
        "image/jpeg", "image/png", "image/gif", "image/webp"
    };

    public ImageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string SaveImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new Exception("Файл не предоставлен");
        if (!AllowedImageTypes.Contains(file.ContentType))
            throw new Exception("Недопустимый тип файла. Разрешены: JPEG, PNG, GIF, WEBP");
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "products");
        var filePath = Path.Combine(uploadsFolder, fileName);
        Directory.CreateDirectory(uploadsFolder);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }
        return Path.Combine("uploads", "products", fileName).Replace('\\', '/');
    }
    
    public void DeleteImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath)) return;
        
        var fullPath = Path.Combine(_env.WebRootPath, imagePath);
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
        }
    }
}