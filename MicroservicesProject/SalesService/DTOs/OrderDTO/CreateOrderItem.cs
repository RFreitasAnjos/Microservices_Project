namespace SalesService.DTOs.OrderDTO
{
    public class CreateOrderItem
    {
        /// <summary>
        ///  ID do produto no StockService e sua quantidade
        /// </summary>
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
