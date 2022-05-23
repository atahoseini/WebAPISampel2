using OnlineShope.Applicaition.Models;

namespace OnlineShope.Applicaition.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAll();
        Task<CustomerDto> Get(int id);
        Task<CustomerDto> Add(CustomerDto model);
        Task<CustomerDto> Update(CustomerDto model);
        Task<CustomerDto> Delete(int id);
    }
}
