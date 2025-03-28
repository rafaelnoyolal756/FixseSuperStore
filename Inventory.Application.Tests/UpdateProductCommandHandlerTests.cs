using Inventory.Application.Features.Commands;
using Inventory.Application.Features.Handler;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Tests
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnUpdatedProduct_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = new Product { Id = productId, Name = "Old Name", Price = 50, Stock = 10 };
            var updateProductCommand = new UpdateProductCommand
            {
                Id = productId,
                Name = "New Name",
                Price = 100,
                Stock = 20
            };

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            _productRepositoryMock.Setup(repo => repo.UpdateAsync(existingProduct))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(updateProductCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateProductCommand.Name, result.Name);
            Assert.Equal(updateProductCommand.Price, result.Price);
            Assert.Equal(updateProductCommand.Stock, result.Stock);

            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateAsync(existingProduct), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var updateProductCommand = new UpdateProductCommand
            {
                Id = productId,
                Name = "New Name",
                Price = 100,
                Stock = 20
            };

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _handler.Handle(updateProductCommand, CancellationToken.None);

            // Assert
            Assert.Null(result);

            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}
