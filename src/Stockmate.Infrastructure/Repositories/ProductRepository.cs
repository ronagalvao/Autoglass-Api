using Stockmate.Domain.Entities;
using Stockmate.Domain.Interfaces.Repositories;
using Stockmate.Infrastructure.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Stockmate.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task UpdateAsync(Product product)
    {
        var entity = await _context.Products.FindAsync(product.Id);

        _mapper.Map(product, entity);
        
        _context.Update(entity!);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(_mapper.Map<Product>(product));
        await _context.SaveChangesAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var entity = await _context.Products.FindAsync(id);
        return _mapper.Map<Product>(entity);
    }

    public async Task<List<Product>> GetFilteredAsync(string? description, DateTime? manufacturingDate, DateTime? expirationDate)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(description))
        {
            query = query.Where(x => x.Description!.Contains(description));
        }

        if (manufacturingDate != null)
        {
            Predicate<Product> manufacturingPredicate = p => p.ManufacturingDate >= manufacturingDate;
            query = query.Where(p => manufacturingPredicate(p));
        }

        if (expirationDate != null)
        {
            Predicate<Product> expirationPredicate = p => p.ExpirationDate <= expirationDate;
            query = query.Where(p => expirationPredicate(p));
        }

        var entities = await query.ToListAsync();
        return _mapper.Map<List<Product>>(entities);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Products.FindAsync(id);

        if (entity == null)
        {
            return;
        }

        entity.Status = ProductStatus.Deleted;
        await _context.SaveChangesAsync();
    }
}