using SalesService.DTOs.OrderDTO;

namespace SalesService.Services
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateAsync(CreateOrderRequest request, CancellationToken ct = default);
        Task<OrderResponse?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<OrderResponse>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default);
    }
}
