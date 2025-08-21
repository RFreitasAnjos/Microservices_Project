using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockService.Models;
using StockService.Services;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _looger;
        public ProductsController(IProductService service, ILogger<ProductsController> logger) 
        {
            _productService = service;
            _looger = logger;
        }

        // TO DO -> realizar melhoria posterior
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllASync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdASync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if(product == null) return BadRequest();

            var existingProduct = await _productService.GetByIdASync(id);
            if (existingProduct == null) return NotFound();

            var updated = await _productService.UpdateAsync(existingProduct);
            return Ok(updated);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var created = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}
