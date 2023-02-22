using Autoglass.Domain.Entities;

namespace Autoglass.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierDocument { get; set; }
    }

}
