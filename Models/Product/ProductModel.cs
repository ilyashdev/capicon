using System.ComponentModel.DataAnnotations;

namespace capicon_backend.Models.Catalog;

public class ProductModel
{
    public required int Id { get; init; }
    [Required(ErrorMessage = "Укажите заголовок.")]
    public required string Title { get; init; } 
    public string Subtitle { get; init; }
    
    [Required(ErrorMessage = "Укажите изображение.")]
    public required string MainImage { get; init; }
    
    public string Description1 { get; init; }
    public string Description2 { get; init; }

    public ProductDetails details { get; set; }
}



