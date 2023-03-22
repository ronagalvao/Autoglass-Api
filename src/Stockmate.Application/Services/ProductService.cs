using Stockmate.Application.Interfaces;
using Stockmate.Domain.Entities;
using Stockmate.Domain.Interfaces.Repositories;

namespace Stockmate.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product;
    }

    public async Task<IEnumerable<Product>> GetFilteredProductAsync(string? description, DateTime? manufacturingDate, DateTime? expirationDate)
    {
        var products = await _productRepository.GetFilteredAsync(description, manufacturingDate, expirationDate);
        return products;
    }

    public async Task AddProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<string> CanBeUpdatedAsync(int id)
    {
        var erro = "";

        if (await _productRepository.GetByIdAsync(id) == null)
        {
            erro = $"Produto com código {id} não foi encontrado.";
            return erro;
        }

        return erro;
    }
}