using Basket.Host.Configurations;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using Microsoft.OpenApi.Models;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Store- Basket HTTP API",
        Description = "The Store Basket Service HTTP API"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { "basket" }
        }
    });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows()
            {
                Implicit = new OpenApiOAuthFlow()
                {
                    AuthorizationUrl = new Uri("http://localhost:7001/connect/authorize"),
                    TokenUrl = new Uri("http://localhost:7001/connect/token"),
                    Scopes = new Dictionary<string, string>()
                    {
                        { "basket", "Basket Api - access to bff" },
                    }
                }
            }
        }
    );
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        // required audience of access tokens
        options.Audience = "BasketApi";
        options.RequireHttpsMetadata = false;


        // auth server base endpoint (this will be used to search for disco doc)
        options.Authority = "http://localhost:7001";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "basket");
    });
});

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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute().RequireAuthorization("ApiScope");
    endpoints.MapControllers();//.RequireAuthorization -> for all controllers
});

app.Run();

Log.CloseAndFlush();

