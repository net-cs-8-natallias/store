using Basket.Host.Configurations;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var redisConfig = builder.Configuration.GetSection("Redis");
builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("Redis"));
builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<IRedisCacheConnectionService, RedisCacheConnectionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});

app.Run();

