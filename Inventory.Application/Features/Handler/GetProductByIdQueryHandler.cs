using Inventory.Application.Features.Queries;
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
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
