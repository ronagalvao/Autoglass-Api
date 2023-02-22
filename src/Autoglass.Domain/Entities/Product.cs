namespace Autoglass.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime ManufacturingDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int SupplierId { get; set; }

    virtual public Supplier? Vendor { get; set; }
}

public enum ProductStatus
{
    Active, Inactive
}
