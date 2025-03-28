using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
    }
}
