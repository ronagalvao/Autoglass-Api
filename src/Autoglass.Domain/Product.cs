namespace Autoglass.Domain;

public class Product
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public ProductStatus Status { get; set; }
    public DateRange? DateRange { get; set; }
    public Vendor? Vendor { get; set; }
}

public enum ProductStatus
{
    Active, Inactive
}
