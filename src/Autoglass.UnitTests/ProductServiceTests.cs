using Autoglass.Application.Mappings;
using Autoglass.Application.Services;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Interfaces.Repositories;

using AutoMapper;

using Moq;

namespace UnitTests;

public class ProductServiceTests
{

    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServiceTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
        _mapper = new Mapper(mapperConfig);
        _unitOfWork = new Mock<IUnitOfWork>().Object;
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var productDescription = "Test Product";
        var status = ProductStatus.Active;
        var manufacturingDate = new DateTime(2022, 01, 01);
        var expirationDate = new DateTime(2022, 01, 01);
        var supplierId = 1;
        var supplierDescription = "Test Supplier";
        var supplierDocument = "1234567890";

        var product = new Product
        (
            productId,
            productDescription,
            status,
            manufacturingDate,
            expirationDate,
            supplierId,
            supplierDescription,
            supplierDocument
        );
        _mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
        var productService = new ProductService(_mockProductRepository.Object, _unitOfWork);

        // Act
        var result = await productService.GetProductByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
        Assert.Equal(productId, result.Id);
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = 1;
        _mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync((Product?)null!);
        var productService = new ProductService(_mockProductRepository.Object, _unitOfWork);

        // Act
        var result = await productService.GetProductByIdAsync(productId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetFilteredProductAsync_ReturnsFilteredProducts_WhenProductsExist()
    {
        var productId = 1;
        var productDescription = "Test Product";
        var status = ProductStatus.Active;
        var manufacturingDate = new DateTime(2022, 01, 01);
        var expirationDate = new DateTime(2022, 01, 01);
        var supplierId = 1;
        var supplierDescription = "Test Supplier";
        var supplierDocument = "1234567890";

        // Arrange
        var product1 = new Product
        (
            productId,
            productDescription,
            status,
            manufacturingDate,
            expirationDate,
            supplierId,
            supplierDescription,
            supplierDocument
        );

        var product2 = new Product
        (
            2,
            "This is a test product 2.",
            status,
            new DateTime(2022, 02, 01),
            new DateTime(2022, 03, 01),
            supplierId,
            supplierDescription,
            supplierDocument
        );

        var productList = new List<Product> { product1, product2 };
        var filteredDescription = "This is a test product 1.";
        var filteredManufacturingDate = new DateTime(2022, 01, 01);
        var filteredExpirationDate = new DateTime(2022, 02, 01);
        _mockProductRepository.Setup(r => r.GetFilteredAsync(filteredDescription, filteredManufacturingDate, filteredExpirationDate)).ReturnsAsync(productList);
        var productService = new ProductService(_mockProductRepository.Object, _unitOfWork);

        // Act
        var result = await productService.GetFilteredProductAsync(filteredDescription, filteredManufacturingDate, filteredExpirationDate);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
        Assert.Equal(2, ((List<Product>)result).Count);
        Assert.Contains(product1, (List<Product>)result);
        Assert.Contains(product2, (List<Product>)result);
    }
}