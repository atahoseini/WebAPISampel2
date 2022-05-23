using OnlineShope.Applicaition.Models;

namespace OnlineShope.Applicaition.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAll();
        Task<EmployeeDto> Get(int id);
        Task<EmployeeDto> Add(EmployeeDto model);
        Task<EmployeeDto> Update(EmployeeDto model);
        Task<EmployeeDto> Delete(int id);
    }    
}
