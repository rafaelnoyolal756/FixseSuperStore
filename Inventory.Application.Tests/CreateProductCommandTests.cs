using Inventory.Application.Features.Commands;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.Application.Tests
{
    public class CreateProductCommandTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly CreateProductHandler _handler;

        public CreateProductCommandTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new CreateProductHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProduct_WhenProductIsCreated()
        {
            // Arrange
            var createProductCommand = new CreateProductCommand
            {
                Name = "Test Product",
                Price = 100,
                Stock = 50
            };

            _productRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product product) => product);

            // Act
            var result = await _handler.Handle(createProductCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createProductCommand.Name, result.Name);
            Assert.Equal(createProductCommand.Price, result.Price);
            Assert.Equal(createProductCommand.Stock, result.Stock);

            _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
