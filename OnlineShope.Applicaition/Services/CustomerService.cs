using Microsoft.EntityFrameworkCore;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;
using OnlineShope.Core;
using OnlineShope.Core.Entities;


namespace OnlineShope.Applicaition.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly OnlineShopDbContext dbContext;

        public CustomerService(OnlineShopDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        public async Task<CustomerDto> Add(CustomerDto model)
        {
            var customer = new Customer
            {
               FirstName= model.FirstName,
               LastName= model.LastName,
               Address= model.Address,
               CityId=model.CityId,
               Email=model.Email,
               Mobile=model.Mobile,

            };

            //dbContext.Products.Add(product);
            await dbContext.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            model.Id=customer.Id;
            return model;
        }

        public async Task<CustomerDto> Get(int id)
        {
            var customer = await dbContext.Customers.FindAsync(id);
            var model = new CustomerDto
            {
                FirstName= customer.FirstName,
                LastName= customer.LastName,
                Address= customer.Address,
                CityId=customer.CityId,
                Email=customer.Email,
                Mobile=customer.Mobile,

            };
            return model;
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            var result = dbContext.Customers.Select(Customer => new CustomerDto
            {
                Mobile=Customer.Mobile,
                Email=Customer.Email,
                FirstName=Customer.FirstName,   
                Address=Customer.Address,
                CityId=Customer.CityId,
                CityName=Customer.City.CityName,
                LastName=Customer.LastName,
                ProvinceName=Customer.City.Province.Title
            }).ToListAsync();
            return await result;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            var result = await dbContext.Customers
                .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                dbContext.Customers.Remove(result);
                await dbContext.SaveChangesAsync();
                var customerDto = new CustomerDto
                {
                   FirstName= result.FirstName,
                   Address= result.Address,
                   CityId = result.CityId,
                   Email=result.Email,
                   LastName=result.LastName,
                   Id=id,
                   Mobile=result.Mobile,
                   CityName=result.City.CityName,
                   ProvinceName=result.City.Province.Title
                };
                return customerDto;
            }

            return null;
        }

        public async Task<CustomerDto> Update(CustomerDto model)
        {
            var result = await dbContext.Customers
              .FirstOrDefaultAsync(e => e.Id == model.Id);

            if (result != null)
            {
                result.Mobile = model.Mobile;
                result.FirstName = model.FirstName;
                result.LastName = model.LastName;
                result.Email = model.Email;
                result.Address = model.Address;
                result.CityId = model.CityId;
                await dbContext.SaveChangesAsync();
                CustomerDto customerDto = new CustomerDto
                {
                    FirstName= result.FirstName,
                    Address= result.Address,
                    CityId = result.CityId,
                    Email=result.Email,
                    LastName=result.LastName,
                    Id=result.Id,
                    Mobile=result.Mobile,
                    CityName=result.City.CityName,
                    ProvinceName=result.City.Province.Title
                };
                return customerDto;
            }

            return null;
        }
    }
}
