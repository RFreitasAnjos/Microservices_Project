using StockService.DTOs;

namespace SalesService.Integrations
{
    public interface IStockClient
    {
        Task<StockProductDto> GetProductAsync(int id, CancellationToken ct = default);
    }
}
