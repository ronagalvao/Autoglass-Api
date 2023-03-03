using Autoglass.Application.Dtos;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Validations;
using AutoMapper;
using FluentValidation.TestHelper;
using Moq;

namespace Autoglass.UnitTests.Domain.Validations;

public class ProductValidationTests
{
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductValidation _productValidation;

    public ProductValidationTests()
    {
        _mapperMock = new Mock<IMapper>();
        _productValidation = new ProductValidation();
    }

    [Fact]
    public void ManufacturingDate_ShouldHaveError_WhenEmpty()
    {
        // Arrange
        var productDto = new ProductDto { Id = 1, Description = "Test product", ExpirationDate = DateTime.Now.AddDays(30) };
        
        var product = new Product();
        _mapperMock.Setup(m => m.Map<Product>(productDto)).Returns(product);

        // Act
        var result = _productValidation.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ManufacturingDate)
              .WithErrorMessage("A data de fabricação do produto é obrigatória.");
    }

    [Fact]
    public void ManufacturingDate_ShouldHaveError_WhenGreaterThanExpirationDate()
    {
        // Arrange
        var product = new Product
        (
            1,
            "Test Product",
            ProductStatus.Active,
            new DateTime (2024, 03, 02),
            new DateTime (2023, 03, 02),
            1,
            "Test Supplier",
            "1234567890"
        );
        
        // Act
        var result = _productValidation.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ManufacturingDate)
              .WithErrorMessage("A data de fabricação não pode ser maior ou igual à data de validade.");
    }

    [Fact]
    public void ManufacturingDate_ShouldNotHaveError_WhenValid()
    {
        // Arrange
        var product = new Product
        (
            1,
            "Test Product",
            ProductStatus.Active,
            DateTime.Now,
            DateTime.Now.AddDays(30),
            1,
            "Test Supplier",
            "1234567890"
        );

        // Act
        var result = _productValidation.TestValidate(product);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.ManufacturingDate);
    }
}