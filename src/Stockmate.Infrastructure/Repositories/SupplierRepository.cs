using Stockmate.Domain.Entities;
using Stockmate.Domain.Interfaces.Repositories;
using Stockmate.Infrastructure.Context;
using AutoMapper;

namespace Stockmate.Infrastructure.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public SupplierRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Supplier> GetByIdAsync(int id)
    {
        var entity = await _context.Suppliers.FindAsync(id);
        return _mapper.Map<Supplier>(entity);
    }

    public async Task CreateAsync(Supplier supplier)
    {
        await _context.Suppliers.AddAsync(_mapper.Map<Supplier>(supplier));
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        var entity = await _context.Suppliers.FindAsync(supplier.Id);

        if (entity == null)
        {
            return;
        }

        _mapper.Map(supplier, entity);
        await _context.SaveChangesAsync();
    }
}
