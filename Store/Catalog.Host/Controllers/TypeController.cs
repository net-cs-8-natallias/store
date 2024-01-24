using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("types")]
public class TypeController: ControllerBase
{
    private readonly ILogger<TypeController> _logger;
    private readonly ICatalogService<ItemType> _service;

    public TypeController(ICatalogService<ItemType> service,
        ILogger<TypeController> logger)
    {
        _service = service;
        _logger = logger;
    }
}