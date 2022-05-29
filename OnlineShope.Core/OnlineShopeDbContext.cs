﻿using Microsoft.EntityFrameworkCore;
using OnlineShope.Core.Entities;
using OnlineShope.Core.FluentAPIConfiguration;
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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Product>()
        //    //        .Property(s => s.ProductName)
        //    //        .HasColumnName("Name")
        //    //        .IsRequired();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        }

        //public DbSet<Product> Products { get; set; }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<User> Users => Set<User>();



    }
}
