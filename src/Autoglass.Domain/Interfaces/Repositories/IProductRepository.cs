using Autoglass.Domain.Entities;

namespace Autoglass.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetByDescriptionAsync(string description, int pageIndex, int pageSize);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
