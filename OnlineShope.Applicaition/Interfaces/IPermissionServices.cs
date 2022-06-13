using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition.Interfaces
{
    public interface IPermissionServices
    {
        Task<bool> CheckPermission(Guid userId,string permissionFalg);
    }
}
