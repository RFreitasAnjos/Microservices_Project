using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Models;

namespace StockService.Services
{
    public class ProductService : IProductService
    {
        private readonly StockContext _context;

        public ProductService(StockContext context)
        {
            _context = context; 
        }

        public async Task<List<Product>> GetAllASync() => await _context.Products.ToListAsync();

        public async Task<Product> GetByIdASync(int id) => await _context.Products.FindAsync(id);

        public async Task<Product> CreateAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex); //-> fazer update, esse código deve ir para o repository
            }
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}",ex); // -> Fazer melhoria 
            }
        }
    }
}
