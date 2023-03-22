using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Stockmate.Application.Dtos;
using Stockmate.Application.Interfaces;
using Stockmate.Domain.Entities;
using Stockmate.Domain.Validations;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using FluentAssertions;

namespace Stockmate.UnitTests.Api.Extensions;
public class PathExtensionTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ProductValidation> _productValidationMock;

    public PathExtensionTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _mapperMock = new Mock<IMapper>();
        _productValidationMock = new Mock<ProductValidation>();

    }

    [Fact]
    public async Task PathExtensions_GetProduct_ReturnsOkResult()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_productServiceMock.Object);
                    services.AddSingleton(_mapperMock.Object);
                }));
                
        var client = application.CreateClient();

        var id = 1;

        var productDto = new ProductDto
        {
            Id = 1,
            Description = "Test product",
            Status = ProductStatus.Active,
            ManufacturingDate = DateTime.Now,
            ExpirationDate = DateTime.Now.AddDays(30),
            SupplierId = 1,
            SupplierDescription = "Test Supplier",
            SupplierDocument = "47.607.687/0001-91"
        };

        var product = new Product
        {
            Id = 1,
            Description = "Test product",
            Status = ProductStatus.Active,
            ManufacturingDate = DateTime.Now,
            ExpirationDate = DateTime.Now.AddDays(30),
            SupplierId = 1,
            SupplierDescription = "Test Supplier",
            SupplierDocument = "67.237.025/0001-84"
        };

        _productServiceMock.Setup(x => x.GetProductByIdAsync(id))
            .ReturnsAsync(product);

        _mapperMock.Setup(x => x.Map<ProductDto>(product))
            .Returns(productDto);

        // Act
        var response = await client.GetAsync($"/api/GetProduct/{id}");
        response.EnsureSuccessStatusCode();

        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
        var responseDto = JsonSerializer.Deserialize<ProductDto>(responseString, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            });

        responseDto.Should().BeEquivalentTo(productDto);
    }

        [Fact]
    public async Task PathExtensions_GetProduct_ReturnsNotFoundResult()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_productServiceMock.Object);
                    services.AddSingleton(_mapperMock.Object);
                }));
                
        var client = application.CreateClient();

        var nonExistingProductId = 9999;
        _productServiceMock.Setup(x => x.GetProductByIdAsync(nonExistingProductId)).ReturnsAsync((Product)null!);

        // Act
        var response = await client.GetAsync($"/api/GetProduct/{nonExistingProductId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
