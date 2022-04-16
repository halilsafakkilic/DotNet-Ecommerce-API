using System;

namespace ProductAPI.Models;

public class Product
{
    public Product(Guid id, string name, string sku, int stockAmount)
    {
        Id = id;
        Name = name;
        Sku = sku;
        StockAmount = stockAmount;
    }

    public Guid Id { get; }

    public string Name { get; }

    public string Sku { get; }

    public int StockAmount { get; }
}