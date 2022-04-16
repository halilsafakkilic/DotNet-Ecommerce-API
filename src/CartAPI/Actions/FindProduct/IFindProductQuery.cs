using System.Threading;
using System.Threading.Tasks;
using CartAPI.DTOs;

namespace CartAPI.Actions.FindProduct;

public interface IFindProductQuery
{
    Task<ProductItemDto> SendRequestAsync(AddToCartRequestDto request, CancellationToken cancellationToken);
}