using OnlineShope.Applicaition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> Get(int id);
        Task<ProductDto> Add(ProductDto model);
        Task<ProductDto> Update(ProductDto model);
        Task<ProductDto> Delete(int id);
    }
}
