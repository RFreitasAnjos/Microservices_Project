using StockService.DTOs;

namespace SalesService.Integrations
{
    public class StockClient : IStockClient
    {
        private readonly HttpClient _http;

        public StockClient(HttpClient http) => _http = http;

        public async Task<StockProductDto?> GetProductAsync(int id, CancellationToken ct = default)
        {
            /*        --->       Integrando com StockService       <---
             * Usando endpoint do StockService para validar estoque e obter preço no momento do pedido
             */
            var resp = await _http.GetAsync($"/api/Products/{id}", ct);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<StockProductDto?>();
        }

    }
}
