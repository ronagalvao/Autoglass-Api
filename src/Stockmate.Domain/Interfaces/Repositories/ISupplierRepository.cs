using Stockmate.Domain.Entities;

namespace Stockmate.Domain.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier> GetByIdAsync(int id);
    }
}
