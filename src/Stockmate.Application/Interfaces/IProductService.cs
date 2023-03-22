using Stockmate.Domain.Entities;

namespace Stockmate.Application.Interfaces;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetFilteredProductAsync(string? description, DateTime? manufacturingDate, DateTime? expirationDate);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<string> CanBeUpdatedAsync(int id);
}
