using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CartAPI.DTOs;
using CartAPI.Infrastructure;
using Newtonsoft.Json;

namespace CartAPI.Actions.FindProduct;

public class FindProductQuery : IFindProductQuery
{
    private readonly HttpClient _httpClient;

    public FindProductQuery(IHttpClientFactory clientFactory, IProjectSettings projectSettings)
    {
        _httpClient = clientFactory.CreateClient("productService");
        _httpClient.BaseAddress = new Uri(projectSettings.ApiGatewayUrl);
    }

    public async Task<ProductItemDto> SendRequestAsync(AddToCartRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/api/product/findBySKU/{request.Sku}", cancellationToken);

        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception($"HTTP Request Failed! StatusCode: {response.StatusCode} | Response: {responseBody}");

        return JsonConvert.DeserializeObject<ProductItemDto>(responseBody);
    }
}