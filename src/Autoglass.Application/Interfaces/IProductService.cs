using Autoglass.Domain.Entities;

namespace Autoglass.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync(int? vendorCode, DateTime? manufacturingDate, DateTime? expirationDate, int? pageNumber, int? pageSize);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
