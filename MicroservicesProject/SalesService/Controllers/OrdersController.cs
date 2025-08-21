using Microsoft.AspNetCore.Mvc;
using SalesService.DTOs.OrderDTO;
using SalesService.Services;

namespace SalesService.Controllers
{
    /// <summary>
    /// Endpoints para criação e consulta de pedidos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService service, ILogger<OrdersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Cria um novo pedido validando estoque no StockService (sincrônico).
        /// </summary>
        /// <param name="request">Itens do pedido.</param>
        /// <response code="201">Pedido criado com sucesso.</response>
        /// <response code="400">Requisição inválida.</response>
        /// <response code="422">Falha de negócio (ex.: estoque insuficiente).</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken ct)
        {
            try
            {
                var created = await _service.CreateAsync(request, ct);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Falha ao criar pedido");
                // 422 UnprocessableEntity para regras de negócio
                return UnprocessableEntity(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retorna um pedido pelo ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken ct)
        {
            var order = await _service.GetByIdAsync(id, ct);
            return order is null ? NotFound() : Ok(order);
        }

        /// <summary>
        /// Lista pedidos paginados.
        /// </summary>
        /// <param name="page">Página (>=1)</param>
        /// <param name="pageSize">Itens por página (1-100)</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        {
            var list = await _service.GetAllAsync(page, pageSize, ct);
            return Ok(list);
        }
    }
}
