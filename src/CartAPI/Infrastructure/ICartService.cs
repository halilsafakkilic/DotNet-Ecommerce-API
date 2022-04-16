using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CartAPI.Model;
using MongoDB.Driver;

namespace CartAPI.Infrastructure;

public interface ICartService
{
    public Task<Cart> FindBySkuAsync(string sku, CancellationToken cancellationToken);

    public Task<Cart> FindOneAndDeleteBySkuAsync(string sku, CancellationToken cancellationToken);


    public Task<List<Cart>> FindAsync(CancellationToken cancellationToken);

    public Task CreateAsync(Cart cart, CancellationToken cancellationToken);

    public Task<ReplaceOneResult> UpdateAsync(string id, Cart cart, CancellationToken cancellationToken);
}