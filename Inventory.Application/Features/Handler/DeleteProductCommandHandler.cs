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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return false; // Consider throwing an exception or using a Result object
            }

            await _productRepository.DeleteAsync(product);
            return true;
        }
    }
}
