namespace Autoglass.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Description { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime ManufacturingDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int SupplierId { get; set; }
    public string SupplierDescription { get; set; }
    public string SupplierDocument { get; set; }

    public Product(int id, string description, ProductStatus status, DateTime manufacturingDate, DateTime expirationDate, int supplierId, string supplierDescription, string supplierDocument)
    {
        Id = id;
        Description = description;
        Status = status;
        ManufacturingDate = manufacturingDate;
        ExpirationDate = expirationDate;
        SupplierId = supplierId;
        SupplierDescription = supplierDescription;
        SupplierDocument = supplierDocument;
    }
}

public enum ProductStatus
{
    Active, Inactive
}
