using Newtonsoft.Json;

namespace CartAPI.DTOs;

public class AddToCartRequestDto
{
    public string Sku { get; set; }

    [JsonProperty("quantity")] public int AmountInOrder { get; set; }
}