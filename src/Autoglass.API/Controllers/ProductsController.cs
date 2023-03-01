using Autoglass.API.Helpers;
using Autoglass.Application.Dtos;
using Autoglass.Application.Interfaces;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Validations;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Autoglass.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly ProductValidation _productValidation;

    public ProductsController
    (
        IProductService productService,
        IMapper mapper,
        ProductValidation productValidation
    )
    {
        _productService = productService;
        _mapper = mapper;
        _productValidation = productValidation;
    }

    [HttpGet("ProductById/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
            return NotFound();

        var productDto = _mapper.Map<ProductDto>(product);

        return Ok(productDto);
    }

    [HttpGet]
    [Route("FilteredProduct")]
    public async Task<IActionResult> GetFilteredProduct([FromQuery] string? description, [FromQuery] DateTime? manufacturingDate, [FromQuery] DateTime? expirationDate, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        pageNumber ??= 1;
        pageSize ??= 10;

        var products = await _productService.GetFilteredProductAsync(description, manufacturingDate, expirationDate);

        var totalProducts = products.Count();

        var productsPage = products.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(productsPage);

        var pagedList = new PagedList<ProductDto>(productDtos.ToList(), pageNumber.Value, pageSize.Value, totalProducts);

        return Ok(pagedList);
    }

    [HttpPost]
    [Route("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);

        _productValidation.ValidateAndThrow(product);

        await _productService.AddProductAsync(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var findedProduct = await _productService.CanBeUpdatedAsync(product.Id);

        if (id != productDto.Id || findedProduct == null)
            return BadRequest(findedProduct);
        
        _productValidation.ValidateAndThrow(product);

        await _productService.UpdateProductAsync(product);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);

        return Ok();
    }
}