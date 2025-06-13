

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace capicon.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Название обязательно")]
    public string Title { get; set; } = string.Empty;
    
    public string Subtitle { get; set; } = string.Empty;
    
    [Display(Name = "Главное изображение")]
    public string MainImage { get; set; } = string.Empty;
    
    [Display(Name = "Описание 1")]
    public string Description1 { get; set; } = string.Empty;
    
    [Display(Name = "Описание 2")]
    public string Description2 { get; set; } = string.Empty;
    
    public List<ProductSpecification> Specifications { get; set; } = new();
    
    public string Usage { get; set; } = string.Empty;
    public string Warning { get; set; } = string.Empty; 
    public string StoragePeriod { get; set; } = string.Empty;
    public string Recomendation { get; set; } = string.Empty;

    [NotMapped]
    [Display(Name = "Загрузить изображение")]
    public IFormFile? ImageFile { get; set; }
}


