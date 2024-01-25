using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("brands")]
public class BrandController: ControllerBase
{
    private readonly ILogger<BrandController> _logger;
    private readonly ICatalogService<ItemBrand> _service;

    public BrandController(ICatalogService<ItemBrand> service,
        ILogger<BrandController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("brands")]
    public async Task<ActionResult> GetBrands()
    {
        var brands = await _service.GetCatalog();
        return Ok(brands);
    }

    [HttpGet("brands/{id}")]
    public async Task<ActionResult> GetBrandById(int id)
    {
        var brand = await _service.FindById(id);
        return Ok(brand);
    }

    [HttpPost("brands")]
    public async Task<ActionResult> AddBrand(ItemBrand brand)
    {
        var brandId = await _service.AddToCatalog(brand);
        return Ok(brandId);
    }

    [HttpPut("brands")]
    public async Task<ActionResult> UpdateBrand(ItemBrand brand)
    {
        var updatedBrand = await _service.UpdateInCatalog(brand);
        return Ok(updatedBrand);
    }

    [HttpDelete("brands/{id}")]
    public async Task<ActionResult> DeleteBrand(int id)
    {
        var brand = await _service.RemoveFromCatalog(id);
        return Ok(brand);
    }

}