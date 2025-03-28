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
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenProductIsDeleted()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = new Product { Id = productId, Name = "Test Product", Price = 100, Stock = 50 };
            var deleteProductCommand = new DeleteProductCommand { Id = productId };

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            _productRepositoryMock.Setup(repo => repo.DeleteAsync(existingProduct))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(deleteProductCommand, CancellationToken.None);

            // Assert
            Assert.True(result);

            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            _productRepositoryMock.Verify(repo => repo.DeleteAsync(existingProduct), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var deleteProductCommand = new DeleteProductCommand { Id = productId };

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _handler.Handle(deleteProductCommand, CancellationToken.None);

            // Assert
            Assert.False(result);

            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            _productRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}
