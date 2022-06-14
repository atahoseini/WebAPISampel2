using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;
using OnlineShope.Core;
using OnlineShope.Core.Entities;
using OnlineShope.Infrastructure.Utilitiy;

namespace OnlineShope.Applicaition.Services
{
    public class ProductService : IProductService
    {
        private readonly OnlineShopDbContext dbContext;
        private readonly IMapper mapper;
        private readonly MyFileUtility myFileUtility;

        public ProductService(OnlineShopDbContext dbContext, IMapper mapper, MyFileUtility myFileUtility)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.myFileUtility=myFileUtility;
        }
        public async Task<ProductDto> Add(ProductDto model)
        {
            //بخش ذخیره تصوریر در جلسه هفتم
            var product = new Product
            {
                ProductName = model.ProductName,
                Price=model.Price,
                //db=>byte[]
                Thubmnail=myFileUtility.ConvertToByteArray(model.Thumbnail),
                ThumbnailFileExtension=myFileUtility.GetFileExtension(model.Thumbnail.FileName),
                ThumbnailFileName= model.Thumbnail.FileName,
                ThumbnailFileSize=model.Thumbnail.Length,
            };
            //var product = mapper.Map<Product>(model);
            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();
            model.Id = product.Id;
            return model;
        }

        public async Task<ProductDto> Get(int id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product != null)
            {
                //var model = new ProductDto
                //{
                //    ProductName = product.ProductName,
                //    Id = product.Id,
                //    Price = product.Price,

                //};
                var model = mapper.Map<ProductDto>(product);
                return model;
            }
            else
                return null;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var result = await dbContext.Products.Select(Product => new ProductDto
            {
                Price = Product.Price,
                ProductName = Product.ProductName,
                Id=Product.Id,
            }).ToListAsync();

            //Automapper
            //var products = dbContext.Products.ToList();
            //var result = mapper.Map<List<ProductDto>>(products);

            return result;
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
                    Price = result.Price,
                    ProductName = result.ProductName
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
                    Id = model.Id,
                    Price = model.Price,
                    ProductName = model.ProductName
                };
                return product;
            }

            return null;
        }
    }

}
