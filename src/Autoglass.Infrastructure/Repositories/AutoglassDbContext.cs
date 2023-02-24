using Autoglass.Domain.Entities;
using Autoglass.Domain.Interfaces.Repositories;
using Autoglass.Infrastructure.Context;

namespace Autoglass.Infrastructure.Repositories;

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
