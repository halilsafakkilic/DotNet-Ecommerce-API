using System.Threading;
using System.Threading.Tasks;
using CartAPI.DTOs;

namespace CartAPI.Actions.SaveToCart;

public interface IAddToCartCommand
{
    Task<ProductItemDto> Call(AddToCartRequestDto request, CancellationToken cancellationToken);
}