using Autoglass.API.Helpers;
using Autoglass.Application.Dtos;
using Autoglass.Application.Interfaces;
using Autoglass.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Autoglass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string? description, [FromQuery] DateTime? manufacturingDate, [FromQuery] DateTime? expirationDate, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            pageNumber ??= 1;
            pageSize ??= 10;

            var products = await _productService.GetProductsAsync(description, manufacturingDate, expirationDate);

            var totalProducts = products.Count();
            
            var productsPage = products.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(productsPage);

            var pagedList = new PagedList<ProductDto>(productDtos.ToList(), pageNumber.Value, pageSize.Value, totalProducts);

            return Ok(pagedList);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _productService.AddProductAsync(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest();

            var product = _mapper.Map<Product>(productDto);

            await _productService.UpdateProductAsync(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}