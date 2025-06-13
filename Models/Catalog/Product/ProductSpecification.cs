namespace capicon.Models;
public class ProductSpecification
{
public int Id { get; set; }
public string Text { get; set; }
public int Amount { get; set; }
public ProductViewModel Product { get; set; }
}