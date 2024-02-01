using System.Net;
using System.Security.Claims;
using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Host.Controllers;

//[Authorize(Policy = "ApiScope")]
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
    public async Task<IActionResult> Add(Item item, string userId = null)
    {
        if (userId == null)
        {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        await _basketService.AddItem(userId!, item);
        return Ok();
    }
    
    [HttpDelete("item/{itemId}/{quantity}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(int itemId, int quantity, string userId = null)
    {
        if (userId == null)
        {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        await _basketService.RemoveItem(userId!, new(){ItemId = itemId, Quantity = quantity});
        return Ok();
    }

    [HttpGet("items")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(string userId = null)
    {
        if (userId == null)
        {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        var response = await _basketService.GetItems(userId!);
        return Ok(response);
    }
    
    [HttpDelete("items")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteAll(string userId = null)
    {
        if (userId == null)
        {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        await _basketService.RemoveAll(userId!);
        return Ok();
    }

    [HttpPost("items/checkout")]
    public async Task<IActionResult> CheckoutBasket(string userId = null)
    {
        if (userId == null)
        {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        var orderId = await _basketService.CheckoutBasket(userId);
        return Ok(orderId);
    }
}




