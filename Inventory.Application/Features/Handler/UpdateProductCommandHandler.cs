using Inventory.Application.Features.Commands;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.Handler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return null; // Consider throwing an exception or returning a Result object
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            await _productRepository.UpdateAsync(product);
            return product;
        }
    }
}
