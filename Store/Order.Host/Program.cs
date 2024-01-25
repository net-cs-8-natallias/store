

using Microsoft.EntityFrameworkCore;
using Order.Host.Configurations;
using Order.Host.DbContextData;
using Order.Host.DbContextData.Entities;
using Order.Host.Repositories;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;
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

builder.Services.Configure<OrderConfigurations>(configuration);
builder.Services.AddDbContextFactory<ApplicationDbContext>(opts => opts.UseNpgsql(configuration["ConnectionString"]));

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IHttpClientService, HttpClientService>();

builder.Services.AddTransient<ICatalogOrderRepository, CatalogOrderRepository>();
builder.Services.AddTransient<IOrderRepository<OrderItem>, OrderItemRepository>();

builder.Services.AddTransient<IJsonSerializer, JsonSerializer>();
builder.Services.AddTransient<IOrderService<CatalogOrder>, CatalogOrderService>();
builder.Services.AddTransient<IOrderService<OrderItem>, OrderItemService>();
builder.Services.AddTransient<IOrderBffService, OrderBffService>();

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
    var builderConfig = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddEnvironmentVariables();

    return builderConfig.Build();
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

