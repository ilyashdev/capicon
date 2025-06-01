namespace capicon.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _env;

    public ImageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string SaveImage(IFormFile file)
    {
        if (!file.ContentType.Contains("image"))
            throw new Exception("invalid file type");
        using (var fileStream = new FileStream(Path.Combine(_env.WebRootPath, "image", file.FileName), FileMode.Create))
        {
            file.CopyTo(fileStream);
        }
        return Path.Combine("image", file.FileName);
    }
}