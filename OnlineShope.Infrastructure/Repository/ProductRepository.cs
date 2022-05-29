using OnlineShope.Core;
using OnlineShope.Core.Entities;
using OnlineShope.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShopDbContext onlineShopDbContext;

        public ProductRepository(OnlineShopDbContext onlineShopDbContext)
        {
            this.onlineShopDbContext=onlineShopDbContext;
        }
        public async Task<Product> Get(int id)
        {
            return await onlineShopDbContext.Products.FindAsync(id);
        }

        public Task<List<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(Product product)
        {
            await onlineShopDbContext.Products.AddAsync(product);   
            //await onlineShopDbContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
