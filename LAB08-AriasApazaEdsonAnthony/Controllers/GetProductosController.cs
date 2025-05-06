using LAB08_AriasApazaEdsonAnthony.Repositories;
using LAB08_AriasApazaEdsonAnthony.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB08_AriasApazaEdsonAnthony.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProductosController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public GetProductosController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/GetProductos?precioMinimo=20
        [HttpGet]
        public async Task<IActionResult> GetProductos([FromQuery] decimal precioMinimo)
        {
            var productos = await _productRepository.FindAsync(p => p.Price > precioMinimo);

            if (productos == null || !productos.Any())
            {
                return NotFound("No se encontraron productos con un precio mayor al valor especificado.");
            }

            return Ok(productos);
        }
    }
}