using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.DTOs.OrderDTO;
using SalesService.Integrations;
using SalesService.Models;

namespace SalesService.Services
{
    public class OrderService : IOrderService
    {
        private readonly SalesContext _context;
        private readonly IStockClient _stock;

        public OrderService(SalesContext context, IStockClient stock)
        {
            _context = context;
            _stock = stock;
        }

        public async Task<OrderResponse> CreateAsync(CreateOrderRequest request, CancellationToken ct = default)
        {
            try
            {
                // Validando itens e enriquecendo com dados de produto do StockService
                var items = new List<OrderItem>();
                decimal total = 0m;

                foreach (var reqItem in request.Item)
                {
                    var product = await _stock.GetProductAsync(reqItem.ProductId);
                    // Criar uma lógica em outra classe para deixar código mais limpo
                    // TODO: criar log para debugar em caso de erro.
                    if (product is null) throw new InvalidOperationException($"Produto {reqItem.ProductId} não encontrado no estoque");
                    if (product.Quantity < reqItem.Quantity) throw new InvalidOperationException($"Estoque insuficente para o produto {reqItem.ProductId}");

                    var unitPrice = product.Price;
                    items.Add(new OrderItem
                    {
                        ProductId = reqItem.ProductId,
                        Quantity = reqItem.Quantity,
                        UnitPrice = unitPrice,
                    });
                    total += unitPrice * reqItem.Quantity;
                }

                //Persistir dados do pedido e tiens em db
                var order = new Order { Item = items, Total = total };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync(ct);

                // depois usar automapper
                return new OrderResponse
                {
                    Id = order.Id,
                    CreatedAtUtc = order.CreatedDate,
                    Total = order.Total,
                    Items = order.Item.Select(i => new OrderItemResponse
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                    }).ToList(),
                };
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        public async Task<IReadOnlyList<OrderResponse>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default)
        {
            try
            {
                page = Math.Max(page, 1);
                pageSize = Math.Clamp(pageSize, 1, 100);

                var orders = await _context.Orders
                    .AsNoTracking()
                    .Include(o => o.Item)
                    .OrderByDescending(o => o.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(ct);
                return orders.Select(o => new OrderResponse
                {
                    Id = o.Id,
                    CreatedAtUtc = o.CreatedDate,
                    Total = o.Total,
                    Items = o.Item.Select(i => new OrderItemResponse
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderResponse?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var order = await _context.Orders.AsNoTracking()
                    .Include(o => o.Item)
                    .FirstOrDefaultAsync(o => o.Id == id, ct);
                if (order is null) return null;
                return new OrderResponse
                {
                    Id = order.Id,
                    CreatedAtUtc = order.CreatedDate,
                    Total = order.Total,
                    Items = order.Item.Select(i => new OrderItemResponse
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
