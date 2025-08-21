namespace StockService.DTOs
{
    public class StockProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
