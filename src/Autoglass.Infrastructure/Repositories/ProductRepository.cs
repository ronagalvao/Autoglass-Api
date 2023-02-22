using Autoglass.Domain.Entities;
using Autoglass.Domain.Interfaces.Repositories;
using Autoglass.Infrastructure.Context;
using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace Autoglass.Infrastructure.Repositories
{
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

        public async Task<List<Product>> GetAllAsync()
        {
            var entities = await _context.Products.ToListAsync();
            return _mapper.Map<List<Product>>(entities);
        }

        public async Task<List<Product>> GetByDescriptionAsync(string description, int pageIndex, int pageSize)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(x => x.Description.Contains(description));
            }

            var entities = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map<List<Product>>(entities);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);

            if (entity == null)
            {
                return;
            }

            entity.Status = ProductStatus.Inactive;
            await _context.SaveChangesAsync();
        }
    }
}
