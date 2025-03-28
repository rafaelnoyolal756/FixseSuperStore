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
    public class GetAllProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetAllProductsQueryHandler _handler;

        public GetAllProductsQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetAllProductsQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 10, Stock = 100 },
            new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 20, Stock = 200 }
        };

            _productRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(products);

            var query = new GetAllProductsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(products, result);

            _productRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            _productRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Product>());

            var query = new GetAllProductsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            _productRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
