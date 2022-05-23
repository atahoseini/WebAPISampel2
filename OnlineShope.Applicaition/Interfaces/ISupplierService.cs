using OnlineShope.Applicaition.Models;

namespace OnlineShope.Applicaition.Interfaces
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetAll();
        Task<SupplierDto> Get(int id);
        Task<SupplierDto> Add(SupplierDto model);
        Task<SupplierDto> Update(SupplierDto model);
        Task<SupplierDto> Delete(int id);
    }
}
