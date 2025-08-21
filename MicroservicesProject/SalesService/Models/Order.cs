using System.ComponentModel.DataAnnotations;

namespace SalesService.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public List<OrderItem> Item { get; set; } = new();
    }
}
