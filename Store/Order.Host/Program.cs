

using Microsoft.EntityFrameworkCore;
using Order.Host.Configurations;
using Order.Host.DbContextData;
using Serilog;

var configuration = GetConfiguration();
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OrderConfigurations>(configuration);
builder.Services.AddDbContextFactory<ApplicationDbContext>(opts => opts.UseNpgsql(configuration["ConnectionString"]));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

