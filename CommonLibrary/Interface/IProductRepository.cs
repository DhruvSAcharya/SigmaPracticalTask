using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Interface
{
    public interface IProductRepository
    {
        Task AddAsync(Product Product);
        Task UpdateAsync(Product Product);
        Task DeleteAsync(int id);
        Task<Product> GetAsync(int id);
        Task<List<Product>> GetAllAsync();
    }
}
