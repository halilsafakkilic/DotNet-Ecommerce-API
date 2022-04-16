using System.Threading;
using System.Threading.Tasks;
using CartAPI.Actions.SaveToCart;
using CartAPI.DTOs;
using CartAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CartAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IAddToCartCommand _saveToCartCommand;

    public CartController(IAddToCartCommand saveToCartCommand, ICartService cartService)
    {
        _saveToCartCommand = saveToCartCommand;
        _cartService = cartService;
    }

    [HttpPut("add")]
    public async Task<IActionResult> AddAsync([FromBody] AddToCartRequestDto dto, CancellationToken cancellationToken)
    {
        await _saveToCartCommand.Call(dto, cancellationToken);

        return Ok();
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListAsync(CancellationToken cancellationToken)
    {
        return Ok(await _cartService.FindAsync(cancellationToken));
    }

    [HttpGet("detail")]
    public async Task<IActionResult> RemoveAsync(string sku, CancellationToken cancellationToken)
    {
        return Ok(await _cartService.FindBySkuAsync(sku, cancellationToken));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAsync(string sku, CancellationToken cancellationToken)
    {
        return Ok(await _cartService.FindOneAndDeleteBySkuAsync(sku, cancellationToken));
    }
}