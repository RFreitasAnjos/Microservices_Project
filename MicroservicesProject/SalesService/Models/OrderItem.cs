using System.ComponentModel.DataAnnotations;

namespace SalesService.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        // Produto no StockService
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Foreign Key -> 
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;
    }
}
