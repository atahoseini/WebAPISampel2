using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineShope.API;
using OnlineShope.Applicaition;
using OnlineShope.Applicaition.CQRS.ProductCommandQuery.Command;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Services;
using OnlineShope.Core;
using OnlineShope.Core.IRepositories;
using OnlineShope.Infrastructure;
using OnlineShope.Infrastructure.Model;
using OnlineShope.Infrastructure.Repository;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOptions();
builder.Services.Configure<Configs>(builder.Configuration.GetSection("Configs"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Store API", Version = "v1" });
//    c.EnableAnnotations();
//});



builder.Services.AddMediatR(typeof(SaveProductCommand));

builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();
builder.Services.AddInfraUtility();

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

builder.Services.AddJWT();
builder.Services.AddSwagger();
builder.Services.AddApplicaitionServices();

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
