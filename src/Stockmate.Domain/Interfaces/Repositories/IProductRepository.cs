using Stockmate.Domain.Entities;

namespace Stockmate.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<List<Product>> GetFilteredAsync(string? description, DateTime? manufacturingDate, DateTime? expirationDate);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
