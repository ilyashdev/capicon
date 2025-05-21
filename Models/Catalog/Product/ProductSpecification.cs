namespace capicon.Models;
public class ProductSpecification
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Parameters { get; set; }

    public ProductSpecification(string name, Dictionary<string, string> parameters)
    {
        Name = name;
        Parameters = parameters;
    }
    public int ProductId { get; set; }
}