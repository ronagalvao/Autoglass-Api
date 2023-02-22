using Autoglass.Domain.Entities;

namespace Autoglass.Domain.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier> GetByIdAsync(int id);
    }
}
