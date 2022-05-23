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

        public async Task<ProductDto> Delete(int id)
        {
            var result = await dbContext.Products
                .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                dbContext.Products.Remove(result);
                await dbContext.SaveChangesAsync();
                var productMode = new ProductDto
                {
                    Id = result.Id,
                    Price= result.Price,
                    ProductName=result.ProductName
                };
                return productMode;
            }

            return null;
        }

        public async Task<ProductDto> Update(ProductDto model)
        {
            var result = await dbContext.Products
              .FirstOrDefaultAsync(e => e.Id == model.Id);

            if (result != null)
            {
                result.ProductName = model.ProductName;
                 result.Price = model.Price;
                await dbContext.SaveChangesAsync();
                ProductDto product = new ProductDto
                {
                    Id=model.Id,
                    Price = model.Price,
                    ProductName= model.ProductName
                };
                return product;
            }

            return null;
        }
    }

}
