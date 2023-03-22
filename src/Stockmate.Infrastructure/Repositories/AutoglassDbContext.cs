using Stockmate.Domain.Entities;
using Stockmate.Domain.Interfaces.Repositories;
using Stockmate.Infrastructure.Context;

namespace Stockmate.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private bool _disposed;

    public UnitOfWork(AppDbContext context,
        IProductRepository productRepository,
        ISupplierRepository supplierRepository)
    {
        _context = context;
        Products = productRepository;
        Suppliers = supplierRepository;
    }

    public IProductRepository Products { get; }
    public ISupplierRepository Suppliers { get; }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
