using Microsoft.EntityFrameworkCore;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;
using OnlineShope.Core;
using OnlineShope.Core.Entities;


namespace OnlineShope.Applicaition.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly OnlineShopDbContext dbContext;

        public SupplierService(OnlineShopDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        public async Task<SupplierDto> Add(SupplierDto model)
        {
            var supplier = new Supplier
            {
               SupplierName= model.SupplierName,
               Address= model.Address,
               CityId=model.CityId,
            };

            //dbContext.Products.Add(product);
            await dbContext.AddAsync(supplier);
            await dbContext.SaveChangesAsync();
            model.Id=supplier.Id;
            return model;
        }

        public async Task<SupplierDto> Get(int id)
        {
            var supplier = await dbContext.Suppliers.FindAsync(id);
            var model = new SupplierDto
            {
               SupplierName = supplier.SupplierName,
               Address= supplier.Address,
               CityId = supplier.CityId,
               CityName=supplier.City.CityName,
               Id=supplier.Id
            };
            return model;
        }

        public async Task<List<SupplierDto>> GetAll()
        {
            var result = dbContext.Suppliers.Select(Supplier => new SupplierDto
            {
                SupplierName = Supplier.SupplierName,
                Address= Supplier.Address,
                CityId = Supplier.CityId,
                CityName=Supplier.City.CityName,
                Id=Supplier.Id
            }).ToListAsync();
            return await result;
        }

        public async Task<SupplierDto> Delete(int id)
        {
            var result = await dbContext.Suppliers
                .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                dbContext.Suppliers.Remove(result);
                await dbContext.SaveChangesAsync();
                var supplierDto = new SupplierDto
                {
                    SupplierName = result.SupplierName,
                    Address= result.Address,
                    CityId = result.CityId,
                    CityName=result.City.CityName,
                    Id=result.Id
                };
                return supplierDto;
            }

            return null;
        }

        public async Task<SupplierDto> Update(SupplierDto model)
        {
            var result = await dbContext.Suppliers
              .FirstOrDefaultAsync(e => e.Id == model.Id);

            if (result != null)
            {

                result.SupplierName = model.SupplierName;
                result.Address= model.Address;
                result.CityId = model.CityId;
                await dbContext.SaveChangesAsync();
                var supplierDto = new SupplierDto
                {
                    SupplierName = result.SupplierName,
                    Address= result.Address,
                    CityId = result.CityId,
                    CityName=result.City.CityName,
                    Id=result.Id
                };
                return supplierDto;
            }

            return null;
        }
    }

}
