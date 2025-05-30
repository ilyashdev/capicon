namespace capicon_backend.Models.Catalog;

public class ProductDetails
{
    public required int Id { get; set; }
    public string ShelfLife { get; set; }
    public string Packaging { get; set; }
    public string Dosage { get; set; }
    public string Certificates { get; set; }
    public int ProductId { get; set; }
}