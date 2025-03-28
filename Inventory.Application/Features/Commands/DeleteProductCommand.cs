using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
