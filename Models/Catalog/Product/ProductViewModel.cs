namespace capicon.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Subtitle { get; set; }
    public string MainImage { get; set; }
    
    public string Description1 { get; set; }
    public string Description2 { get; set; }
    
    public List<ProductSpecification> Specifications { get; set; }
    

    public string Usage { get; set; }
    public string Warning { get; set;} 
    

    public ProductDetails Details { get; set; } 
}




