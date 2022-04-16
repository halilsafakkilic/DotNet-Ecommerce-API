using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CartAPI.Model;
using MongoDB.Driver;

namespace CartAPI.Infrastructure;

public class CartService : ICartService
{
    private readonly IMongoCollection<Cart> _cart;

    public CartService(IProjectSettings settings)
    {
        var client = new MongoClient(settings.DataBaseSettings.ConnectionString);
        var database = client.GetDatabase(settings.DataBaseSettings.DatabaseName);

        _cart = database.GetCollection<Cart>(settings.DataBaseSettings.CartCollectionName);
    }

    public async Task<Cart> FindBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        var items = await _cart.FindAsync(x => x.Sku == sku, null, cancellationToken);

        return items.FirstOrDefault();
    }

    public async Task<Cart> FindOneAndDeleteBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        return await _cart.FindOneAndDeleteAsync(x => x.Sku == sku, null, cancellationToken);
    }


    public async Task<List<Cart>> FindAsync(CancellationToken cancellationToken)
    {
        var result = await _cart.FindAsync(x => true, null, cancellationToken);

        return result.ToList();
    }

    public async Task CreateAsync(Cart cart, CancellationToken cancellationToken)
    {
        await _cart.InsertOneAsync(cart, cancellationToken: cancellationToken);
    }

    public async Task<ReplaceOneResult> UpdateAsync(string id, Cart cart, CancellationToken cancellationToken)
    {
        return await _cart.ReplaceOneAsync(x => x.Id == id, cart, cancellationToken: cancellationToken);
    }
}