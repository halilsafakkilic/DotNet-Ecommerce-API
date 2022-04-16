using System;
using System.Threading;
using System.Threading.Tasks;
using CartAPI.Actions.FindProduct;
using CartAPI.DTOs;
using CartAPI.Infrastructure;
using CartAPI.Model;

namespace CartAPI.Actions.SaveToCart;

public class AddToCartCommand : IAddToCartCommand
{
    private readonly ICartService _cartService;
    private readonly IFindProductQuery _checkProductQuery;

    public AddToCartCommand(IFindProductQuery checkProductQuery, ICartService cartService)
    {
        _checkProductQuery = checkProductQuery;
        _cartService = cartService;
    }

    public async Task<ProductItemDto> Call(AddToCartRequestDto request, CancellationToken cancellationToken)
    {
        var requestedAmount = request.AmountInOrder;
        if (requestedAmount <= 0) throw new Exception("Requested quantity is not valid!");

        var product = await _checkProductQuery.SendRequestAsync(request, cancellationToken);
        if (product == null) throw new Exception("Requested product is not found!");

        var cart = await _cartService.FindBySkuAsync(request.Sku, cancellationToken);
        if (cart != default(Cart)) requestedAmount += cart.Quantity;

        if (product.StockAmount < requestedAmount) throw new Exception("Available stock is not found!");

        await SaveDb(request, requestedAmount, cart, cancellationToken);

        return product;
    }

    private async Task SaveDb(AddToCartRequestDto request, int requestedAmount, Cart cart,
        CancellationToken cancellationToken)
    {
        if (cart != default(Cart))
        {
            cart.Quantity = requestedAmount;

            await _cartService.UpdateAsync(cart.Id, cart, cancellationToken);
        }
        else
        {
            await _cartService.CreateAsync(new Cart
            {
                Sku = request.Sku,
                Quantity = request.AmountInOrder
            }, cancellationToken);
        }
    }
}