using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;
using OnlineShope.Core;
using OnlineShope.Core.Entities;
using OnlineShope.Infrastructure.Model;
using OnlineShope.Infrastructure.Utilitiy;

namespace OnlineShope.Applicaition.Services
{
    public class ProductService : IProductService
    {
        private readonly OnlineShopDbContext dbContext;
        private readonly IMapper mapper;
        private readonly MyFileUtility myFileUtility;
        private readonly ILogger<ProductService> logger;

        public ProductService(OnlineShopDbContext dbContext, IMapper mapper,
            MyFileUtility myFileUtility, ILogger<ProductService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.myFileUtility=myFileUtility;
            this.logger=logger;
            this.logger=logger;
        }
        public async Task<ProductDto> Add(ProductDto model)
        {
            logger.LogInformation("Call Add from ProductDervice");

            //بخش ذخیره تصوریر در جلسه هفتم
            var product = new Product
            {
                Price=model.Price,
                ProductName = model.ProductName,
                //save in folder
                ThumbnailFileName=myFileUtility.SaveFileInFolder(model.Thumbnail,nameof(Product),true),
                //db=>byte[]
                Thumbnail= myFileUtility.EncryptFile(myFileUtility.ConvertToByteArray(model.Thumbnail)),
                ThumbnailFileExtenstion=myFileUtility.GetFileExtension(model.Thumbnail.FileName),
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

            //if (product != null)
            //{
            //    var model = new ProductDto
            //    {
            //        ProductName = product.ProductName,
            //        Id = product.Id,
            //        Price = product.Price,

            //    };
            //    var model = mapper.Map<ProductDto>(product);
            //    return model;
            //}
            //else
            //    return null;
            var model = new ProductDto
            {
                ProductName = product.ProductName,
                Id = product.Id,
                Price = product.Price,
                PriceWithComma=product.Price.ToString("##.##"),
                ThumbnailBase64 = myFileUtility.ConvertToBase64(myFileUtility.DecryptFile(product.Thumbnail)),
                ThumbnailURL = myFileUtility.GetFileUrl(product.ThumbnailFileName,nameof(product)),

            };
           // var model = mapper.Map<ProductDto>(product);
            return model;
        }

        //public async Task<List<ProductDto>> GetAll(int page = 1, int size = 3)
        public async Task<ShopActionResult<List<ProductDto>>> GetAll(int page = 1, int size = 3)
        {
            logger.LogInformation("Call GetAll from ProductDervice");

            var result = new ShopActionResult<List<ProductDto>>();

            try
            {
                var products = await dbContext.Products
                .Skip((page-1)*size).Take(size)
                .AsNoTracking()
                .Select(Product => new ProductDto
                {
                    Price = Product.Price,
                    ProductName = Product.ProductName,
                    Id=Product.Id,
                }).ToListAsync();
                var totalRecordCount = await dbContext.Products.CountAsync();

                result.IsSuccess=true;
                result.Data=products;
                result.Page=page;
                result.Size=size;
                result.Total=totalRecordCount;

                logger.LogInformation("GetAll from ProductDervice success call");

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                result.IsSuccess=false;
                result.Message=ex.Message;
            }
            

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
