using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineShope.Applicaition;
using OnlineShope.Applicaition.CQRS.ProductCommandQuery.Command;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Services;
using OnlineShope.Core;
using OnlineShope.Core.IRepositories;
using OnlineShope.Infrastructure.Repository;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Store API", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddMediatR(typeof(SaveProductCommand));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

//register AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperConfig());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineShopConnection"));
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
