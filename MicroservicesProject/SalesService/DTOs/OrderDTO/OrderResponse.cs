namespace SalesService.DTOs.OrderDTO
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();

    }
}
