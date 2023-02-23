using Autoglass.API.Controllers;
using Autoglass.API.Helpers;
using Autoglass.Application.Dtos;
using Autoglass.Application.Interfaces;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Autoglass.UnitTests.API.Controllers;
public class ProductControllerTests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ProductValidation> _productValidationMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _mapperMock = new Mock<IMapper>();
            _productValidationMock = new Mock<ProductValidation>();

            _controller = new ProductsController(
                _productServiceMock.Object,
                _mapperMock.Object,
                _productValidationMock.Object
            );
        }

        [Fact]
        public async Task GetProductById_ReturnsOkResult_WhenProductExists()
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

            var productDto = new ProductDto { Id = productId };

            _productServiceMock.Setup(s => s.GetProductByIdAsync(productId))
                .ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductDto>(product))
                .Returns(productDto);

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFoundResult_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 1;

            _productServiceMock.Setup(s => s.GetProductByIdAsync(productId))
                .ReturnsAsync((Product)null!);

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetFilteredProduct_ReturnsOkResult_WithPagedList_WhenProductsExist()
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
            var products = new List<Product>
            {
                product1,
                product2
            };

            var productDtos = new List<ProductDto>
            {
                new ProductDto { Id = 1 },
                new ProductDto { Id = 2 }
            };
            var pageNumber = 1;
            var pageSize = 2;

            _productServiceMock.Setup(s => s.GetFilteredProductAsync(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(productDtos);

            // Act
            var result = await _controller.GetFilteredProduct(null, null, null, pageNumber, pageSize) as OkObjectResult;
            var pagedList = result!.Value as PagedList<ProductDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(pagedList);
            Assert.Equal(products.Count, pagedList.PageSize);
            Assert.Equal(pageNumber, pagedList.PageNumber);
            Assert.Equal(pageSize, pagedList.PageSize);
            Assert.Equal(productDtos, pagedList.Items);
        }
    }
}
