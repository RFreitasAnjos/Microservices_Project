namespace SalesService.DTOs.OrderDTO
{
    public class CreateOrderRequest
    {
        public List<CreateOrderItem> Item { get; set; } = new();
    }
}
