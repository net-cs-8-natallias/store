using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;


var configuration = GetConfiguration();
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CatalogConfigurations>(configuration);
builder.Services.AddDbContextFactory<ApplicationDbContext>(opts => opts.UseNpgsql(configuration["ConnectionString"]));

builder.Services.AddTransient<ICatalogRepository<ItemBrand>, BrandsRepository>();
builder.Services.AddTransient<ICatalogRepository<ItemType>, TypeRepository>();
builder.Services.AddTransient<ICatalogRepository<ItemCategory>, CategoryRepository>();
builder.Services.AddTransient<ICatalogItemsRepository, CatalogItemsRepository>();
builder.Services.AddTransient<ICatalogRepository<Item>, ItemRepository>();
builder.Services.AddTransient<ICatalogRepository<Stock>, StockRepository>();

builder.Services.AddTransient<IBffService, BffService>();
builder.Services.AddTransient<ICatalogService<ItemBrand>, BrandService>();
builder.Services.AddTransient<ICatalogService<ItemType>, TypeService>();
builder.Services.AddTransient<ICatalogService<ItemCategory>, CategoryService>();
builder.Services.AddTransient<ICatalogService<CatalogItem>, CatalogItemService>();
builder.Services.AddTransient<ICatalogService<Item>, ItemService>();
builder.Services.AddTransient<ICatalogService<Stock>, StockService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

CreateDbIfNotExists(app);

app.Run();

Log.CloseAndFlush();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddEnvironmentVariables();

    return builder.Build();
}

void CreateDbIfNotExists(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context).GetAwaiter().GetResult();
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occured creating DB");
    }
}

