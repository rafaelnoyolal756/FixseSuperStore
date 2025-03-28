using Inventory.Application.Features.Handler;
using Inventory.Application.Features.Queries;
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
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = new Product { Id = productId, Name = "Test Product", Price = 100, Stock = 50 };
            var getProductByIdQuery = new GetProductByIdQuery(productId);

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            // Act
            var result = await _handler.Handle(getProductByIdQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingProduct, result);

            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var getProductByIdQuery = new GetProductByIdQuery(productId);

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _handler.Handle(getProductByIdQuery, CancellationToken.None);

            // Assert
            Assert.Null(result);

            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
        }
    }
}
