using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CartAPI.Model;

public class Cart
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Sku { get; set; }

    public int Quantity { get; set; }
}