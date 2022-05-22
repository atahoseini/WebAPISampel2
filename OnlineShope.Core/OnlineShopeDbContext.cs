using Microsoft.EntityFrameworkCore;
using OnlineShope.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Core
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<Product> Products { get; set; }
        public DbSet<Product> Products => Set<Product>();

    }
}
