using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using OnlineShope.API;
using OnlineShope.API.Hubs;
using OnlineShope.Applicaition;
using OnlineShope.Applicaition.CQRS.ProductCommandQuery.Command;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Services;
using OnlineShope.Core;
using OnlineShope.Core.IRepositories;
using OnlineShope.Infrastructure;
using OnlineShope.Infrastructure.Model;
using OnlineShope.Infrastructure.Repository;
using Serilog;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;




//var builder = WebApplication.CreateBuilder(args);


//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    Args = args
});


var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//add SignalR
builder.Services.AddSignalR();

//fill configs from appsetting.json
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
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
     
});
//Add MiniProfiler
builder.Services.AddMemoryCache();
builder.Services.AddMiniProfiler(options => options.RouteBasePath = "/profiler").AddEntityFramework();

builder.Services.AddJWT();
builder.Services.AddSwagger();
builder.Services.AddApplicaitionServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAPI",
      builder =>
      {
          builder.WithOrigins("*");
          builder.WithHeaders("*");
          builder.WithMethods("*");
      });
});


builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiniProfiler();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.WebRootPath, "Media")),
    RequestPath = "/Media"
});

app.UseRouting();
app.UseCors("MyAPI");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapHub<ChatHub>("/chatHub");

app.Run();
