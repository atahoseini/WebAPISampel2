using Microsoft.EntityFrameworkCore;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;
using OnlineShope.Core;
using OnlineShope.Core.Entities;


namespace OnlineShope.Applicaition.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly OnlineShopDbContext dbContext;

        public EmployeeService(OnlineShopDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        public async Task<EmployeeDto> Add(EmployeeDto model)
        {
            var employee = new Employee
            {
                FirstName=model.FirstName,
                LastName=model.LastName,
                IsActive=true,
                Mobile=model.Mobile,
            };

            await dbContext.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            model.Id=employee.Id;
            return model;
        }

        public async Task<EmployeeDto> Get(int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            var model = new EmployeeDto
            {
                Mobile=employee.Mobile,
                FirstName=employee.FirstName,
                LastName=employee.LastName,
                ActiveStatus= employee.IsActive ? "فعال":"غیرفعال"
            };
            return model;
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            var result = dbContext.Employees.Select(Employees => new EmployeeDto
            {
                Mobile=Employees.Mobile,
                FirstName=Employees.FirstName,
                LastName=Employees.LastName,
                ActiveStatus= Employees.IsActive ? "فعال" : "غیرفعال",
                Id=Employees.Id              
            }).ToListAsync();
            return await result;
        }

        public async Task<EmployeeDto> Delete(int id)
        {
            var result = await dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                dbContext.Employees.Remove(result);
                await dbContext.SaveChangesAsync();
                var employeeDto = new EmployeeDto
                {
                    Mobile=result.Mobile,
                    FirstName=result.FirstName,
                    LastName=result.LastName,
                    ActiveStatus= result.IsActive ? "فعال" : "غیرفعال",
                    Id=result.Id,
                };
                return employeeDto;
            }

            return null;
        }

        public async Task<EmployeeDto> Update(EmployeeDto model)
        {
            var result = await dbContext.Employees
              .FirstOrDefaultAsync(e => e.Id == model.Id);

            if (result != null)
            {
                result.Mobile = model.Mobile;
                result.FirstName = model.FirstName; 
                result.LastName = model.LastName;
                result.IsActive=model.ActiveStatus == "فعال"?true:false;             
                await dbContext.SaveChangesAsync();
                EmployeeDto employeeDto = new EmployeeDto
                {
                    Mobile=result.Mobile,
                    FirstName=result.FirstName,
                    LastName=result.LastName,
                    ActiveStatus= result.IsActive ? "فعال" : "غیرفعال",
                    Id=result.Id,
                };
                return employeeDto;
            }

            return null;
        }
    }

}
