using OnlineShope.Core;
using OnlineShope.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShopDbContext onlineShopDbContext;

        public UnitOfWork(OnlineShopDbContext onlineShopDbContext )
        {
            this.onlineShopDbContext=onlineShopDbContext;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await this.onlineShopDbContext.SaveChangesAsync();
        }
    }
}
