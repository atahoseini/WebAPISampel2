using Microsoft.EntityFrameworkCore;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;
using OnlineShope.Core;
using OnlineShope.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace OnlineShope.Applicaition.Services
{
    public class ProductService : IProductService
    {
        private readonly OnlineShopDbContext dbContext;

        public ProductService(OnlineShopDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        public async Task<ProductDto> Add(ProductDto model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                Price=model.Price,
            };

            //dbContext.Products.Add(product);
            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();
            model.Id=product.Id;
            return model;
        }

        public async Task<ProductDto> Get(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            var model = new ProductDto
            {
                ProductName = product.ProductName,
                Id = product.Id,
                Price = product.Price,

            };
            return model;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var result = dbContext.Products.Select(Product => new ProductDto
            {
                Price = Product.Price,
                ProductName = Product.ProductName,
                Id=Product.Id,
            }).ToListAsync();
            return await result;
        }
    }
}
