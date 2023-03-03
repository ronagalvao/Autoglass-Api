using Autoglass.API.Helpers;
using Autoglass.Application.Dtos;
using Autoglass.Application.Interfaces;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Validations;
using AutoMapper;
using FluentValidation;

namespace Autoglass.API.Extensions;

public static class PathExtensions
{
    public static WebApplication AddPathExtensions(this WebApplication app)
    {
        app.MapGet("/api/GetProduct/{id}", async (int id, IProductService productService, IMapper mapper) =>
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
                return Results.NotFound();

            var productDto = mapper.Map<ProductDto>(product);

            return Results.Ok(productDto);
        });

        app.MapGet("/api/GetFilteredProduct", async (string? description, DateTime? manufacturingDate, DateTime? expirationDate, int? pageNumber, int? pageSize, IProductService productService, IMapper mapper) =>
        {
            pageNumber ??= 1;
            pageSize ??= 10;

            var products = await productService.GetFilteredProductAsync(description, manufacturingDate, expirationDate);

            var totalProducts = products.Count();

            var productsPage = products.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

            var productDtos = mapper.Map<IEnumerable<ProductDto>>(productsPage);

            var pagedList = new PagedList<ProductDto>(productDtos.ToList(), pageNumber.Value, pageSize.Value, totalProducts);

            return Results.Ok(pagedList);
        });

        app.MapPost("/api/AddProduct", async (ProductDto productDto, IProductService productService, IMapper mapper, ProductValidation productValidation) =>
        {
            var product = mapper.Map<Product>(productDto);

            productValidation.ValidateAndThrow(product);

            await productService.AddProductAsync(product);

            return Results.Ok(product);
        });

        app.MapPut("/api/UpdateProduct/{id}", async (int id, ProductDto productDto, IProductService productService, IMapper mapper, ProductValidation productValidation) =>
        {
            var product = mapper.Map<Product>(productDto);
            var findedProduct = await productService.CanBeUpdatedAsync(product.Id);

            if (id != productDto.Id || findedProduct == null)
                return Results.BadRequest(findedProduct);

            productValidation.ValidateAndThrow(product);

            await productService.UpdateProductAsync(product);

            return Results.Ok();
        });

        app.MapDelete("/api/DeleteProduct/{id}", async (int id, IProductService productService) =>
        {
            await productService.DeleteProductAsync(id);

            return Results.Ok();
        });

        return app;
    }
}