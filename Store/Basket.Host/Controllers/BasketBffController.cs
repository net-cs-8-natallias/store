using System.Net;
using System.Security.Claims;
using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Host.Controllers;

[ApiController]
[Route("basket-bff-controller")]
public class BasketBffController: ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }
    
    [HttpPost("item")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(Item item)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _basketService.AddItem(userId!, item);
        return Ok();
    }
    
    [HttpDelete("item")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(Item item)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _basketService.RemoveItem(userId!, item);
        return Ok();
    }

    [HttpGet("items")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _basketService.GetItems(userId!);
        return Ok(response);
    }
}




