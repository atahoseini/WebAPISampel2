using OnlineShope.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Core.IRepositories
{
    public interface IProductRepository
    {
        Task<Product> Get(int id);
        Task<List<Product>> GetAll();
        Task<int> Insert(Product product);

    }
}
