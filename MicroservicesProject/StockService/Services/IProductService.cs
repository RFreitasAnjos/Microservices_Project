using StockService.Models;

namespace StockService.Services
{
    public interface IProductService
    {
        // TODO update -> inserir métodos específicos aqui e enviar para a classe.
        Task<List<Product>> GetAllASync();
        Task<Product> GetByIdASync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
    }
}
