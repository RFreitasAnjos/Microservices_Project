using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(50, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [MaxLength(80)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between 0.01 and 1,000,000.00.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0,9999999, ErrorMessage = "Quantity must be between 1 and 9999999")]
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
