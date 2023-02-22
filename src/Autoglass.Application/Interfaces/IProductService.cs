using Autoglass.Domain.Entities;

namespace Autoglass.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync(string? description, DateTime? manufacturingDate, DateTime? expirationDate);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
